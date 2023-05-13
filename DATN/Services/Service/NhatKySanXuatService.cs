using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Service
{
    public class NhatKySanXuatService : INhatKySanXuat
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;

        public NhatKySanXuatService(MasterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable> DanhSachKhuVuc(int nguoiDung)
        {
            return await _dbContext.KhuVucs.Where(c => c.MaNguoiDung == nguoiDung).ToListAsync();
        }

        public async Task<IEnumerable> DanhSachKho()
        {
            return await _dbContext.KhoVatTus.ToListAsync();
        }

        public async Task<IEnumerable> DanhSachVatTu(int warehouse, int csntId)
        {
            var vatTus = await _dbContext.NhatKyMuaSams.Include(c => c.NguoiDung).Where(c => c.MaKhoVatTu == warehouse && c.NguoiDung.MaCoSoNuoiTrong == csntId).ToListAsync();
            return vatTus;
        }

        public async Task<PaginatedResponse<NhatKySanXuat>> DanhSachNhatKySanXuat(string? search = null, int? pageIndex = 1, int? maCSNT = null)
        {
            var nksx = _dbContext.NhatKySanXuats.Include(c => c.KhoVatTu).Include(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).Where(c => c.KhuVuc.NguoiDung.MaCoSoNuoiTrong == maCSNT).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                nksx = nksx.Where(c => c.TenVatTu.Contains(search));
            }
            return new PaginatedResponse<NhatKySanXuat>
            {
                Items = await PaginatedList<NhatKySanXuat>.Create(nksx, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)nksx.Count() / pageSize)
            };
        }

        public async Task<NhatKySanXuat?> SuaNhatKySanXuat(int id, NhatKySanXuatEditRequest request)
        {
            var nksx = await _dbContext.NhatKySanXuats.FirstOrDefaultAsync(c => c.MaNhatKySanXuat == id);
            var nkms = await _dbContext.NhatKyMuaSams.FirstOrDefaultAsync(c => c.TenVatTu == nksx.TenVatTu);
            nksx.SoLuongSuDung = request.SoLuongSuDung;
            if(nkms.SoLuong >= request.SoLuongSuDung)
            {
                nkms.SoLuongDaSuDung = request.SoLuongSuDung;
                Update(nksx);
                Save();
                _dbContext.Update(nkms);
                Save();
                return nksx;
            }
            return null;
        }

        public async Task<NhatKySanXuat?> ThemMoiNhatKySanXuat(NhatKySanXuatRequest request, int csntId)
        {
            var nkms = await _dbContext.NhatKyMuaSams.Include(c => c.NguoiDung).FirstOrDefaultAsync(c => c.TenVatTu == request.TenVatTu && c.NguoiDung.MaCoSoNuoiTrong == csntId);
            var nksxAdd = _mapper.Map<NhatKySanXuat>(request);
            
            
            if(nkms.SoLuong >= (TongSoLuongVatTuDaSuDung(request.TenVatTu, csntId) + request.SoLuongSuDung))
            {
                _dbContext.Add(nksxAdd);
                await _dbContext.SaveChangesAsync();
                nkms.SoLuongDaSuDung = TongSoLuongVatTuDaSuDung(request.TenVatTu, csntId);
                _dbContext.Update(nkms);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }
            return nksxAdd;
        }

        public async Task<NhatKySanXuat?> Tim(Expression<Func<NhatKySanXuat, bool>>? filter = null)
        {
            return await _dbContext.NhatKySanXuats.Include(c => c.KhoVatTu).Include(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).FirstOrDefaultAsync(filter);
        }

        public async Task<NhatKySanXuat?> XoaNhatKySanXuat(int id, int csntId)
        {
            var nksx = await _dbContext.NhatKySanXuats.FirstOrDefaultAsync(c => c.MaNhatKySanXuat == id);
            var nkms = await _dbContext.NhatKyMuaSams.Include(c => c.NguoiDung).FirstOrDefaultAsync(c => c.TenVatTu == nksx.TenVatTu && c.NguoiDung.MaCoSoNuoiTrong == csntId);
            if (nksx == null)
            {
                return null;
            }
            nkms.SoLuongDaSuDung -= nksx.SoLuongSuDung;
            _dbContext.Update(nkms);
            _dbContext.NhatKySanXuats.Remove(nksx);
            await _dbContext.SaveChangesAsync();
            return nksx;
        }

        public int TongSoLuongVatTuDaSuDung(string tenVatTu, int csntId)
        {
            var listVatTu = _dbContext.NhatKySanXuats.Include(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).Where(c => c.TenVatTu == tenVatTu && c.KhuVuc.NguoiDung.MaCoSoNuoiTrong == csntId).ToList();
            int tong = 0;
            foreach(var item in listVatTu)
            {
                tong += item.SoLuongSuDung;
            }
            return tong;
        }
        public NhatKySanXuat Update(NhatKySanXuat entry)
        {
            _dbContext.Entry(entry).State = EntityState.Modified;
            return entry;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

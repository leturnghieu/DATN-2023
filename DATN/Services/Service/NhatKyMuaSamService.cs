using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Service
{
    public class NhatKyMuaSamService : INhatKyMuaSam
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;

        public NhatKyMuaSamService(MasterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<PaginatedResponse<NhatKyMuaSam>> DanhSachNhatKyMuaSam(string? search = null, int? pageIndex = 1, int? maCSNT = null)
        {
            var nkms = _dbContext.NhatKyMuaSams.Include(c => c.KhoVatTu).Include(c => c.NguoiDung).Where(c => c.NguoiDung.MaCoSoNuoiTrong == maCSNT).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                nkms = nkms.Where(c => c.TenVatTu.Contains(search));
            }
            return new PaginatedResponse<NhatKyMuaSam>
            {
                Items = await PaginatedList<NhatKyMuaSam>.Create(nkms, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)nkms.Count() / pageSize)
            };
        }
        public async Task<PaginatedResponse<NhatKyMuaSam>> DanhSachVatTuHetHan(string? search = null, int? pageIndex = 1, int? maCSNT = null)
        {
            var nkms = _dbContext.NhatKyMuaSams.Include(c => c.KhoVatTu).Include(c => c.NguoiDung).Where(c => c.NguoiDung.MaCoSoNuoiTrong == maCSNT && c.TrangThai == "Hết hạn").AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                nkms = nkms.Where(c => c.TenVatTu.Contains(search));
            }
            return new PaginatedResponse<NhatKyMuaSam>
            {
                Items = await PaginatedList<NhatKyMuaSam>.Create(nkms, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)nkms.Count() / pageSize)
            };
        }
        public async Task<NhatKyMuaSam?> SuaNhatKyMuaSam(int id, NhatKyMuaSam request)
        {
            var nkms = await _dbContext.NhatKyMuaSams.FirstOrDefaultAsync(c => c.MaNhatKyMuaSam == id);
            if (nkms == null)
            {
                return null;
            }
            nkms.TenVatTu = request.TenVatTu;
            nkms.XuatXu = request.XuatXu;
            nkms.SoLuong = request.SoLuong;
            nkms.Gia = request.Gia;
            nkms.MaLoaiVatTu = request.MaLoaiVatTu;
            nkms.MaKhoVatTu = request.MaKhoVatTu;
            nkms.NgaySanXuat = request.NgaySanXuat;
            nkms.HanSuDung = request.HanSuDung;
            if (request.HanSuDung < DateTime.Now)
            {
                nkms.TrangThai = "Hết hạn";
            }
            else
            {
                nkms.TrangThai = "Còn hạn";
            }
            _dbContext.Update(nkms);
            await _dbContext.SaveChangesAsync();
            return nkms;
        }

        public async Task<NhatKyMuaSam?> ThemMoiNhatKyMuaSam(int idNguoiDung, NhatKyMuaSamRequest request)
        {
            var nkmsAdd = _mapper.Map<NhatKyMuaSam>(request);
            nkmsAdd.MaNguoiDung = idNguoiDung;
            if(request.HanSuDung < DateTime.Now)
            {
                nkmsAdd.TrangThai = "Hết hạn";
            }
            nkmsAdd.TrangThai = "Còn hạn";
            _dbContext.Add(nkmsAdd);
            await _dbContext.SaveChangesAsync();
            return nkmsAdd;
        }

        public async Task<NhatKyMuaSam?> Tim(Expression<Func<NhatKyMuaSam, bool>>? filter = null)
        {
            return await _dbContext.NhatKyMuaSams.Include(c => c.NguoiDung).Include(c => c.KhoVatTu).FirstOrDefaultAsync(filter);
        }

        public async Task<NhatKyMuaSam?> XoaNhatKyMuaSam(int id)
        {
            var nkms = await _dbContext.NhatKyMuaSams.FirstOrDefaultAsync(c => c.MaNhatKyMuaSam == id);
            if (nkms == null)
            {
                return null;
            }
            _dbContext.NhatKyMuaSams.Remove(nkms);
            await _dbContext.SaveChangesAsync();
            return nkms;
        }

        public async Task<IEnumerable> DanhSachLoaiVatTu()
        {
            return await _dbContext.LoaiVatTus.ToListAsync();
        }

        public async Task<IEnumerable> DanhSachKhoVatTu()
        {
            return await _dbContext.KhoVatTus.ToListAsync();
        }
        public NhatKyMuaSam Update(NhatKyMuaSam entry)
        {
            _dbContext.Entry(entry).State = EntityState.Modified;
            return entry;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public double TongSoTienMuaSam(int maCsnt)
        {
            var nkms = _dbContext.NhatKyMuaSams.Include(c => c.NguoiDung).Where(c => c.NguoiDung.MaCoSoNuoiTrong == maCsnt).ToList();
            double tongTien = 0;
            foreach(var item in nkms)
            {
                tongTien += (double)(item.SoLuong * item.Gia);
            }
            return tongTien;
        }

        public int TongSoVatTuHetHan(int maCsnt)
        {
            var nkms = _dbContext.NhatKyMuaSams.Include(c => c.NguoiDung).Where(c => c.NguoiDung.MaCoSoNuoiTrong == maCsnt).ToList();
            var dem = 0;
            foreach(var item in nkms)
            {
                if(item.TrangThai == "Hết hạn")
                {
                    dem++;
                }    
            }
            return dem;
        }
    }
}

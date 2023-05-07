using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Service
{
    public class NhatKyThuHoachService : INhatKyThuHoach
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;

        public NhatKyThuHoachService(MasterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable> DanhSachKhuVuc(int nguoiDung)
        {
            return await _dbContext.KhuVucs.Where(c => c.MaNguoiDung == nguoiDung).ToListAsync();
        }

        public async Task<PaginatedResponse<NhatKyThuHoach>> DanhSachNhatKyThuHoach(string? search = null, int? pageIndex = 1, int? maCSNT = null)
        {
            var nkth = _dbContext.NhatKyThuHoaches.Include(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).Where(c => c.KhuVuc.NguoiDung.MaCoSoNuoiTrong == maCSNT).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                nkth = nkth.Where(c => c.KhuVuc.SanPham.Contains(search));
            }
            return new PaginatedResponse<NhatKyThuHoach>
            {
                Items = await PaginatedList<NhatKyThuHoach>.Create(nkth, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)nkth.Count() / pageSize)
            };
        }

        public async Task<NhatKyThuHoachEditRequest?> SuaNhatKyThuHoach(int id, NhatKyThuHoachEditRequest request)
        {
            var nkth = await _dbContext.NhatKyThuHoaches.FirstOrDefaultAsync(c => c.MaNhatKyThuHoach == id);
            nkth.SoLuongThuHoach = request.SoLuongThuHoach;
            if(request.SoLuongThuHoach < nkth.SoLuongDaBan)
            {
                return null;
            }
            _dbContext.Update(nkth);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<NhatKyThuHoachEditRequest>(nkth);
        }

        public async Task<NhatKyThuHoach?> ThemMoiNhatKyThuHoach(NhatKyThuHoachRequest request)
        {
            var nkthAdd = _mapper.Map<NhatKyThuHoach>(request);
                _dbContext.Add(nkthAdd);
                await _dbContext.SaveChangesAsync();
            return nkthAdd;
        }

        public async Task<NhatKyThuHoach?> Tim(Expression<Func<NhatKyThuHoach, bool>>? filter = null)
        {
            return await _dbContext.NhatKyThuHoaches.Include(c => c.KhuVuc).Include(c => c.NhatKyBanSanPhams).FirstOrDefaultAsync(filter);
        }

        public async Task<NhatKyThuHoach?> XoaNhatKyThuHoach(int id)
        {
            var nkth = await _dbContext.NhatKyThuHoaches.FirstOrDefaultAsync(c => c.MaNhatKyThuHoach == id);
            if(nkth.SoLuongDaBan == 0)
            {
                _dbContext.NhatKyThuHoaches.Remove(nkth);
                await _dbContext.SaveChangesAsync();
                return nkth;
            }
            return null;
        }
    }
}

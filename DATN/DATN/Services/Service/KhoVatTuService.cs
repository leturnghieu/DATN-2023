using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Service
{
    public class KhoVatTuService : IKhoVatTu
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;

        public KhoVatTuService(MasterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable> DanhSachLoaiVatTu()
        {
            return await _dbContext.LoaiVatTus.ToListAsync();
        }

        public async Task<PaginatedResponse<KhoVatTu>> DanhSachKhoVatTu(string? search = null, int? pageIndex = 1, int? maCSNT = null)
        {
            var vatTu = _dbContext.KhoVatTus.Include(c => c.NhatKySanXuats.Where(c => c.KhuVuc.NguoiDung.MaCoSoNuoiTrong == maCSNT)).Include(c => c.NhatKyVatTus.Where(c => c.NguoiDung.MaCoSoNuoiTrong == maCSNT)).ThenInclude(c => c.NguoiDung).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                vatTu = vatTu.Where(c => c.TenKho.Contains(search));
            }
            return new PaginatedResponse<KhoVatTu>
            {
                Items = await PaginatedList<KhoVatTu>.Create(vatTu, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)vatTu.Count() / pageSize)
            };
        }

        public async Task<KhoVatTu?> ThemMoiKhoVatTu(KhoVatTuRequest request)
        {
            var khoVatTu = await _dbContext.KhoVatTus.FirstOrDefaultAsync(c => c.TenKho == request.TenKho);
            if (khoVatTu != null)
            {
                return null;
            }
            var khoVatTuAdd = _mapper.Map<KhoVatTu>(request);
            _dbContext.Add(khoVatTuAdd);
            await _dbContext.SaveChangesAsync();
            return khoVatTuAdd;
        }

        public async Task<KhoVatTu?> SuaKhoVatTu(int id, KhoVatTu request)
        {
            var kho = await _dbContext.KhoVatTus.FirstOrDefaultAsync(c => c.MaKhoVatTu == id);
            if(kho == null)
            {
                return null;
            }
            kho.TenKho = request.TenKho;
            _dbContext.Update(kho);
            await _dbContext.SaveChangesAsync();
            return kho;
        }

        public async Task<KhoVatTu?> Tim(Expression<Func<KhoVatTu, bool>>? filter = null)
        {
            return await _dbContext.KhoVatTus.FirstOrDefaultAsync(filter);
        }

        public async Task<KhoVatTu?> XoaKhoVatTu(int id)
        {
            var kho = await _dbContext.KhoVatTus.Include(c => c.NhatKyVatTus).Include(c => c.NhatKySanXuats).FirstOrDefaultAsync(c => c.MaKhoVatTu == id);
            if(kho == null)
            {
                return null;
            }
            if(kho.NhatKyVatTus.Count != 0 || kho.NhatKySanXuats.Count != 0)
            {
                return null;
            }
            _dbContext.KhoVatTus.Remove(kho);
            await _dbContext.SaveChangesAsync();
            return kho;
        }
    }
}

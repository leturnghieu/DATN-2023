using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DATN.Services.Service
{
    public class CoSoNuoiTrongService : ICoSoNuoiTrong
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;
        public CoSoNuoiTrongService(MasterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<PaginatedResponse<CoSoNuoiTrong>> DanhSachCoSoNuoiTrong(string? search = null, int? pageIndex = 1)
        {
            var csnt = _dbContext.CoSoNuoiTrongs.Include(c => c.NguoiDungs).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                csnt = csnt.Where(c => c.DiaChi.Contains(search));
            }
            return new PaginatedResponse<CoSoNuoiTrong>
            {
                Items = await PaginatedList<CoSoNuoiTrong>.Create(csnt, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)csnt.Count() / pageSize)
            };
        }

        public async Task<CoSoNuoiTrong?> SuaCoSoNuoiTrong(int id, CoSoNuoiTrongRequest request)
        {
            var csnt = await _dbContext.CoSoNuoiTrongs.FirstOrDefaultAsync(c => c.MaCoSoNuoiTrong == id);
            if (csnt == null)
            {
                return null;
            }
            csnt.DienTich = request.DienTich;
            csnt.DiaChi = request.DiaChi;
            _dbContext.Update(csnt);
            await _dbContext.SaveChangesAsync();
            return csnt;
        }

        public async Task<CoSoNuoiTrong?> ThemMoiCoSoNuoiTrong(CoSoNuoiTrongRequest request)
        {
            var csnt = await _dbContext.CoSoNuoiTrongs.FirstOrDefaultAsync(c => c.DiaChi == request.DiaChi);
            if(csnt == null)
            {
                var csntAdd = _mapper.Map<CoSoNuoiTrong>(request);
                _dbContext.CoSoNuoiTrongs.Add(csntAdd);
                await _dbContext.SaveChangesAsync();
                return csntAdd;
            }
            return null;
        }

        public async Task<CoSoNuoiTrong?> Tim(Expression<Func<CoSoNuoiTrong, bool>>? filter = null)
        {
            return await _dbContext.CoSoNuoiTrongs.Include(c => c.NguoiDungs).FirstOrDefaultAsync(filter);
        }

        public async Task<CoSoNuoiTrong?> XoaCoSoNuoiTrong(int id)
        {
            var csnt = await _dbContext.CoSoNuoiTrongs.Include(c => c.NguoiDungs).FirstOrDefaultAsync(c => c.MaCoSoNuoiTrong == id);
            if(csnt.NguoiDungs.Count > 0)
            {
                return null;
            }    
            _dbContext.CoSoNuoiTrongs.Remove(csnt);
            await _dbContext.SaveChangesAsync();
            return csnt;
        }
    }
}

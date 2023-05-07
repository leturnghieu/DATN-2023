using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList;
using System.Collections;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace DATN.Services.Service
{
    public class KhuVucService : IKhuVuc
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;
        public KhuVucService(MasterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<KhuVuc>> DanhSachKhuVuc(string? search = null, int? pageIndex = 1, int? maCSNT = null)
        {
            var khuVuc = _dbContext.KhuVucs.Include(k => k.NguoiDung).ThenInclude(c => c.CoSoNuoiTrong).Where(c => c.NguoiDung.MaCoSoNuoiTrong == maCSNT).OrderBy(c => c.TenKhuVuc).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if(search != null)
            {
                khuVuc = khuVuc.Where(c => c.TenKhuVuc.Contains(search));
            }
            return new PaginatedResponse<KhuVuc>
            {
                Items = await PaginatedList<KhuVuc>.Create(khuVuc, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)khuVuc.Count() / pageSize)
            };
        }

        public async Task<KhuVuc?> SuaKhuVuc(int id, KhuVuc request, IFormFile fileAnh)
        {
            var khuVuc = await _dbContext.KhuVucs.Include(c => c.NguoiDung).ThenInclude(c => c.CoSoNuoiTrong).FirstOrDefaultAsync(c => c.MaKhuVuc == id);
            var coSo = await _dbContext.CoSoNuoiTrongs.FirstOrDefaultAsync(c => c.MaCoSoNuoiTrong == khuVuc.NguoiDung.MaCoSoNuoiTrong);
            if(khuVuc == null)
            {
                return null;
            }
            khuVuc.TenKhuVuc = request.TenKhuVuc;
            khuVuc.DienTich = request.DienTich;
            khuVuc.SanPham = request.SanPham;
            if(fileAnh != null)
            {
                khuVuc.HinhAnhSanPham = HinhAnh(fileAnh);
            }
            _dbContext.Update(khuVuc);
            await _dbContext.SaveChangesAsync();
            UpdateCoSo(coSo.MaCoSoNuoiTrong, await TongDienTichKhuVuc(khuVuc.NguoiDung.MaNguoiDung));
            return khuVuc;
        }

        public async Task<KhuVuc?> TaoKhuVuc(KhuVucRequest request, int maNguoiDung)
        { 
            var user = await _dbContext.NguoiDungs.Include(c => c.CoSoNuoiTrong).FirstOrDefaultAsync(c => c.MaNguoiDung == maNguoiDung);
            var coSo = await _dbContext.CoSoNuoiTrongs.Include(c => c.NguoiDungs).FirstOrDefaultAsync(c => c.MaCoSoNuoiTrong == user.MaCoSoNuoiTrong);
            var khuVuc = _mapper.Map<KhuVuc>(request);
            khuVuc.MaNguoiDung = maNguoiDung;
            if(request.FileAnh != null)
            {
                khuVuc.HinhAnhSanPham = HinhAnh(request.FileAnh);
            }
            khuVuc.ThoiGianTao = DateTime.Now;
            if ((await TongDienTichKhuVuc(maNguoiDung) + request.DienTich) > coSo.DienTich)
            {
                return null;
            }
            _dbContext.KhuVucs.Add(khuVuc);
            await _dbContext.SaveChangesAsync();
            coSo.DienTichDaSuDung = await TongDienTichKhuVuc(maNguoiDung);
            _dbContext.Update(coSo);
            await _dbContext.SaveChangesAsync();
            
            return khuVuc;

        }

        public async Task<KhuVuc?> Tim(Expression<Func<KhuVuc, bool>>? filter = null)
        {
            return await _dbContext.KhuVucs.Include(c => c.NhatKySanXuats).Include(c => c.NhatKyThuHoachs).Include(c => c.NguoiDung).FirstOrDefaultAsync(filter);
        }

        public async Task<KhuVuc?> XoaKhuVuc(int id)
        {
            var khuVuc = await _dbContext.KhuVucs.FirstOrDefaultAsync(c => c.MaKhuVuc == id);
            var nguoiDung = await _dbContext.NguoiDungs.FirstOrDefaultAsync(c => c.MaNguoiDung == khuVuc.MaNguoiDung);
            _dbContext.KhuVucs.Remove(khuVuc);
            await _dbContext.SaveChangesAsync();
            UpdateCoSo(nguoiDung.MaCoSoNuoiTrong, await TongDienTichKhuVuc(nguoiDung.MaNguoiDung));
            return khuVuc;
        }
        public async Task<double> TongDienTichKhuVuc(int maNguoiDung)
        {
            var khuVuc = await _dbContext.KhuVucs.Where(c => c.MaNguoiDung == maNguoiDung).ToListAsync();
            double Sum = 0;
            foreach(var item in khuVuc)
            {
                Sum += item.DienTich;
            }    
            return Sum;
        }

        public void UpdateCoSo(int id, double dienTich)
        {
            var coSo = _dbContext.CoSoNuoiTrongs.Include(c => c.NguoiDungs).FirstOrDefault(c => c.MaCoSoNuoiTrong == id);
            coSo.DienTichDaSuDung = dienTich;
            _dbContext.Update(coSo);
            _dbContext.SaveChanges();
        }

        public string HinhAnh(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File not selected");

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return "/images/" + fileName;
        }

        public int TongKhuVuc(int maCSNT)
        {
            return _dbContext.KhuVucs.Include(c => c.NguoiDung).Where(c => c.NguoiDung.MaCoSoNuoiTrong == maCSNT).ToList().Count;
        }
    }
}

using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Collections;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace DATN.Services.Service
{
    public class NguoiDungService : INguoiDung
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;

        public NguoiDungService(MasterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NguoiDungRequest?> ChinhSua(int id, NguoiDungRequest request)
        {
            var user = await _dbContext.NguoiDungs.Include(c => c.CoSoNuoiTrong).FirstOrDefaultAsync(c => c.MaNguoiDung == id);
            if(user == null)
            {
                return null;
            }
            user.TenNguoiDung = request.TenNguoiDung;
            user.NgaySinh = request.NgaySinh;
            user.SDT = request.SDT;
            user.GioiTinh = request.GioiTinh;
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<NguoiDungRequest>(user);
        }

        public async Task<NguoiDung?> DangKy(DangKyRequest request)
        {
            var userCheck = await _dbContext.NguoiDungs.FirstOrDefaultAsync(c => c.Email == request.Email);
            if(userCheck != null)
            {
                return null;
            }
            NguoiDung user = _mapper.Map<NguoiDung>(request);
            _dbContext.NguoiDungs.Add(user);
            _dbContext.SaveChanges();
            return user;

        }

        public async Task<NguoiDung?> DangNhap(DangNhapRequest request)
        {
            var user = await _dbContext.NguoiDungs.FirstOrDefaultAsync(c => c.Email == request.Email && c.MatKhau == request.MatKhau);
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<IEnumerable> DanhSachCoSoNuoiTrong()
        {
            return await _dbContext.CoSoNuoiTrongs.ToListAsync();
        }

        public  bool ThemHinhAnh(int id, HinhAnhRequest request)
        {
            if (request.fileAnh == null || request.fileAnh.Length == 0)
                throw new ArgumentException("File not selected");

            var fileName = Path.GetFileName(request.fileAnh.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                request.fileAnh.CopyTo(stream);
            }
            var user =  _dbContext.NguoiDungs.FirstOrDefault(t => t.MaNguoiDung == id);
            user.HinhAnh = "/images/" + fileName;
            _dbContext.SaveChanges();

            return true;
        }
        public async Task<NguoiDung?> Tim(Expression<Func<NguoiDung, bool>>? filter = null)
        {
            var user = await _dbContext.NguoiDungs.Include(c => c.CoSoNuoiTrong).Include(c => c.KhuVucs).Include(c => c.NhatKyMuaSams).FirstOrDefaultAsync(filter);
            return user;
        }
        public async Task<PaginatedResponse<NguoiDung>> DanhSachNguoiDung(string? search = null, int? pageIndex = 1)
        {
            var dsnd = _dbContext.NguoiDungs.Include(c => c.CoSoNuoiTrong).Include(c => c.KhuVucs).Include(c => c.NhatKyMuaSams).Where(c => c.Roles == Models.Enum.Roles.USER).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                dsnd = dsnd.Where(c => c.TenNguoiDung.Contains(search));
            }
            return new PaginatedResponse<NguoiDung>
            {
                Items = await PaginatedList<NguoiDung>.Create(dsnd, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)dsnd.Count() / pageSize)
            };
        }

        public List<NguoiDung> NguoiDungs()
        {
            return _dbContext.NguoiDungs.Include(c => c.CoSoNuoiTrong).Where(c => c.Roles != Models.Enum.Roles.ADMIN).ToList();
        }

        public async Task<PaginatedResponse<NguoiDung>> DanhSachNguoiDungTheoCSNT(string? search = null, int? pageIndex = 1, int? csntId = null)
        {
            var dsnd = _dbContext.NguoiDungs.Include(c => c.CoSoNuoiTrong).Include(c => c.KhuVucs).Include(c => c.NhatKyMuaSams).Where(c => c.Roles == Models.Enum.Roles.USER && c.MaCoSoNuoiTrong == csntId).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                dsnd = dsnd.Where(c => c.TenNguoiDung.Contains(search));
            }
            return new PaginatedResponse<NguoiDung>
            {
                Items = await PaginatedList<NguoiDung>.Create(dsnd, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)dsnd.Count() / pageSize)
            };
        }

        public async Task<NguoiDung?> XoaNguoiDung(int id)
        {
            var nguoiDung = await _dbContext.NguoiDungs.FirstOrDefaultAsync(c => c.MaNguoiDung == id);
            _dbContext.NguoiDungs.Remove(nguoiDung);
            await _dbContext.SaveChangesAsync();
            return nguoiDung;
        }
        public double TongChiPhi(int id)
        {
            var nkms = _dbContext.NhatKyMuaSams.Include(c => c.NguoiDung).Where(c => c.NguoiDung.MaCoSoNuoiTrong == id).ToList();
            double tongTien = 0;
            foreach (var item in nkms)
            {
                tongTien += (double)(item.SoLuong * item.Gia);
            }
            return tongTien;
        }
        public double TongDoanhSo(int id)
        {
            var nkbsp = _dbContext.NhatKyBanSanPhams.Include(c => c.NhatKyThuHoach).ThenInclude(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).Where(c => c.NhatKyThuHoach.KhuVuc.NguoiDung.MaCoSoNuoiTrong == id).ToList();
            double tong = 0;
            foreach (var item in nkbsp)
            {
                tong += (item.SoLuong * item.GiaBan);
            }
            return tong;
        }
    }
}

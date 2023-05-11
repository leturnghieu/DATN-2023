using AutoMapper;
using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Drawing;
using System.Linq.Expressions;
using System.Drawing.Imaging;
using QRCoder;

namespace DATN.Services.Service
{
    public class NhatKyBanSanPhamService : INhatKyBanSanPham
    {
        private readonly MasterDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public NhatKyBanSanPhamService(MasterDbContext dbContext, IMapper mapper, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<IEnumerable> DanhSachNhatKyThuHoach(int id)
        {
            return await _dbContext.NhatKyThuHoaches.Include(c => c.KhuVuc).Where(c => c.KhuVuc.MaNguoiDung == id).ToListAsync();
        }

        public async Task<PaginatedResponse<NhatKyBanSanPham>> DanhSachNhatKyBanSanPham(string? search = null, int? pageIndex = 1, int? maCSNT = null)
        {
            var nkth = _dbContext.NhatKyBanSanPhams.Include(c => c.NhatKyThuHoach).ThenInclude(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).Where(c => c.NhatKyThuHoach.KhuVuc.NguoiDung.MaCoSoNuoiTrong == maCSNT).AsQueryable();
            int pageSize = 5;
            int pageNumber = (pageIndex ?? 1);
            if (search != null)
            {
                nkth = nkth.Where(c => c.NhatKyThuHoach.KhuVuc.SanPham.Contains(search));
            }
            return new PaginatedResponse<NhatKyBanSanPham>
            {
                Items = await PaginatedList<NhatKyBanSanPham>.Create(nkth, pageNumber, pageSize),
                PageIndex = pageNumber,
                TotalPages = (int)Math.Ceiling((double)nkth.Count() / pageSize)
            };
        }

        public async Task<NhatKyBanSanPhamRequest?> SuaNhatKyBanSanPham(int id, NhatKyBanSanPhamRequest request)
        {
            var nkbsp = await _dbContext.NhatKyBanSanPhams.FirstOrDefaultAsync(c => c.MaNhatKyBanSanPham == id);
            var nkth = await FindNKTH(nkbsp.MaNhatKyThuHoach);
            var slCon = nkth.SoLuongThuHoach - nkth.SoLuongDaBan;
            if(request.SoLuong <= slCon)
            {
                nkbsp.SoLuong = request.SoLuong;
                nkbsp.GiaBan = request.GiaBan;
                nkth.SoLuongDaBan = request.SoLuong;
                _dbContext.Update(nkbsp);
                await _dbContext.SaveChangesAsync();
                _dbContext.Update(nkth);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<NhatKyBanSanPhamRequest>(nkbsp);
            }
            return null;
        }

        public async Task<NhatKyBanSanPham?> ThemMoiNhatKyBanSanPham(NhatKyBanSanPhamRequest request)
        {
            var nkth = await _dbContext.NhatKyThuHoaches.Include(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).ThenInclude(c => c.CoSoNuoiTrong).FirstOrDefaultAsync(c => c.MaNhatKyThuHoach == request.MaNhatKyThuHoach);
            var nkbspAdd = _mapper.Map<NhatKyBanSanPham>(request);
            nkbspAdd.NgayBan = DateTime.Now;
            if (request.SoLuong > nkth.SoLuongThuHoach)
            {
                return null;
            }
            var content = $"Cơ sở nuôi trồng: {nkth.KhuVuc.NguoiDung.CoSoNuoiTrong.DiaChi}, Khu vực nuôi trồng : {nkth.KhuVuc.TenKhuVuc}, thời gian trồng : {nkth.KhuVuc.ThoiGianTao}, thời gian thu hoạch : {nkth.NgayThuHoach}";
            nkbspAdd.QRCode = CreateQRCode(content);
            nkth.SoLuongDaBan = request.SoLuong;
            _dbContext.Add(nkbspAdd);
            await _dbContext.SaveChangesAsync();
            _dbContext.Update(nkth);
            await _dbContext.SaveChangesAsync();
            return nkbspAdd;
        }

        public async Task<NhatKyBanSanPham?> Tim(Expression<Func<NhatKyBanSanPham, bool>>? filter = null)
        {
            return await _dbContext.NhatKyBanSanPhams.Include(c => c.NhatKyThuHoach).ThenInclude(c => c.KhuVuc).FirstOrDefaultAsync(filter);
        }

        public async Task<NhatKyBanSanPham?> XoaNhatKyBanSanPham(int id)
        {
            var nkbsp = await Tim(c => c.MaNhatKyBanSanPham == id);
            var nkth = await FindNKTH(nkbsp.MaNhatKyThuHoach);
            nkth.SoLuongDaBan -= nkbsp.SoLuong;
            if(nkth.SoLuongDaBan < 0)
            {
                nkth.SoLuongDaBan = 0;
            }
            _dbContext.Update(nkth);
            await _dbContext.SaveChangesAsync();
            _dbContext.NhatKyBanSanPhams.Remove(nkbsp);
            await _dbContext.SaveChangesAsync();
            return nkbsp;
        }

        public async Task<NhatKyThuHoach> FindNKTH(int id)
        {
            var nkth = await _dbContext.NhatKyThuHoaches.FirstOrDefaultAsync(c => c.MaNhatKyThuHoach == id);
            return nkth;
        }

        public string CreateQRCode(string content)
        {
            QRCodeGenerator QR = new QRCodeGenerator();
            QRCodeData QRData = QR.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode result = new QRCode(QRData);
            Bitmap bitmap = result.GetGraphic(20);
            Guid guid = Guid.NewGuid();
            string filePath = $"{guid}.png";
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "qrcode", filePath);
            bitmap.Save(path, ImageFormat.Png);
            return $"/qrcode/{filePath}";
        }

        public double TongTienBanSanPham(int csntId)
        {
            var nkbsp = _dbContext.NhatKyBanSanPhams.Include(c => c.NhatKyThuHoach).ThenInclude(c => c.KhuVuc).ThenInclude(c => c.NguoiDung).Where(c => c.NhatKyThuHoach.KhuVuc.NguoiDung.MaCoSoNuoiTrong == csntId).ToList();
            double tong = 0;
            foreach(var item in nkbsp)
            {
                tong += (item.SoLuong * item.GiaBan);
            }
            return tong;
        }
    }
}

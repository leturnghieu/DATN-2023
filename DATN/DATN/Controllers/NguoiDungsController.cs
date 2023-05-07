using AutoMapper;
using DATN.DTOs;
using DATN.Extentions;
using DATN.Services.Interfaces;
using DATN.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;

namespace DATN.Controllers
{
    public class NguoiDungsController : Controller
    {
        private readonly INguoiDung _nguoiDungService;
        private readonly IMapper _mapper;
        private readonly INhatKyMuaSam _nhatKyMuaSam;
        private readonly INhatKyBanSanPham _nhatKyBanSanPham;

        public NguoiDungsController(INguoiDung nguoiDung, IMapper mapper, INhatKyMuaSam nhatKyMuaSam, INhatKyBanSanPham nhatKyBanSanPham)
        {
            _nguoiDungService = nguoiDung;
            _mapper = mapper;
            _nhatKyMuaSam = nhatKyMuaSam;
            _nhatKyBanSanPham = nhatKyBanSanPham;
        }
        public async Task<IActionResult> DanhSachNguoiDung(string search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return View(await _nguoiDungService.DanhSachNguoiDung(search, pageIndex));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> DanhSachNguoiDungTheoCSNT(string search, int? pageIndex, int csntId)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return View("DanhSachNguoiDung", await _nguoiDungService.DanhSachNguoiDungTheoCSNT(search, pageIndex, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> ThemMoiNguoiDung()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewBag.MaCoSoNuoiTrong = new SelectList(await _nguoiDungService.DanhSachCoSoNuoiTrong(), "MaCoSoNuoiTrong", "DiaChi");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        [HttpPost]
        public async Task<IActionResult> ThemMoiNguoiDung(DangKyRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                if (ModelState.IsValid)
                {
                    var user = await _nguoiDungService.DangKy(request);
                    if (user == null)
                    {
                        ViewBag.Message = "Email đã tồn tại!";
                        ViewData["MaCoSoNuoiTrong"] = new SelectList(await _nguoiDungService.DanhSachCoSoNuoiTrong(), "MaCoSoNuoiTrong", "DiaChi");
                        return View();
                    }
                    ViewBag.Message = "Thêm người dùng thành công!";
                    return View("DanhSachNguoiDung", await _nguoiDungService.DanhSachNguoiDung());
                }
                ViewBag.Message = "Thêm mới thất bại thất bại!";
                ViewData["MaCoSoNuoiTrong"] = new SelectList(await _nguoiDungService.DanhSachCoSoNuoiTrong(), "MaCoSoNuoiTrong", "DiaChi");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetUserName() != null)
            {
                var user = await _nguoiDungService.Tim(x => x.MaNguoiDung == id);
                return View(_mapper.Map<NguoiDungRequest>(user));
            }
            return RedirectToAction("DangNhap");
        }
        [HttpPost]
        public async Task<IActionResult> Sua(int id, NguoiDungRequest request)
        {
            if (HttpContext.Session.GetUserName() != null)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var user = await _nguoiDungService.ChinhSua(id, request);
                ViewBag.MaNguoiDung = id;
                if (user == null)
                {
                    ViewBag.Message = "Lỗi chỉnh sửa";
                    return View();
                }
                ViewBag.Message = "Chỉnh sửa người dùng thành công!";
                return View("DanhSachNguoiDung", await _nguoiDungService.DanhSachNguoiDung());
            }
            return RedirectToAction("DangNhap");
        }

        public IActionResult ThemHinhAnh(int id)
        {
            if (HttpContext.Session.GetUserName() != null)
            {
                ViewBag.MaNguoiDung = id;
                return View();
            }
            return RedirectToAction("DangNhap");
        }

        [HttpPost]
        public async Task<IActionResult> ThemHinhAnh(int id, HinhAnhRequest request)
        {
            if (HttpContext.Session.GetUserName() != null)
            {
                var user = await _nguoiDungService.Tim(t => t.MaNguoiDung == id);
                ViewBag.MaNguoiDung = id;
                _nguoiDungService.ThemHinhAnh(id, request);
                HttpContext.Session.SetString("Avarta", user.HinhAnh);
                ViewBag.Message = "Chỉnh sửa hình ảnh thành công!";
                return View("ChiTiet", user);
            }
            return RedirectToAction("DangNhap");
        }
        public async Task<IActionResult> ChiTiet(int id)
        {
            if (HttpContext.Session.GetUserName() != null)
            {
                var user = await _nguoiDungService.Tim(x => x.MaNguoiDung == id);
                ViewBag.TongChiTieu = _nhatKyMuaSam.TongSoTienMuaSam(user.MaCoSoNuoiTrong);
                ViewBag.TongDoanhSo = _nhatKyBanSanPham.TongTienBanSanPham(user.MaCoSoNuoiTrong);
                return View(user);
            }
            return RedirectToAction("DangNhap");
        }
        public IActionResult ExportToExcel()
        {
            var data = _nguoiDungService.NguoiDungs();
            using var package = new ExcelPackage();

            // Add a new worksheet to the package
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

            // Add headers to the worksheet
            worksheet.Cells[1, 1].Value = "Họ và tên";
            worksheet.Cells[1, 2].Value = "Email";
            worksheet.Cells[1, 3].Value = "Giới tính";
            worksheet.Cells[1, 4].Value = "Số điện thoại";
            worksheet.Cells[1, 5].Value = "Địa chỉ cơ sở nuôi trồng";
            worksheet.Cells[1, 6].Value = "Diện tích cơ sở nuôi trồng";
            worksheet.Cells[1, 7].Value = "Diện tích đã sử dụng";
            worksheet.Cells[1, 8].Value = "Tổng số tiền chi tiêu";
            worksheet.Cells[1, 9].Value = "Tổng doanh số";

            // Add data to the worksheet
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = data[i].TenNguoiDung;
                worksheet.Cells[i + 2, 2].Value = data[i].Email;
                worksheet.Cells[i + 2, 3].Value = data[i].GioiTinh;
                worksheet.Cells[i + 2, 4].Value = data[i].SDT;
                worksheet.Cells[i + 2, 5].Value = data[i].CoSoNuoiTrong.DiaChi;
                worksheet.Cells[i + 2, 6].Value = data[i].CoSoNuoiTrong.DienTich;
                worksheet.Cells[i + 2, 7].Value = data[i].CoSoNuoiTrong.DienTichDaSuDung;
                worksheet.Cells[i + 2, 8].Value = _nguoiDungService.TongChiPhi(data[i].MaCoSoNuoiTrong);
                worksheet.Cells[i + 2, 9].Value = _nguoiDungService.TongDoanhSo(data[i].MaCoSoNuoiTrong);

            }

            // Set the content type and file name for the response
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "danhsachnguoidung.xlsx";

            // Return the Excel file as a download
            return File(package.GetAsByteArray(), contentType, fileName);
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nguoiDung = await _nguoiDungService.Tim(x => x.MaNguoiDung == id);
                return View("Xoa", nguoiDung);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost(), ActionName("Xoa")]
        public async Task<IActionResult> XacNhanXoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nguoiDung = await _nguoiDungService.Tim(c => c.MaNguoiDung == id);
                if (nguoiDung.KhuVucs.Count == 0 && nguoiDung.NhatKyMuaSams.Count == 0)
                {
                    await _nguoiDungService.XoaNguoiDung(id);
                    ViewBag.Message = "Xóa người dùng thành công!";
                    return View("DanhSachNguoiDung", await _nguoiDungService.DanhSachNguoiDung());
                }
                ViewBag.Message = "Xóa người dùng thất bại!";
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }
    }
}

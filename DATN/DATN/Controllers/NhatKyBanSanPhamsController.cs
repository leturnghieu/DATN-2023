using AutoMapper;
using DATN.DTOs;
using DATN.Extentions;
using DATN.Services.Interfaces;
using DATN.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Helpers;

namespace DATN.Controllers
{
    public class NhatKyBanSanPhamsController : Controller
    {
        private readonly INhatKyBanSanPham _nhatKyBanSanPhamService;
        private readonly IMapper _mapper;

        public NhatKyBanSanPhamsController(INhatKyBanSanPham nhatKyBanSanPham, IMapper mapper)
        {
            _nhatKyBanSanPhamService = nhatKyBanSanPham;
            _mapper = mapper;
        }
        public async Task<IActionResult> DanhSachNhatKyBanSanPham(string search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nkbsp = await _nhatKyBanSanPhamService.DanhSachNhatKyBanSanPham(search, pageIndex, csntId);
                return View(nkbsp);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> ThemMoiNhatKyBanSanPham()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                ViewBag.MaNhatKyThuHoach = new SelectList(await _nhatKyBanSanPhamService.DanhSachNhatKyThuHoach(userId), "MaNhatKyThuHoach", "KhuVuc.SanPham");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        [HttpPost]
        public async Task<IActionResult> ThemMoiNhatKyBanSanPham(NhatKyBanSanPhamRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nkbsp = await _nhatKyBanSanPhamService.ThemMoiNhatKyBanSanPham(request);
                if(nkbsp == null)
                {
                    var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                    ViewBag.Message = "Thêm thất bại, do số lượng bán lớn hơn trong kho";
                    ViewBag.MaNhatKyThuHoach = new SelectList(await _nhatKyBanSanPhamService.DanhSachNhatKyThuHoach(userId), "MaNhatKyThuHoach", "KhuVuc.SanPham");
                    return View();
                }
                ViewBag.Message = "Thêm nhật ký bán sản phẩm thành công!";
                return View("DanhSachNhatKyBanSanPham", await _nhatKyBanSanPhamService.DanhSachNhatKyBanSanPham(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nkbsp = await _nhatKyBanSanPhamService.Tim(c => c.MaNhatKyBanSanPham == id);
                return View(_mapper.Map<NhatKyBanSanPhamRequest>(nkbsp));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> Sua(int id, NhatKyBanSanPhamRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                if (ModelState.IsValid)
                {
                    var nkbsp = await _nhatKyBanSanPhamService.SuaNhatKyBanSanPham(id, request);
                    if (nkbsp == null)
                    {
                        ViewBag.Message = "Số lượng không hợp lên vì nhỏ hơn số lượng bán";
                        return View();
                    }
                    ViewBag.Message = "Sửa nhật ký bán sản phẩm thành công!";
                    return View("DanhSachNhatKyBanSanPham", await _nhatKyBanSanPhamService.DanhSachNhatKyBanSanPham(null, 1, csntId));
                }
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nkbsp = await _nhatKyBanSanPhamService.Tim(x => x.MaNhatKyBanSanPham == id);
                return View("Xoa", nkbsp);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost(), ActionName("Xoa")]
        public async Task<IActionResult> XacNhanXoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                await _nhatKyBanSanPhamService.XoaNhatKyBanSanPham(id);
                ViewBag.Message = "Xóa nhật ký bán sản phẩm thành công!";
                return View("DanhSachNhatKyBanSanPham", await _nhatKyBanSanPhamService.DanhSachNhatKyBanSanPham(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        [HttpGet]
        public async Task<IActionResult> NhatKyThuHoach(int maNKTH)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nkth = await _nhatKyBanSanPhamService.FindNKTH(maNKTH);
                return Json(nkth);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
    }
}

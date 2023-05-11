using DATN.DTOs;
using DATN.Models;
using DATN.Services.Interfaces;
using DATN.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Controllers
{
    public class KhoVatTusController : Controller
    {
        private readonly IKhoVatTu _khoService;

        public KhoVatTusController(IKhoVatTu khoService)
        {
            _khoService = khoService;
        }
        public async Task<IActionResult> DanhSachKhoVatTu(string? search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var listKho = await _khoService.DanhSachKhoVatTu(search, pageIndex, csntId);
                return View(listKho);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        
        public  IActionResult ThemMoiKhoVatTu()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> ThemMoiKhoVatTu(KhoVatTuRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var khoVatTu = await _khoService.ThemMoiKhoVatTu(request);
                if(khoVatTu == null)
                {
                    ViewBag.Message = "Tên kho đã tồn tại!";
                    return View();
                }
                ViewBag.Message = "Thêm kho mới thành công!";
                return View("DanhSachKhoVatTu", await _khoService.DanhSachKhoVatTu(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var kho = await _khoService.Tim(c => c.MaKhoVatTu == id);
                if (kho == null)
                {
                    ViewBag.Message = "Kho vật tư đã bị xóa!";
                    return NotFound();
                }
                return View("Sua", kho);

            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> Sua(int id, KhoVatTu request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var kho = await _khoService.SuaKhoVatTu(id, request);
                if (kho == null)
                {
                    ViewBag.Message = "Kho vật tư đã bị xóa!";
                    return NotFound();
                }
                ViewBag.Message = "Sửa kho thành công!";
                return View("DanhSachKhoVatTu", await _khoService.DanhSachKhoVatTu(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var kho = await _khoService.Tim(x => x.MaKhoVatTu == id);
                return View("Xoa", kho);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost(), ActionName("Xoa")]
        public async Task<IActionResult> XacNhanXoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var kho = await _khoService.XoaKhoVatTu(id);
                if (kho == null)
                {
                    ViewBag.Message = "Xóa kho vật tư thất bại!";
                    return View();
                }
                ViewBag.Message = "Xóa kho vật thành công!";
                return View("DanhSachKhoVatTu", await _khoService.DanhSachKhoVatTu(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }
    }
}

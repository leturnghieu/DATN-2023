using DATN.DTOs;
using DATN.Extentions;
using DATN.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using DATN.Models;

namespace DATN.Controllers
{
    public class NhatKyMuaSamsController : Controller
    {
        private readonly INhatKyMuaSam _nhatKyMuaSamService;

        public NhatKyMuaSamsController(INhatKyMuaSam nhatKyMuaSam)
        {
            _nhatKyMuaSamService = nhatKyMuaSam;
        }
        public async Task<IActionResult> DanhSachNhatKyMuaSam(string search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nkms = await _nhatKyMuaSamService.DanhSachNhatKyMuaSam(search, pageIndex, csntId);
                return View(nkms);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        public async Task<IActionResult> DanhSachVatTuHetHan(string search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nkms = await _nhatKyMuaSamService.DanhSachVatTuHetHan(search, pageIndex, csntId);
                return View("DanhSachNhatKyMuaSam", nkms);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        public async Task<IActionResult> ThemMoiNhatKyMuaSam()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewData["MaLoai"] = new SelectList(await _nhatKyMuaSamService.DanhSachLoaiVatTu(), "MaLoai", "TenLoai");
                ViewData["MaKho"] = new SelectList(await _nhatKyMuaSamService.DanhSachKhoVatTu(), "MaKhoVatTu", "TenKho");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> ThemMoiNhatKyMuaSam(NhatKyMuaSamRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                if(ModelState.IsValid)
                {
                    var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                    var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                    await _nhatKyMuaSamService.ThemMoiNhatKyMuaSam(userId, request);
                    ViewBag.Message = "Thêm nhật ký mua sắm mới thành công!";
                    return View("DanhSachNhatKyMuaSam", await _nhatKyMuaSamService.DanhSachNhatKyMuaSam(null, 1, csntId));
                }
                ViewData["MaLoai"] = new SelectList(await _nhatKyMuaSamService.DanhSachLoaiVatTu(), "MaLoai", "TenLoai");
                ViewData["MaKho"] = new SelectList(await _nhatKyMuaSamService.DanhSachKhoVatTu(), "MaKhoVatTu", "TenKho");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }
       
        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nkms = await _nhatKyMuaSamService.Tim(c => c.MaNhatKyMuaSam == id);
                ViewData["MaLoai"] = new SelectList(await _nhatKyMuaSamService.DanhSachLoaiVatTu(), "MaLoai", "TenLoai");
                ViewData["MaKho"] = new SelectList(await _nhatKyMuaSamService.DanhSachKhoVatTu(), "MaKhoVatTu", "TenKho");
                return View(nkms);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> Sua(int id, NhatKyMuaSam request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nkms = await _nhatKyMuaSamService.SuaNhatKyMuaSam(id, request);
                if(nkms == null)
                {
                    ViewBag.Message = "Sửa nhật ký mua sắm thất bại!";
                }
                ViewBag.Message = "Sửa nhật ký mua sắm thành công!";
                return View("DanhSachNhatKyMuaSam", await _nhatKyMuaSamService.DanhSachNhatKyMuaSam(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var kho = await _nhatKyMuaSamService.Tim(x => x.MaNhatKyMuaSam == id);
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
                var kho = await _nhatKyMuaSamService.XoaNhatKyMuaSam(id);
                if (kho == null)
                {
                    ViewBag.Message = "Xóa nhật ký mua sắm thất bại!";
                    return View();
                }
                ViewBag.Message = "Xóa nhật ký mua sắm thành công!";
                return View("DanhSachNhatKyMuaSam", await _nhatKyMuaSamService.DanhSachNhatKyMuaSam(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }
    }
}

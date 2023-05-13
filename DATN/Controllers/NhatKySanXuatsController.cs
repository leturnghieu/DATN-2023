using AutoMapper;
using DATN.DTOs;
using DATN.Extentions;
using DATN.Models;
using DATN.Services.Interfaces;
using DATN.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DATN.Controllers
{
    public class NhatKySanXuatsController : Controller
    {
        private readonly INhatKySanXuat _nhatKySanXuatService;
        private readonly IMapper _mapper;

        public NhatKySanXuatsController(INhatKySanXuat nhatKySanXuat, IMapper mapper)
        {
            _nhatKySanXuatService = nhatKySanXuat;
            _mapper = mapper;
        }
        public async Task<IActionResult> DanhSachNhatKySanXuat(string search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nksx = await _nhatKySanXuatService.DanhSachNhatKySanXuat(search, pageIndex, csntId);
                return View(nksx);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByWarehouse(int warehouse)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var products = await _nhatKySanXuatService.DanhSachVatTu(warehouse, csntId);
                return Json(products);
            }
            return RedirectToAction("TrangChu", "Auths");
            
        }

        public async Task<IActionResult> ThemMoiNhatKySanXuat()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                ViewBag.MaKho = new SelectList(await _nhatKySanXuatService.DanhSachKho(), "MaKhoVatTu", "TenKho");
                ViewBag.MaKhuVuc = new SelectList(await _nhatKySanXuatService.DanhSachKhuVuc(userId), "MaKhuVuc", "TenKhuVuc");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> ThemMoiNhatKySanXuat(NhatKySanXuatRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                if (ModelState.IsValid)
                {
                    var nksx = await _nhatKySanXuatService.ThemMoiNhatKySanXuat(request, csntId);
                    if(nksx == null)
                    {
                        ViewBag.Message = "Số lượng nhập không hợp lệ!";
                        
                        ViewBag.MaKho = new SelectList(await _nhatKySanXuatService.DanhSachKho(), "MaKhoVatTu", "TenKho");
                        ViewBag.MaKhuVuc = new SelectList(await _nhatKySanXuatService.DanhSachKhuVuc(userId), "MaKhuVuc", "TenKhuVuc");
                        return View();
                    }
                    ViewBag.Message = "Thêm mới thành công nhật ký sản xuất!";
                    return View("DanhSachNhatKySanXUat", await _nhatKySanXuatService.DanhSachNhatKySanXuat(null, 1, csntId));
                }
                ViewBag.MaKho = new SelectList(await _nhatKySanXuatService.DanhSachKho(), "MaKhoVatTu", "TenKho");
                ViewBag.MaKhuVuc = new SelectList(await _nhatKySanXuatService.DanhSachKhuVuc(userId), "MaKhuVuc", "TenKhuVuc");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var kho = await _nhatKySanXuatService.Tim(x => x.MaNhatKySanXuat == id);
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
                var kho = await _nhatKySanXuatService.XoaNhatKySanXuat(id, csntId);
                if (kho == null)
                {
                    ViewBag.Message = "Xóa nhật ký sản xuất thất bại!";
                    return View();
                }
                ViewBag.Message = "Xóa nhật ký sản xuất thành công!";
                return View("DanhSachNhatKySanXuat", await _nhatKySanXuatService.DanhSachNhatKySanXuat(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nkms = await _nhatKySanXuatService.Tim(c => c.MaNhatKySanXuat == id);
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                ViewBag.MaKho = new SelectList(await _nhatKySanXuatService.DanhSachKho(), "MaKhoVatTu", "TenKho");
                ViewBag.MaKhuVuc = new SelectList(await _nhatKySanXuatService.DanhSachKhuVuc(userId), "MaKhuVuc", "TenKhuVuc");
                return View(_mapper.Map<NhatKySanXuatEditRequest>(nkms));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> Sua(int id, NhatKySanXuatEditRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                if (ModelState.IsValid)
                {
                    var nkms = await _nhatKySanXuatService.SuaNhatKySanXuat(id, request);
                    if (nkms == null)
                    {
                        ViewBag.Message = "Sửa nhật ký sản xuất thất bại!";
                    }
                    ViewBag.Message = "Sửa nhật ký sản xuất thành công!";
                    return View("DanhSachNhatKySanXuat", await _nhatKySanXuatService.DanhSachNhatKySanXuat(null, 1, csntId));
                }
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                ViewBag.MaKho = new SelectList(await _nhatKySanXuatService.DanhSachKho(), "MaKhoVatTu", "TenKho");
                ViewBag.MaKhuVuc = new SelectList(await _nhatKySanXuatService.DanhSachKhuVuc(userId), "MaKhuVuc", "TenKhuVuc");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }
    }
}

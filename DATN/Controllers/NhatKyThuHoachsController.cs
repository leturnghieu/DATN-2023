using AutoMapper;
using DATN.DTOs;
using DATN.Extentions;
using DATN.Services.Interfaces;
using DATN.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DATN.Controllers
{
    public class NhatKyThuHoachsController : Controller
    {
        private readonly INhatKyThuHoach _nhatKyThuHoachService;
        private readonly IMapper _mapper;

        public NhatKyThuHoachsController(INhatKyThuHoach nhatKyThuHoach, IMapper mapper)
        {
            _nhatKyThuHoachService = nhatKyThuHoach;
            _mapper = mapper;
        }
        public async Task<ActionResult> DanhSachNhatKyThuHoach(string search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nkth = await _nhatKyThuHoachService.DanhSachNhatKyThuHoach(search, pageIndex, csntId);
                return View(nkth);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        public async Task<ActionResult> ThemMoiNhatKyThuHoach()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                ViewBag.MaKhuVuc = new SelectList(await _nhatKyThuHoachService.DanhSachKhuVuc(userId), "MaKhuVuc", "TenKhuVuc");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> ThemMoiNhatKyThuHoach(NhatKyThuHoachRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                if (ModelState.IsValid)
                {
                    await _nhatKyThuHoachService.ThemMoiNhatKyThuHoach(request);
                    return View("DanhSachNhatKyThuHoach", await _nhatKyThuHoachService.DanhSachNhatKyThuHoach(null, 1, csntId));
                }
                ViewBag.MaKhuVuc = new SelectList(await _nhatKyThuHoachService.DanhSachKhuVuc(userId), "MaKhuVuc", "TenKhuVuc");
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nkth = await _nhatKyThuHoachService.Tim(c => c.MaNhatKyThuHoach == id);
                return View(_mapper.Map<NhatKyThuHoachEditRequest>(nkth));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> Sua(int id, NhatKyThuHoachEditRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                if (ModelState.IsValid)
                {
                    var nkth =  await _nhatKyThuHoachService.SuaNhatKyThuHoach(id, request);
                    if(nkth == null)
                    {
                        ViewBag.Message = "Số lượng không hợp lên vì nhỏ hơn số lượng bán";
                        return View();
                    }    
                    ViewBag.Message = "Sửa nhật ký thu hoạch thành công!";
                    return View("DanhSachNhatKyThuHoach", await _nhatKyThuHoachService.DanhSachNhatKyThuHoach(null, 1, csntId));
                }
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var nkth = await _nhatKyThuHoachService.Tim(x => x.MaNhatKyThuHoach == id);
                return View("Xoa", nkth);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost(), ActionName("Xoa")]
        public async Task<IActionResult> XacNhanXoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var nkth = await _nhatKyThuHoachService.XoaNhatKyThuHoach(id);
                if (nkth == null)
                {
                    ViewBag.Message = "Xóa nhật ký thu hoạch thất bại!";
                    return View();
                }
                ViewBag.Message = "Xóa nhật ký thu hoạch thành công!";
                return View("DanhSachNhatKyThuHoach", await _nhatKyThuHoachService.DanhSachNhatKyThuHoach(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }
    }
}

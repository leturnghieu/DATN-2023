using AutoMapper;
using DATN.DTOs;
using DATN.Extentions;
using DATN.Models;
using DATN.Services.Interfaces;
using DATN.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Controllers
{
    public class CoSoNuoiTrongsController : Controller
    {
        private readonly ICoSoNuoiTrong _coSoNuoiTrongService;
        private readonly IMapper _mapper;

        public CoSoNuoiTrongsController(ICoSoNuoiTrong coSoNuoiTrongService, IMapper mapper)
        {
            _coSoNuoiTrongService = coSoNuoiTrongService;
            _mapper = mapper;
        }
        public async Task<ActionResult<PaginatedResponse<KhuVuc>>> DanhSachCoSoNuoiTrong(string search, int? pageIndex)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csnt = await _coSoNuoiTrongService.DanhSachCoSoNuoiTrong(search, pageIndex);
                return View(csnt);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        public async Task<IActionResult> ThemMoiCoSoNuoiTrong()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return View();
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        [HttpPost]
        public async Task<IActionResult> ThemMoiCoSoNuoiTrong(CoSoNuoiTrongRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var khuVuc = await _coSoNuoiTrongService.ThemMoiCoSoNuoiTrong(request);
                if (khuVuc == null)
                {
                    ViewBag.Message = "Thêm mới cơ sở nuôi trồng thất bại!";
                    return View("ThemMoiCoSoNuoiTrong");
                }
                ViewBag.Message = "Thêm mới cơ sở nuôi trồng thành công!";
                return View("DanhSachCoSoNuoiTrong", await _coSoNuoiTrongService.DanhSachCoSoNuoiTrong());

            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csnt = await _coSoNuoiTrongService.Tim(c => c.MaCoSoNuoiTrong == id);
                if (csnt == null)
                {
                    return NotFound();
                }
                return View("Sua", _mapper.Map<CoSoNuoiTrongRequest>(csnt));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> Sua(int id, CoSoNuoiTrongRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csnt = await _coSoNuoiTrongService.SuaCoSoNuoiTrong(id, request);
                if (csnt == null)
                {
                    ViewBag.Message = "Không tìm thấy cơ sở nuôi trồng trong hệ thống!";
                    return NotFound();
                }
                ViewBag.Message = "Sửa cơ sở nuôi trồng thành công!";
                return View("DanhSachCoSoNuoiTrong", await _coSoNuoiTrongService.DanhSachCoSoNuoiTrong());
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csnt = await _coSoNuoiTrongService.Tim(x => x.MaCoSoNuoiTrong == id);
                return View("Xoa", csnt);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost(), ActionName("Xoa")]
        public async Task<IActionResult> XacNhanXoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csnt = await _coSoNuoiTrongService.XoaCoSoNuoiTrong(id);
                if (csnt == null)
                {
                    ViewBag.Message = "Xóa cơ sở nuôi trồng thất bại do có người đang hoạt động!";
                    return View();
                }
                ViewBag.Message = "Xóa cơ sở nuôi trồng thành công!";
                return View("DanhSachCoSoNuoiTrong", await _coSoNuoiTrongService.DanhSachCoSoNuoiTrong());
            }
            return RedirectToAction("TrangChu", "Auths");
        }
    }
}

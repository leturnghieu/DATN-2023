using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATN.Models;
using DATN.Services.Interfaces;
using DATN.DTOs;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using static DATN.Models.Enum;
using DATN.Extentions;
using System.Security.Claims;
using DATN.Services.Service;
using AutoMapper;

namespace DATN.Controllers
{
    public class AuthsController : Controller
    {
        private readonly INguoiDung _nguoiDungService;
        private readonly IChatGPT _chatGPTService;
        private readonly IMapper _mapper;
        private readonly INhatKyMuaSam _nhatKyMuaSam;
        private readonly INhatKyBanSanPham _nhatKyBanSanPham;
        private readonly IKhuVuc _khuVuc;

        public AuthsController(INguoiDung nguoiDungService, IChatGPT ChatGPTService, IMapper mapper,
            INhatKyMuaSam nhatKyMuaSam,
            INhatKyBanSanPham nhatKyBanSanPham,
            IKhuVuc khuVuc)
        {
            _nguoiDungService = nguoiDungService;
            _chatGPTService = ChatGPTService;
            _mapper = mapper;
            _nhatKyMuaSam = nhatKyMuaSam;
            _nhatKyBanSanPham = nhatKyBanSanPham;
            _khuVuc = khuVuc;
        }

        // GET: Auths
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DangKy(DangKyDangNhapRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = await _nguoiDungService.DangKy(request.DangKyRequest);
                if (user == null)
                {
                    ViewBag.Message = "Email đã tồn tại!";
                    ViewData["MaCoSoNuoiTrong"] = new SelectList(await _nguoiDungService.DanhSachCoSoNuoiTrong(), "MaCoSoNuoiTrong", "DiaChi");
                    return View("DangNhap");
                }
                ViewBag.Message = "Đăng ký thành công!";
                return View("DangNhap");
            }
            ViewBag.Message = "Đăng ký thất bại!";
            ViewData["MaCoSoNuoiTrong"] = new SelectList(await _nguoiDungService.DanhSachCoSoNuoiTrong(), "MaCoSoNuoiTrong", "DiaChi");
            return View("DangNhap");
        }
        public async Task<IActionResult> DangNhap()
        {
            ViewData["MaCoSoNuoiTrong"] = new SelectList(await _nguoiDungService.DanhSachCoSoNuoiTrong(), "MaCoSoNuoiTrong", "DiaChi");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangNhap(DangKyDangNhapRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var user = await _nguoiDungService.DangNhap(request.DangNhapRequest);
                if (user == null)
                {
                    ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không chính xác !";
                    return View();
                }
                HttpContext.Session.SetString("UserName", user.TenNguoiDung);
                if(user.HinhAnh != null)
                {
                    HttpContext.Session.SetString("Avarta", user.HinhAnh);
                }
                HttpContext.Session.SetInt32("CsntId", user.MaCoSoNuoiTrong);
                HttpContext.Session.SetInt32("Role", (int)user.Roles);
                HttpContext.Session.SetInt32("UserId", user.MaNguoiDung);
                if (user.Roles == 0)
                {
                    HttpContext.Session.SetString("UserName", user.TenNguoiDung);
                    if (user.HinhAnh != null)
                    {
                        HttpContext.Session.SetString("Avarta", user.HinhAnh);
                    }
                    HttpContext.Session.SetInt32("Role", (int)user.Roles);
                    HttpContext.Session.SetInt32("UserId", user.MaNguoiDung);
                    ViewBag.Message = "Đăng nhập vào trang ADMIN thành công";
                    return View("TrangChuAdmin");
                }
                ViewBag.Message = "Đăng nhập thành công!";
                var csntId = HttpContext.Session.GetCsntId().GetValueOrDefault();
                ViewBag.TongKhuVuc = _khuVuc.TongKhuVuc(csntId);
                ViewBag.TongTienMuaSam = _nhatKyMuaSam.TongSoTienMuaSam(csntId);
                ViewBag.TongTienBanSanPham = _nhatKyBanSanPham.TongTienBanSanPham(csntId);
                ViewBag.TongVatTuHetHan = _nhatKyMuaSam.TongSoVatTuHetHan(csntId);
                return View("TrangChu");
            }
        }
        public async Task<IActionResult> DangXuat()
        {
            HttpContext.Session.Clear();
            ViewData["MaCoSoNuoiTrong"] = new SelectList(await _nguoiDungService.DanhSachCoSoNuoiTrong(), "MaCoSoNuoiTrong", "DiaChi");
            return View("DangNhap");
        }
        public IActionResult TrangChu()
        {
            if(HttpContext.Session.GetUserName() != null)
            {
                var csntId = HttpContext.Session.GetCsntId().GetValueOrDefault();
                ViewBag.TongKhuVuc = _khuVuc.TongKhuVuc(csntId);
                ViewBag.TongTienMuaSam = _nhatKyMuaSam.TongSoTienMuaSam(csntId);
                ViewBag.TongTienBanSanPham = _nhatKyBanSanPham.TongTienBanSanPham(csntId);
                ViewBag.TongVatTuHetHan = _nhatKyMuaSam.TongSoVatTuHetHan(csntId);
                return View();
            }
            return RedirectToAction("DangNhap");
        }

        [HttpPost]
        public async Task<IActionResult> TrangChu(string? input)
        {
            return View("TrangChu", await _chatGPTService.Ask(input));
        }
        public async Task<IActionResult> ChiTiet(int id)
        {
            if(HttpContext.Session.GetUserName() != null)
            {
                var user = await _nguoiDungService.Tim(x => x.MaNguoiDung == id);
                return View(user);
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
                ViewBag.Message = "Thêm hình ảnh thành công!";
                return View("ChiTiet", user);
            }
            return RedirectToAction("DangNhap");
        }
        public async Task<IActionResult> ChinhSua(int id)
        {
            if (HttpContext.Session.GetUserName() != null)
            {
                var user = await _nguoiDungService.Tim(x => x.MaNguoiDung == id);
                return View(_mapper.Map<NguoiDungRequest>(user));
            }
            return RedirectToAction("DangNhap");
        }
        [HttpPost]
        public async Task<IActionResult> ChinhSua(int id, NguoiDungRequest request)
        {
            if (HttpContext.Session.GetUserName() != null)
            {
                if(!ModelState.IsValid)
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
                return View("ChiTiet", await _nguoiDungService.Tim(c => c.MaNguoiDung == id));
            }
            return RedirectToAction("DangNhap");
        }

        public IActionResult TrangChuAdmin()
        {
            return View("TrangChuAdmin");
        }
    }
}
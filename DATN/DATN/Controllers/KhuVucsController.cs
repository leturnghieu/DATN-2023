using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATN.Models;
using Microsoft.AspNetCore.Authorization;
using DATN.DTOs;
using DATN.Extentions;
using DATN.Services.Interfaces;

namespace DATN.Controllers
{
    public class KhuVucsController : Controller
    {
        private readonly IKhuVuc _khuVucService;
        public KhuVucsController(IKhuVuc khuVuc)
        {
            _khuVucService = khuVuc;
        }

        // GET: KhuVucs
        public async Task<ActionResult<PaginatedResponse<KhuVuc>>> DanhSachKhuVuc(string search, int? pageIndex)
        {
            if(HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetInt32("CsntId").GetValueOrDefault();
                var listKhuVuc = await _khuVucService.DanhSachKhuVuc(search, pageIndex, csntId);
                return View(listKhuVuc);
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        public async Task<IActionResult> ThemMoiKhuVuc()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                return View();
            }
            return RedirectToAction("TrangChu", "Auths"); 
        }
        [HttpPost]
        public async Task<IActionResult> ThemMoiKhuVuc(KhuVucRequest request)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var userId = HttpContext.Session.GetUserId().GetValueOrDefault();
                var csntId = HttpContext.Session.GetCsntId().GetValueOrDefault();
                var khuVuc = await _khuVucService.TaoKhuVuc(request, userId);
                if(khuVuc == null)
                {
                    ViewBag.Message = "Tạo khu vực không thất bại do diện tích lớn hơn diện tích cơ sở!";
                    return View("ThemMoiKhuVuc");
                }
                ViewBag.Message = "Tạo khu vực thành công!";
                return View("DanhSachKhuVuc", await _khuVucService.DanhSachKhuVuc(null, 1, csntId));

            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Sua(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var khuVuc = await _khuVucService.Tim(c => c.MaKhuVuc == id);
                if(khuVuc == null)
                {
                    return NotFound();
                }
                return View("Sua", khuVuc);

            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost]
        public async Task<IActionResult> Sua(int id, KhuVuc request, IFormFile fileAnh)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetCsntId().GetValueOrDefault();
                var khuVuc = await _khuVucService.SuaKhuVuc(id, request, fileAnh);
                if (khuVuc == null)
                {
                    return NotFound();
                }
                return View("DanhSachKhuVuc", await _khuVucService.DanhSachKhuVuc(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        public async Task<IActionResult> Xoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var khuVuc = await _khuVucService.Tim(x => x.MaKhuVuc == id);
                return View("Xoa", khuVuc);
            }
            return RedirectToAction("TrangChu", "Auths");
        }

        [HttpPost(), ActionName("Xoa")]
        public async Task<IActionResult> XacNhanXoa(int id)
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                var csntId = HttpContext.Session.GetCsntId().GetValueOrDefault();
                var khuVuc = await _khuVucService.Tim(c => c.MaKhuVuc == id);
                if(khuVuc.NhatKySanXuats.Count > 0 || khuVuc.NhatKyThuHoachs.Count > 0)
                {
                    ViewBag.Message = "Xóa khu vực thất bại do khu vực đang vận hành!";
                    return View();
                }    
                await _khuVucService.XoaKhuVuc(id);
                ViewBag.Message = "Xóa khu vực thành công!";
                return View("DanhSachKhuVuc", await _khuVucService.DanhSachKhuVuc(null, 1, csntId));
            }
            return RedirectToAction("TrangChu", "Auths");
        }
        /* [HttpPost]
         [Authorize]
         public async Task<IActionResult> Create(KhuVucRequest request)
         {

             if (ModelState.IsValid)
             {
                 var userID = HttpContext.User.GetUserId();
                 var khuVuc = await _khuVucService.TaoKhuVuc(request, userID);
                 if (khuVuc == null)
                 {
                     ViewBag.Message = "Diện tích phải nhỏ hơn hoặc bằng diện tích cơ sở!";
                     return View();
                 }
                 return RedirectToAction("Index");
             }
             ViewData["MaSanPham"] = new SelectList(await _khuVucService.DanhSachSanPham(), "MaSanPham", "TenSanPham");
             return View();
         }*/
    }
}
        // GET: KhuVucs/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KhuVucs == null)
            {
                return NotFound();
            }

            var khuVuc = await _context.KhuVucs
                .Include(k => k.NguoiDung)
                .Include(k => k.SanPham)
                .FirstOrDefaultAsync(m => m.MaKhuVuc == id);
            if (khuVuc == null)
            {
                return NotFound();
            }

            return View(khuVuc);
        }*/

        // GET: KhuVucs/Create


        // POST: KhuVucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        

        // GET: KhuVucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KhuVucs == null)
            {
                return NotFound();
            }

            var khuVuc = await _context.KhuVucs.FindAsync(id);
            if (khuVuc == null)
            {
                return NotFound();
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "Email", khuVuc.MaNguoiDung);
            ViewData["MaSanPham"] = new SelectList(_context.SanPhams, "MaSanPham", "TenSanPham", khuVuc.MaSanPham);
            return View(khuVuc);
        }

        // POST: KhuVucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKhuVuc,TenKhuVuc,DienTich,MaNguoiDung,MaSanPham")] KhuVuc khuVuc)
        {
            if (id != khuVuc.MaKhuVuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khuVuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhuVucExists(khuVuc.MaKhuVuc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaNguoiDung"] = new SelectList(_context.NguoiDungs, "MaNguoiDung", "Email", khuVuc.MaNguoiDung);
            ViewData["MaSanPham"] = new SelectList(_context.SanPhams, "MaSanPham", "TenSanPham", khuVuc.MaSanPham);
            return View(khuVuc);
        }

        // GET: KhuVucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KhuVucs == null)
            {
                return NotFound();
            }

            var khuVuc = await _context.KhuVucs
                .Include(k => k.NguoiDung)
                .Include(k => k.SanPham)
                .FirstOrDefaultAsync(m => m.MaKhuVuc == id);
            if (khuVuc == null)
            {
                return NotFound();
            }

            return View(khuVuc);
        }

        // POST: KhuVucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KhuVucs == null)
            {
                return Problem("Entity set 'MasterDbContext.KhuVucs'  is null.");
            }
            var khuVuc = await _context.KhuVucs.FindAsync(id);
            if (khuVuc != null)
            {
                _context.KhuVucs.Remove(khuVuc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhuVucExists(int id)
        {
          return (_context.KhuVucs?.Any(e => e.MaKhuVuc == id)).GetValueOrDefault();
        }
    }
}
*/
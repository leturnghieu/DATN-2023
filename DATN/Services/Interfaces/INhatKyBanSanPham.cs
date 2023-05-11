using DATN.DTOs;
using DATN.Models;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface INhatKyBanSanPham
    {
        Task<PaginatedResponse<NhatKyBanSanPham>> DanhSachNhatKyBanSanPham(string? search = null, int? pageIndex = 1, int? maCSNT = null);
        Task<NhatKyBanSanPham?> ThemMoiNhatKyBanSanPham(NhatKyBanSanPhamRequest request);
        Task<NhatKyBanSanPhamRequest?> SuaNhatKyBanSanPham(int id, NhatKyBanSanPhamRequest request);
        Task<NhatKyBanSanPham?> Tim(Expression<Func<NhatKyBanSanPham, bool>>? filter = null);
        Task<NhatKyBanSanPham?> XoaNhatKyBanSanPham(int id);
        Task<IEnumerable> DanhSachNhatKyThuHoach(int id);
        Task<NhatKyThuHoach> FindNKTH(int id);
        double TongTienBanSanPham(int csntId);
    }
}

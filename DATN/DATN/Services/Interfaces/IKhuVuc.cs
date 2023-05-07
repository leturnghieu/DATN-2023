using DATN.DTOs;
using DATN.Models;
using PagedList;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface IKhuVuc
    {
        Task<KhuVuc?> TaoKhuVuc(KhuVucRequest request, int nguoiDung);
        Task<PaginatedResponse<KhuVuc>> DanhSachKhuVuc(string? search = null, int? pageIndex = 1, int? maCSNT = null);
        Task<KhuVuc?> SuaKhuVuc(int id, KhuVuc request, IFormFile fileAnh);
        Task<KhuVuc?> Tim(Expression<Func<KhuVuc, bool>>? filter = null);
        Task<KhuVuc?> XoaKhuVuc(int id);
        int TongKhuVuc(int maCSNT);
    }
}

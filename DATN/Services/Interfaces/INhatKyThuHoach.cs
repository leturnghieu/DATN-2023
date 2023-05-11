using DATN.DTOs;
using DATN.Models;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface INhatKyThuHoach
    {
        Task<PaginatedResponse<NhatKyThuHoach>> DanhSachNhatKyThuHoach(string? search = null, int? pageIndex = 1, int? maCSNT = null);
        Task<NhatKyThuHoach?> ThemMoiNhatKyThuHoach(NhatKyThuHoachRequest request);
        Task<NhatKyThuHoachEditRequest?> SuaNhatKyThuHoach(int id, NhatKyThuHoachEditRequest request);
        Task<NhatKyThuHoach?> Tim(Expression<Func<NhatKyThuHoach, bool>>? filter = null);
        Task<NhatKyThuHoach?> XoaNhatKyThuHoach(int id);
        Task<IEnumerable> DanhSachKhuVuc(int nguoiDung);
    }
}

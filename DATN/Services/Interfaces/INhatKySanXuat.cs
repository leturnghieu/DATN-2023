using DATN.DTOs;
using DATN.Models;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface INhatKySanXuat
    {
        Task<PaginatedResponse<NhatKySanXuat>> DanhSachNhatKySanXuat(string? search = null, int? pageIndex = 1, int? maCSNT = null);
        Task<NhatKySanXuat?> ThemMoiNhatKySanXuat(NhatKySanXuatRequest request, int csntId);
        Task<NhatKySanXuat?> SuaNhatKySanXuat(int id, NhatKySanXuatEditRequest request);
        Task<NhatKySanXuat?> Tim(Expression<Func<NhatKySanXuat, bool>>? filter = null);
        Task<NhatKySanXuat?> XoaNhatKySanXuat(int id, int csntId);
        Task<IEnumerable> DanhSachKhuVuc(int nguoiDung);

        Task<IEnumerable> DanhSachKho();
        Task<IEnumerable> DanhSachVatTu(int warehouse, int csntId);
    }
}

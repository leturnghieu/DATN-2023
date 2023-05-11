using DATN.DTOs;
using DATN.Models;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface INhatKyMuaSam
    {
        Task<PaginatedResponse<NhatKyMuaSam>> DanhSachNhatKyMuaSam(string? search = null, int? pageIndex = 1, int? maCSNT = null);
        Task<PaginatedResponse<NhatKyMuaSam>> DanhSachVatTuHetHan(string? search = null, int? pageIndex = 1, int? maCSNT = null);
        Task<NhatKyMuaSam?> ThemMoiNhatKyMuaSam(int idNguoiDung, NhatKyMuaSamRequest request);
        Task<NhatKyMuaSam?> SuaNhatKyMuaSam(int id, NhatKyMuaSam request);
        Task<NhatKyMuaSam?> Tim(Expression<Func<NhatKyMuaSam, bool>>? filter = null);
        Task<NhatKyMuaSam?> XoaNhatKyMuaSam(int id);
        Task<IEnumerable> DanhSachLoaiVatTu();
        Task<IEnumerable> DanhSachKhoVatTu();
        double TongSoTienMuaSam(int maCsnt);
        int TongSoVatTuHetHan(int maCsnt);
    }
}

using DATN.DTOs;
using DATN.Models;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface IKhoVatTu
    {
        Task<PaginatedResponse<KhoVatTu>> DanhSachKhoVatTu(string? search = null, int? pageIndex = 1, int? maCSNT = null);
        Task<KhoVatTu?> ThemMoiKhoVatTu(KhoVatTuRequest request);
        Task<KhoVatTu?> SuaKhoVatTu(int id, KhoVatTu request);
        Task<KhoVatTu?> Tim(Expression<Func<KhoVatTu, bool>>? filter = null);
        Task<KhoVatTu?> XoaKhoVatTu(int id);
    }
}

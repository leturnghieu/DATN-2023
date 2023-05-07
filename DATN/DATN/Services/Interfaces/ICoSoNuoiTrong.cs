using DATN.DTOs;
using DATN.Models;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface ICoSoNuoiTrong
    {
        Task<PaginatedResponse<CoSoNuoiTrong>> DanhSachCoSoNuoiTrong(string? search = null, int? pageIndex = 1);
        Task<CoSoNuoiTrong?> ThemMoiCoSoNuoiTrong(CoSoNuoiTrongRequest request);
        Task<CoSoNuoiTrong?> SuaCoSoNuoiTrong(int id, CoSoNuoiTrongRequest request);
        Task<CoSoNuoiTrong?> Tim(Expression<Func<CoSoNuoiTrong, bool>>? filter = null);
        Task<CoSoNuoiTrong?> XoaCoSoNuoiTrong(int id);
    }
}

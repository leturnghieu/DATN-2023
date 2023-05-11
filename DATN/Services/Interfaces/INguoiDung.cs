using DATN.DTOs;
using DATN.Models;
using System.Collections;
using System.Linq.Expressions;

namespace DATN.Services.Interfaces
{
    public interface INguoiDung
    {
        Task<NguoiDung?> DangKy(DangKyRequest request);
        Task<NguoiDung?> DangNhap(DangNhapRequest request);
        Task<IEnumerable> DanhSachCoSoNuoiTrong();
        Task<NguoiDung?> Tim(Expression<Func<NguoiDung, bool>>? filter = null);
        bool ThemHinhAnh(int id, HinhAnhRequest request);
        Task<NguoiDungRequest?> ChinhSua(int id, NguoiDungRequest request);
        //ADMIN
        Task<PaginatedResponse<NguoiDung>> DanhSachNguoiDung(string? search = null, int? pageIndex = 1);
        Task<PaginatedResponse<NguoiDung>> DanhSachNguoiDungTheoCSNT(string? search = null, int? pageIndex = 1, int? csntId = null);
        List<NguoiDung> NguoiDungs();
        Task<NguoiDung?> XoaNguoiDung(int id);
        double TongChiPhi(int id);
        double TongDoanhSo(int id);
    }
}

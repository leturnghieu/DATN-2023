using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static DATN.Models.Enum;

namespace DATN.DTOs
{
    public class DangKyRequest
    {
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        [EmailAddress(ErrorMessage = "Phải nhập đúng định dạng Email !")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phải nhập tên người dùng")]
        public string? TenNguoiDung { get; set; }
        [Required(ErrorMessage = "Phải nhập cơ sở nuôi trồng")]
        public int MaCoSoNuoiTrong { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải lớn hơn hoặc bằng 6 kí tự !")]
        public string? MatKhau { get; set; }
        [Compare("MatKhau", ErrorMessage = "Nhập giống mật khẩu !")]
        public string? NhapLaiMatKhau { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class DangNhapRequest
    {
        [Required(ErrorMessage = "Email không được bỏ trống!")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        public string? MatKhau { get; set; }
    }
}

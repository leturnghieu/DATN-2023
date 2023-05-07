using DATN.Models;
using static DATN.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class EditUserRequest
    {
        [Required]
        public string? TenNguoiDung { get; set; }
        public GioiTinh GioiTinh { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Nhập đúng định dạng số điện thoại")]
        public string? SDT { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}

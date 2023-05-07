using DATN.Models;
using static DATN.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class NguoiDungAddRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public string? TenNguoiDung { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        [DataType(DataType.Password)]
        public string? MatKhau { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public Roles Roles { get; set; } = Roles.USER;

        [Required(ErrorMessage = "Không được bỏ trống !")]
        public int MaCoSoNuoiTrong { get; set; }
        public string? SDT { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? HinhAnh { get; set; }
    }
}

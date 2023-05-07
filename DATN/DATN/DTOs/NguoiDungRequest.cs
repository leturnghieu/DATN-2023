using DATN.Models;
using static DATN.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DATN.Validations;

namespace DATN.DTOs
{
    public class NguoiDungRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string? TenNguoiDung { get; set; }
        public GioiTinh GioiTinh { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [EmailAddress(ErrorMessage = "Phải đúng định dạng Email!")]
        public string? Email { get; set; }
        [PhoneNumber(ErrorMessage = "Giá trị không phải là số điện thoại!")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Giá trị không phải là số điện thoại!")]
        public string? SDT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập ngày tháng năm sinh!")]
        [DateLessThanNow(ErrorMessage = "Ngày tháng năm sinh không hợp lệ!")]
        public DateTime? NgaySinh { get; set; }
    }
}

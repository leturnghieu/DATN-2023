using DATN.Models;
using DATN.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.DTOs
{
    public class NhatKyMuaSamRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public string? TenVatTu { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public string? XuatXu { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        [Range(0, int.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        [Range(0, double.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public double Gia { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public DateTime NgayMua { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Không được bỏ trống !")]
        [DateLessThanNow(ErrorMessage = "Ngày sản xuất không thể lớn hơn ngày hiện tại!")]
        public DateTime NgaySanXuat { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        [ExpirationDateValidation("NgaySanXuat", ErrorMessage = "Hạn sử dụng phải lớn hơn ngày sản xuất!")]
        public DateTime HanSuDung { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public int MaLoaiVatTu { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public int MaKhoVatTu { get; set; }
    }
}

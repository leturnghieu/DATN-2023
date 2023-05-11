using DATN.Validations;
using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class CoSoNuoiTrongRequest
    {
        public int MaCoSoNuoiTrong { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public double DienTich { get; set; }
        public double DienTichDaSuDung { get; set; } = 0;
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public string? DiaChi { get; set; }
    }
}

using DATN.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.DTOs
{
    public class NhatKyBanSanPhamRequest
    {
        [ForeignKey("NhatKyThuHoach")]
        public int MaNhatKyThuHoach { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [Range(0, int.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public double GiaBan { get; set; }
    }
}

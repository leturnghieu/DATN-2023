using DATN.Validations;
using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class NhatKyThuHoachEditRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [Range(0, int.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public int SoLuongThuHoach { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public int SoLuongDaBan { get; set; }
    }
}

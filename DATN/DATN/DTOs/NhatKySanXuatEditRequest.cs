using DATN.Validations;
using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class NhatKySanXuatEditRequest
    {
        [Required(ErrorMessage = "Không được để trống!")]
        [Range(0, int.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public int SoLuongSuDung { get; set; }
    }
}

using DATN.Validations;
using System.ComponentModel.DataAnnotations;
namespace DATN.DTOs
{
    public class NhatKySanXuatRequest
    {
        [Required(ErrorMessage = "Không được để trống!")]
        public string? TenVatTu { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        [Range(0, int.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public int SoLuongSuDung { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public int MaKhuVuc { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public int MaKhoVatTu { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public DateTime NgaySuDung { get; set; } = DateTime.Now;
    }
}

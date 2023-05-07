using DATN.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.DTOs
{
    public class NhatKyThuHoachRequest
    {
        [Required( ErrorMessage = "Không được bỏ trống!")]
        [Range(0, int.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public int SoLuongThuHoach { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        [DateLessThanNow(ErrorMessage = "Ngày thu hoạch không được lớn hơn ngày hiện tại!")]
        public DateTime NgayThuHoach { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống!")]
        public int MaKhuVuc { get; set; }
    }
}

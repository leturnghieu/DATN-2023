using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class KhoVatTuRequest
    {
        [Required(ErrorMessage = "Không được bỏ trống !")]
        public string? TenKho { get; set; }
    }
}

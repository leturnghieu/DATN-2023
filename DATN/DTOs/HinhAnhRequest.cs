using System.ComponentModel.DataAnnotations;

namespace DATN.DTOs
{
    public class HinhAnhRequest
    {
        [Required(ErrorMessage = "Phải chọn file ảnh")]
        public IFormFile? fileAnh { get; set; }
    }
}

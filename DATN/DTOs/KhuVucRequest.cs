using DATN.Models;
using DATN.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.DTOs
{
    public class KhuVucRequest
    {
        [Required(ErrorMessage = "Phải nhập tên khu vực")]
        public string? TenKhuVuc { get; set; }
        [Required(ErrorMessage = "Phải nhập diện tích")]
        [Range(0, double.MaxValue, ErrorMessage = "Không được nhập số âm!")]
        public double DienTich { get; set; }
        [Required(ErrorMessage = "Sản phẩm phải được thêm")]
        public string? SanPham { get; set; }
        public IFormFile? FileAnh { get; set; }
    }
}

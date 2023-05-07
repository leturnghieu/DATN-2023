using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    [Table("KhuVuc")]
    public class KhuVuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhuVuc { get; set; }
        [Required]
        public string? TenKhuVuc { get; set; }
        [Required]
        public double DienTich { get; set; }
        [Required]
        public string? SanPham { get; set; }
        public string? HinhAnhSanPham { get; set; }
        [Required]
        public DateTime ThoiGianTao { get; set; }
        [ForeignKey("NguoiDung")]
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public ICollection<NhatKyThuHoach>? NhatKyThuHoachs { get; set; }
        public ICollection<NhatKySanXuat>? NhatKySanXuats { get; set; }
    }
}

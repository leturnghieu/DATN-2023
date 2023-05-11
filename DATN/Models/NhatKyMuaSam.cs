using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Timers;

namespace DATN.Models
{
    [Table("NhatKyMuaSam")]
    public class NhatKyMuaSam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhatKyMuaSam { get; set; }
        [Required]
        public string? TenVatTu { get; set; }
        [Required]
        public string? XuatXu { get; set; }
        [Required]
        public int SoLuong { get; set; }
        public int SoLuongDaSuDung { get; set; } = 0;
        [Required]
        public double Gia { get; set; }
        [Required]
        public DateTime NgayMua { get; set; }
        [Required]
        public DateTime NgaySanXuat { get; set; }
        [Required]
        public DateTime HanSuDung { get; set; }
        [ForeignKey("LoaiVatTu")]
        public int MaLoaiVatTu { get; set; }
        public virtual LoaiVatTu? LoaiVatTu { get; set; }
        [ForeignKey("KhoVatTu")]
        public int MaKhoVatTu { get; set; }
        public virtual KhoVatTu? KhoVatTu { get; set; }
        [ForeignKey("NguoiDung")]
        public int MaNguoiDung { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public string? TrangThai { get; set; }
    }
}

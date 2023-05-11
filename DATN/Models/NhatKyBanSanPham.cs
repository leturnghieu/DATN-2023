using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    [Table("NhatKyBanSanPham")]
    public class NhatKyBanSanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhatKyBanSanPham { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public double GiaBan { get; set; }
        [Required]
        public DateTime NgayBan { get; set; }
        public string? QRCode { get; set; }
        [ForeignKey("NhatKyThuHoach")]
        public int MaNhatKyThuHoach { get; set; }
        public virtual NhatKyThuHoach? NhatKyThuHoach { get; set; }
    }
}

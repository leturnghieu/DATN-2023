using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    [Table("NhatKyThuHoach")]
    public class NhatKyThuHoach
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhatKyThuHoach { get; set; }
        [Required]
        public int SoLuongThuHoach { get; set; }
        public int SoLuongDaBan { get; set; }
        [Required]
        public DateTime NgayThuHoach { get; set; }
        [ForeignKey("KhuVuc")]
        public int MaKhuVuc { get; set; }
        public virtual KhuVuc? KhuVuc { get; set; }
        public ICollection<NhatKyBanSanPham>? NhatKyBanSanPhams { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    [Table("NhatKySanXuat")]
    public class NhatKySanXuat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhatKySanXuat { get; set; }
        [Required]
        public string? TenVatTu { get; set; }
        [Required]
        public int SoLuongSuDung { get; set; }
        [Required]
        public DateTime NgaySuDung { get; set; }
        [ForeignKey("KhoVatTu")]
        public int MaKhoVatTu { get; set; }
        public virtual KhoVatTu? KhoVatTu { get; set; }
        [ForeignKey("KhuVuc")]
        public int MaKhuVuc { get; set; }
        public virtual KhuVuc? KhuVuc { get; set; }
    }
}

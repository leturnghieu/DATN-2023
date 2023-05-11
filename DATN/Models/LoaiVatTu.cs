using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    [Table("LoaiVatTu")]
    public class LoaiVatTu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLoai { get; set; }
        [Required]
        public string? TenLoai { get; set; }
        public ICollection<NhatKyMuaSam>? NhatKyMuaSams { get; set; }
    }
}

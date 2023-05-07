using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    [Table("KhoVatTu")]
    public class KhoVatTu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhoVatTu { get; set; }
        [Required]
        public string? TenKho { get; set; }
        public ICollection<NhatKySanXuat>? NhatKySanXuats { get; set; }
        public ICollection<NhatKyMuaSam>? NhatKyVatTus { get; set; }
    }
}

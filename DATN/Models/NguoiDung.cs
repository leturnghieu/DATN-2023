using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using static DATN.Models.Enum;

namespace DATN.Models
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNguoiDung { get; set; }
        [Required]
        public string? TenNguoiDung { get; set; }
        public GioiTinh GioiTinh { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? SDT { get; set; }
        public DateTime? NgaySinh { get; set; }
        [Required]
        public string? MatKhau { get; set; }
        public Roles Roles { get; set; } = Roles.USER;
        public string? HinhAnh { get; set; }
        [ForeignKey("CoSoNuoiTrong")]
        public int MaCoSoNuoiTrong { get; set; }
        public virtual CoSoNuoiTrong? CoSoNuoiTrong { get; set; }
        public ICollection<KhuVuc>? KhuVucs { get; set; }
        public ICollection<NhatKyMuaSam>? NhatKyMuaSams { get; set; }
    }
}

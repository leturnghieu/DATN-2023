using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATN.Models
{
    [Table("CoSoNuoiTrong")]
    public class CoSoNuoiTrong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaCoSoNuoiTrong { get; set; }
        [Required]
        public double DienTich { get; set; }
        [Required]
        public double DienTichDaSuDung { get; set; }
        [Required]
        public string? DiaChi { get; set; }
        public ICollection<NguoiDung>? NguoiDungs { get; set; }
    }
}

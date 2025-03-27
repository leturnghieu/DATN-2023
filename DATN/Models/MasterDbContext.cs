using Microsoft.EntityFrameworkCore;
using DATN.Models;

namespace DATN.Models
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }
        public MasterDbContext() { }
        public DbSet<KhoVatTu> KhoVatTus { get; set; }
        public DbSet<CoSoNuoiTrong> CoSoNuoiTrongs { get; set; }
        public DbSet<KhuVuc> KhuVucs { get; set; }
        public DbSet<LoaiVatTu> LoaiVatTus { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<NhatKyBanSanPham> NhatKyBanSanPhams { get; set; }
        public DbSet<NhatKyMuaSam> NhatKyMuaSams { get; set; }
        public DbSet<NhatKySanXuat> NhatKySanXuats { get; set; }
        public DbSet<NhatKyThuHoach> NhatKyThuHoaches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Data Source=LeTrungHieu;initial catalog=DATN2023;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiVatTu>().HasData(
                new LoaiVatTu
                {
                    MaLoai = 1,
                    TenLoai = "Vật phẩm tiêu hao"
                },
                new LoaiVatTu
                {
                    MaLoai = 2,
                    TenLoai = "Vật tư không tiêu hao"
                }
            );
            modelBuilder.Entity<CoSoNuoiTrong>().HasData(
                new CoSoNuoiTrong
                {
                    MaCoSoNuoiTrong = 1,
                    DienTich = 1800,
                    DiaChi = "Đội 1, xã Tứ Dân, Khoái Châu, Hưng Yên"
                },
                new CoSoNuoiTrong
                {
                    MaCoSoNuoiTrong = 2,
                    DienTich = 2600,
                    DiaChi = "Đội 2, xã Tứ Dân, Khoái Châu, Hưng Yên"
                },
                new CoSoNuoiTrong
                {
                    MaCoSoNuoiTrong = 3,
                    DienTich = 3000,
                    DiaChi = "Đội 3, xã Tứ Dân, Khoái Châu, Hưng Yên"
                },
                new CoSoNuoiTrong
                {
                    MaCoSoNuoiTrong = 4,
                    DienTich = 2500,
                    DiaChi = "Đội 4, xã Tứ Dân, Khoái Châu, Hưng Yên"
                },
                new CoSoNuoiTrong
                {
                    MaCoSoNuoiTrong = 5,
                    DienTich = 1000,
                    DiaChi = "Đội 5, xã Tứ Dân, Khoái Châu, Hưng Yên"
                }
            );
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoSoNuoiTrong",
                columns: table => new
                {
                    MaCoSoNuoiTrong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DienTich = table.Column<double>(type: "float", nullable: false),
                    DienTichDaSuDung = table.Column<double>(type: "float", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoSoNuoiTrong", x => x.MaCoSoNuoiTrong);
                });

            migrationBuilder.CreateTable(
                name: "LoaiVatTu",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiVatTu", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSanPham);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roles = table.Column<int>(type: "int", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaCoSoNuoiTrong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                    table.ForeignKey(
                        name: "FK_NguoiDung_CoSoNuoiTrong_MaCoSoNuoiTrong",
                        column: x => x.MaCoSoNuoiTrong,
                        principalTable: "CoSoNuoiTrong",
                        principalColumn: "MaCoSoNuoiTrong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VatTu",
                columns: table => new
                {
                    MaKhoVatTu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiVatTuMaLoai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatTu", x => x.MaKhoVatTu);
                    table.ForeignKey(
                        name: "FK_VatTu_LoaiVatTu_LoaiVatTuMaLoai",
                        column: x => x.LoaiVatTuMaLoai,
                        principalTable: "LoaiVatTu",
                        principalColumn: "MaLoai");
                });

            migrationBuilder.CreateTable(
                name: "KhuVuc",
                columns: table => new
                {
                    MaKhuVuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhuVuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienTich = table.Column<double>(type: "float", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false),
                    MaSanPham = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuVuc", x => x.MaKhuVuc);
                    table.ForeignKey(
                        name: "FK_KhuVuc_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KhuVuc_SanPham_MaSanPham",
                        column: x => x.MaSanPham,
                        principalTable: "SanPham",
                        principalColumn: "MaSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyMuaSam",
                columns: table => new
                {
                    MaNhatKyMuaSam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenVatTu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XuatXu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    NgayMua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgaySanXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanSuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaLoaiVatTu = table.Column<int>(type: "int", nullable: false),
                    MaKhoVatTu = table.Column<int>(type: "int", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyMuaSam", x => x.MaNhatKyMuaSam);
                    table.ForeignKey(
                        name: "FK_NhatKyMuaSam_LoaiVatTu_MaLoaiVatTu",
                        column: x => x.MaLoaiVatTu,
                        principalTable: "LoaiVatTu",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhatKyMuaSam_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhatKyMuaSam_VatTu_MaKhoVatTu",
                        column: x => x.MaKhoVatTu,
                        principalTable: "VatTu",
                        principalColumn: "MaKhoVatTu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyBanSanPham",
                columns: table => new
                {
                    MaNhatKyBanSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<double>(type: "float", nullable: false),
                    NgayBan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaKhuVuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyBanSanPham", x => x.MaNhatKyBanSanPham);
                    table.ForeignKey(
                        name: "FK_NhatKyBanSanPham_KhuVuc_MaKhuVuc",
                        column: x => x.MaKhuVuc,
                        principalTable: "KhuVuc",
                        principalColumn: "MaKhuVuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhatKySanXuat",
                columns: table => new
                {
                    MaNhatKySanXuat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuongSuDung = table.Column<int>(type: "int", nullable: false),
                    NgaySuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaKhoVatTu = table.Column<int>(type: "int", nullable: false),
                    MaKhuVuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKySanXuat", x => x.MaNhatKySanXuat);
                    table.ForeignKey(
                        name: "FK_NhatKySanXuat_KhuVuc_MaKhuVuc",
                        column: x => x.MaKhuVuc,
                        principalTable: "KhuVuc",
                        principalColumn: "MaKhuVuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhatKySanXuat_VatTu_MaKhoVatTu",
                        column: x => x.MaKhoVatTu,
                        principalTable: "VatTu",
                        principalColumn: "MaKhoVatTu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyThuHoach",
                columns: table => new
                {
                    MaNhatKyThuHoach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuongThuHoach = table.Column<int>(type: "int", nullable: false),
                    NgayThuHoach = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaKhuVUc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyThuHoach", x => x.MaNhatKyThuHoach);
                    table.ForeignKey(
                        name: "FK_NhatKyThuHoach_KhuVuc_MaKhuVUc",
                        column: x => x.MaKhuVUc,
                        principalTable: "KhuVuc",
                        principalColumn: "MaKhuVuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CoSoNuoiTrong",
                columns: new[] { "MaCoSoNuoiTrong", "DiaChi", "DienTich", "DienTichDaSuDung" },
                values: new object[,]
                {
                    { 1, "Đội 1, xã Tứ Dân, Khoái Châu, Hưng Yên", 1800.0, 0.0 },
                    { 2, "Đội 2, xã Tứ Dân, Khoái Châu, Hưng Yên", 2600.0, 0.0 },
                    { 3, "Đội 3, xã Tứ Dân, Khoái Châu, Hưng Yên", 3000.0, 0.0 },
                    { 4, "Đội 4, xã Tứ Dân, Khoái Châu, Hưng Yên", 2500.0, 0.0 },
                    { 5, "Đội 5, xã Tứ Dân, Khoái Châu, Hưng Yên", 1000.0, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "LoaiVatTu",
                columns: new[] { "MaLoai", "TenLoai" },
                values: new object[,]
                {
                    { 1, "Vật phẩm tiêu hao" },
                    { 2, "Vật tư không tiêu hao" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhuVuc_MaNguoiDung",
                table: "KhuVuc",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_KhuVuc_MaSanPham",
                table: "KhuVuc",
                column: "MaSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_MaCoSoNuoiTrong",
                table: "NguoiDung",
                column: "MaCoSoNuoiTrong");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyBanSanPham_MaKhuVuc",
                table: "NhatKyBanSanPham",
                column: "MaKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyMuaSam_MaKhoVatTu",
                table: "NhatKyMuaSam",
                column: "MaKhoVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyMuaSam_MaLoaiVatTu",
                table: "NhatKyMuaSam",
                column: "MaLoaiVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyMuaSam_MaNguoiDung",
                table: "NhatKyMuaSam",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKySanXuat_MaKhoVatTu",
                table: "NhatKySanXuat",
                column: "MaKhoVatTu");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKySanXuat_MaKhuVuc",
                table: "NhatKySanXuat",
                column: "MaKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyThuHoach_MaKhuVUc",
                table: "NhatKyThuHoach",
                column: "MaKhuVUc");

            migrationBuilder.CreateIndex(
                name: "IX_VatTu_LoaiVatTuMaLoai",
                table: "VatTu",
                column: "LoaiVatTuMaLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhatKyBanSanPham");

            migrationBuilder.DropTable(
                name: "NhatKyMuaSam");

            migrationBuilder.DropTable(
                name: "NhatKySanXuat");

            migrationBuilder.DropTable(
                name: "NhatKyThuHoach");

            migrationBuilder.DropTable(
                name: "VatTu");

            migrationBuilder.DropTable(
                name: "KhuVuc");

            migrationBuilder.DropTable(
                name: "LoaiVatTu");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "CoSoNuoiTrong");
        }
    }
}

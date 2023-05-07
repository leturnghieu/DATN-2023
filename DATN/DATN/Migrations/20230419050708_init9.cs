using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN.Migrations
{
    public partial class init9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhatKyBanSanPham_KhuVuc_MaKhuVuc",
                table: "NhatKyBanSanPham");

            migrationBuilder.DropIndex(
                name: "IX_NhatKyBanSanPham_MaKhuVuc",
                table: "NhatKyBanSanPham");

            migrationBuilder.RenameColumn(
                name: "MaKhuVuc",
                table: "NhatKyBanSanPham",
                newName: "MaNhatKyThuHoach");

            migrationBuilder.AddColumn<int>(
                name: "NhatKyThuHoachMaNhatKyThuHoach",
                table: "NhatKyBanSanPham",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyBanSanPham_NhatKyThuHoachMaNhatKyThuHoach",
                table: "NhatKyBanSanPham",
                column: "NhatKyThuHoachMaNhatKyThuHoach");

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKyBanSanPham_NhatKyThuHoach_NhatKyThuHoachMaNhatKyThuHoach",
                table: "NhatKyBanSanPham",
                column: "NhatKyThuHoachMaNhatKyThuHoach",
                principalTable: "NhatKyThuHoach",
                principalColumn: "MaNhatKyThuHoach");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhatKyBanSanPham_NhatKyThuHoach_NhatKyThuHoachMaNhatKyThuHoach",
                table: "NhatKyBanSanPham");

            migrationBuilder.DropIndex(
                name: "IX_NhatKyBanSanPham_NhatKyThuHoachMaNhatKyThuHoach",
                table: "NhatKyBanSanPham");

            migrationBuilder.DropColumn(
                name: "NhatKyThuHoachMaNhatKyThuHoach",
                table: "NhatKyBanSanPham");

            migrationBuilder.RenameColumn(
                name: "MaNhatKyThuHoach",
                table: "NhatKyBanSanPham",
                newName: "MaKhuVuc");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyBanSanPham_MaKhuVuc",
                table: "NhatKyBanSanPham",
                column: "MaKhuVuc");

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKyBanSanPham_KhuVuc_MaKhuVuc",
                table: "NhatKyBanSanPham",
                column: "MaKhuVuc",
                principalTable: "KhuVuc",
                principalColumn: "MaKhuVuc",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

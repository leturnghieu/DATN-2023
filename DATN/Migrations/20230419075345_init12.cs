using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN.Migrations
{
    public partial class init12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyBanSanPham_MaNhatKyThuHoach",
                table: "NhatKyBanSanPham",
                column: "MaNhatKyThuHoach");

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKyBanSanPham_NhatKyThuHoach_MaNhatKyThuHoach",
                table: "NhatKyBanSanPham",
                column: "MaNhatKyThuHoach",
                principalTable: "NhatKyThuHoach",
                principalColumn: "MaNhatKyThuHoach",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhatKyBanSanPham_NhatKyThuHoach_MaNhatKyThuHoach",
                table: "NhatKyBanSanPham");

            migrationBuilder.DropIndex(
                name: "IX_NhatKyBanSanPham_MaNhatKyThuHoach",
                table: "NhatKyBanSanPham");

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
    }
}

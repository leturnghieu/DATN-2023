using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN.Migrations
{
    public partial class init10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhatKyThuHoach_KhuVuc_MaKhuVUc",
                table: "NhatKyThuHoach");

            migrationBuilder.RenameColumn(
                name: "MaKhuVUc",
                table: "NhatKyThuHoach",
                newName: "MaKhuVuc");

            migrationBuilder.RenameIndex(
                name: "IX_NhatKyThuHoach_MaKhuVUc",
                table: "NhatKyThuHoach",
                newName: "IX_NhatKyThuHoach_MaKhuVuc");

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKyThuHoach_KhuVuc_MaKhuVuc",
                table: "NhatKyThuHoach",
                column: "MaKhuVuc",
                principalTable: "KhuVuc",
                principalColumn: "MaKhuVuc",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhatKyThuHoach_KhuVuc_MaKhuVuc",
                table: "NhatKyThuHoach");

            migrationBuilder.RenameColumn(
                name: "MaKhuVuc",
                table: "NhatKyThuHoach",
                newName: "MaKhuVUc");

            migrationBuilder.RenameIndex(
                name: "IX_NhatKyThuHoach_MaKhuVuc",
                table: "NhatKyThuHoach",
                newName: "IX_NhatKyThuHoach_MaKhuVUc");

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKyThuHoach_KhuVuc_MaKhuVUc",
                table: "NhatKyThuHoach",
                column: "MaKhuVUc",
                principalTable: "KhuVuc",
                principalColumn: "MaKhuVuc",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

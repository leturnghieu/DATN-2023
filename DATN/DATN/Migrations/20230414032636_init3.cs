using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhoVatTu_LoaiVatTu_LoaiVatTuMaLoai",
                table: "KhoVatTu");

            migrationBuilder.DropIndex(
                name: "IX_KhoVatTu_LoaiVatTuMaLoai",
                table: "KhoVatTu");

            migrationBuilder.DropColumn(
                name: "LoaiVatTuMaLoai",
                table: "KhoVatTu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoaiVatTuMaLoai",
                table: "KhoVatTu",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KhoVatTu_LoaiVatTuMaLoai",
                table: "KhoVatTu",
                column: "LoaiVatTuMaLoai");

            migrationBuilder.AddForeignKey(
                name: "FK_KhoVatTu_LoaiVatTu_LoaiVatTuMaLoai",
                table: "KhoVatTu",
                column: "LoaiVatTuMaLoai",
                principalTable: "LoaiVatTu",
                principalColumn: "MaLoai");
        }
    }
}

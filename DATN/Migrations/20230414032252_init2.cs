using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhatKyMuaSam_VatTu_MaKhoVatTu",
                table: "NhatKyMuaSam");

            migrationBuilder.DropForeignKey(
                name: "FK_NhatKySanXuat_VatTu_MaKhoVatTu",
                table: "NhatKySanXuat");

            migrationBuilder.DropForeignKey(
                name: "FK_VatTu_LoaiVatTu_LoaiVatTuMaLoai",
                table: "VatTu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VatTu",
                table: "VatTu");

            migrationBuilder.RenameTable(
                name: "VatTu",
                newName: "KhoVatTu");

            migrationBuilder.RenameIndex(
                name: "IX_VatTu_LoaiVatTuMaLoai",
                table: "KhoVatTu",
                newName: "IX_KhoVatTu_LoaiVatTuMaLoai");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KhoVatTu",
                table: "KhoVatTu",
                column: "MaKhoVatTu");

            migrationBuilder.AddForeignKey(
                name: "FK_KhoVatTu_LoaiVatTu_LoaiVatTuMaLoai",
                table: "KhoVatTu",
                column: "LoaiVatTuMaLoai",
                principalTable: "LoaiVatTu",
                principalColumn: "MaLoai");

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKyMuaSam_KhoVatTu_MaKhoVatTu",
                table: "NhatKyMuaSam",
                column: "MaKhoVatTu",
                principalTable: "KhoVatTu",
                principalColumn: "MaKhoVatTu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKySanXuat_KhoVatTu_MaKhoVatTu",
                table: "NhatKySanXuat",
                column: "MaKhoVatTu",
                principalTable: "KhoVatTu",
                principalColumn: "MaKhoVatTu",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhoVatTu_LoaiVatTu_LoaiVatTuMaLoai",
                table: "KhoVatTu");

            migrationBuilder.DropForeignKey(
                name: "FK_NhatKyMuaSam_KhoVatTu_MaKhoVatTu",
                table: "NhatKyMuaSam");

            migrationBuilder.DropForeignKey(
                name: "FK_NhatKySanXuat_KhoVatTu_MaKhoVatTu",
                table: "NhatKySanXuat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KhoVatTu",
                table: "KhoVatTu");

            migrationBuilder.RenameTable(
                name: "KhoVatTu",
                newName: "VatTu");

            migrationBuilder.RenameIndex(
                name: "IX_KhoVatTu_LoaiVatTuMaLoai",
                table: "VatTu",
                newName: "IX_VatTu_LoaiVatTuMaLoai");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VatTu",
                table: "VatTu",
                column: "MaKhoVatTu");

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKyMuaSam_VatTu_MaKhoVatTu",
                table: "NhatKyMuaSam",
                column: "MaKhoVatTu",
                principalTable: "VatTu",
                principalColumn: "MaKhoVatTu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhatKySanXuat_VatTu_MaKhoVatTu",
                table: "NhatKySanXuat",
                column: "MaKhoVatTu",
                principalTable: "VatTu",
                principalColumn: "MaKhoVatTu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VatTu_LoaiVatTu_LoaiVatTuMaLoai",
                table: "VatTu",
                column: "LoaiVatTuMaLoai",
                principalTable: "LoaiVatTu",
                principalColumn: "MaLoai");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATN.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhuVuc_SanPham_MaSanPham",
                table: "KhuVuc");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropIndex(
                name: "IX_KhuVuc_MaSanPham",
                table: "KhuVuc");

            migrationBuilder.DropColumn(
                name: "MaSanPham",
                table: "KhuVuc");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnhSanPham",
                table: "KhuVuc",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SanPham",
                table: "KhuVuc",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianTao",
                table: "KhuVuc",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HinhAnhSanPham",
                table: "KhuVuc");

            migrationBuilder.DropColumn(
                name: "SanPham",
                table: "KhuVuc");

            migrationBuilder.DropColumn(
                name: "ThoiGianTao",
                table: "KhuVuc");

            migrationBuilder.AddColumn<int>(
                name: "MaSanPham",
                table: "KhuVuc",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSanPham = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSanPham);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KhuVuc_MaSanPham",
                table: "KhuVuc",
                column: "MaSanPham",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KhuVuc_SanPham_MaSanPham",
                table: "KhuVuc",
                column: "MaSanPham",
                principalTable: "SanPham",
                principalColumn: "MaSanPham",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

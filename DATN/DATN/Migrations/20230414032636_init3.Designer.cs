﻿// <auto-generated />
using System;
using DATN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DATN.Migrations
{
    [DbContext(typeof(MasterDbContext))]
    [Migration("20230414032636_init3")]
    partial class init3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DATN.Models.CoSoNuoiTrong", b =>
                {
                    b.Property<int>("MaCoSoNuoiTrong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaCoSoNuoiTrong"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("DienTich")
                        .HasColumnType("float");

                    b.Property<double>("DienTichDaSuDung")
                        .HasColumnType("float");

                    b.HasKey("MaCoSoNuoiTrong");

                    b.ToTable("CoSoNuoiTrong");

                    b.HasData(
                        new
                        {
                            MaCoSoNuoiTrong = 1,
                            DiaChi = "Đội 1, xã Tứ Dân, Khoái Châu, Hưng Yên",
                            DienTich = 1800.0,
                            DienTichDaSuDung = 0.0
                        },
                        new
                        {
                            MaCoSoNuoiTrong = 2,
                            DiaChi = "Đội 2, xã Tứ Dân, Khoái Châu, Hưng Yên",
                            DienTich = 2600.0,
                            DienTichDaSuDung = 0.0
                        },
                        new
                        {
                            MaCoSoNuoiTrong = 3,
                            DiaChi = "Đội 3, xã Tứ Dân, Khoái Châu, Hưng Yên",
                            DienTich = 3000.0,
                            DienTichDaSuDung = 0.0
                        },
                        new
                        {
                            MaCoSoNuoiTrong = 4,
                            DiaChi = "Đội 4, xã Tứ Dân, Khoái Châu, Hưng Yên",
                            DienTich = 2500.0,
                            DienTichDaSuDung = 0.0
                        },
                        new
                        {
                            MaCoSoNuoiTrong = 5,
                            DiaChi = "Đội 5, xã Tứ Dân, Khoái Châu, Hưng Yên",
                            DienTich = 1000.0,
                            DienTichDaSuDung = 0.0
                        });
                });

            modelBuilder.Entity("DATN.Models.KhoVatTu", b =>
                {
                    b.Property<int>("MaKhoVatTu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhoVatTu"), 1L, 1);

                    b.HasKey("MaKhoVatTu");

                    b.ToTable("KhoVatTu");
                });

            modelBuilder.Entity("DATN.Models.KhuVuc", b =>
                {
                    b.Property<int>("MaKhuVuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhuVuc"), 1L, 1);

                    b.Property<double>("DienTich")
                        .HasColumnType("float");

                    b.Property<string>("HinhAnhSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaNguoiDung")
                        .HasColumnType("int");

                    b.Property<string>("SanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhuVuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ThoiGianTao")
                        .HasColumnType("datetime2");

                    b.HasKey("MaKhuVuc");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("KhuVuc");
                });

            modelBuilder.Entity("DATN.Models.LoaiVatTu", b =>
                {
                    b.Property<int>("MaLoai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaLoai"), 1L, 1);

                    b.Property<string>("TenLoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLoai");

                    b.ToTable("LoaiVatTu");

                    b.HasData(
                        new
                        {
                            MaLoai = 1,
                            TenLoai = "Vật phẩm tiêu hao"
                        },
                        new
                        {
                            MaLoai = 2,
                            TenLoai = "Vật tư không tiêu hao"
                        });
                });

            modelBuilder.Entity("DATN.Models.NguoiDung", b =>
                {
                    b.Property<int>("MaNguoiDung")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNguoiDung"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GioiTinh")
                        .HasColumnType("int");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaCoSoNuoiTrong")
                        .HasColumnType("int");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<int>("Roles")
                        .HasColumnType("int");

                    b.Property<string>("SDT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNguoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNguoiDung");

                    b.HasIndex("MaCoSoNuoiTrong");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("DATN.Models.NhatKyBanSanPham", b =>
                {
                    b.Property<int>("MaNhatKyBanSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhatKyBanSanPham"), 1L, 1);

                    b.Property<double>("GiaBan")
                        .HasColumnType("float");

                    b.Property<int>("MaKhuVuc")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBan")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("MaNhatKyBanSanPham");

                    b.HasIndex("MaKhuVuc");

                    b.ToTable("NhatKyBanSanPham");
                });

            modelBuilder.Entity("DATN.Models.NhatKyMuaSam", b =>
                {
                    b.Property<int>("MaNhatKyMuaSam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhatKyMuaSam"), 1L, 1);

                    b.Property<double>("Gia")
                        .HasColumnType("float");

                    b.Property<DateTime>("HanSuDung")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaKhoVatTu")
                        .HasColumnType("int");

                    b.Property<int>("MaLoaiVatTu")
                        .HasColumnType("int");

                    b.Property<int>("MaNguoiDung")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayMua")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgaySanXuat")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenVatTu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("XuatXu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNhatKyMuaSam");

                    b.HasIndex("MaKhoVatTu");

                    b.HasIndex("MaLoaiVatTu");

                    b.HasIndex("MaNguoiDung");

                    b.ToTable("NhatKyMuaSam");
                });

            modelBuilder.Entity("DATN.Models.NhatKySanXuat", b =>
                {
                    b.Property<int>("MaNhatKySanXuat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhatKySanXuat"), 1L, 1);

                    b.Property<int>("MaKhoVatTu")
                        .HasColumnType("int");

                    b.Property<int>("MaKhuVuc")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgaySuDung")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuongSuDung")
                        .HasColumnType("int");

                    b.HasKey("MaNhatKySanXuat");

                    b.HasIndex("MaKhoVatTu");

                    b.HasIndex("MaKhuVuc");

                    b.ToTable("NhatKySanXuat");
                });

            modelBuilder.Entity("DATN.Models.NhatKyThuHoach", b =>
                {
                    b.Property<int>("MaNhatKyThuHoach")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaNhatKyThuHoach"), 1L, 1);

                    b.Property<int>("MaKhuVUc")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayThuHoach")
                        .HasColumnType("datetime2");

                    b.Property<int>("SoLuongThuHoach")
                        .HasColumnType("int");

                    b.HasKey("MaNhatKyThuHoach");

                    b.HasIndex("MaKhuVUc");

                    b.ToTable("NhatKyThuHoach");
                });

            modelBuilder.Entity("DATN.Models.KhuVuc", b =>
                {
                    b.HasOne("DATN.Models.NguoiDung", "NguoiDung")
                        .WithMany("KhuVucs")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("DATN.Models.NguoiDung", b =>
                {
                    b.HasOne("DATN.Models.CoSoNuoiTrong", "CoSoNuoiTrong")
                        .WithMany("NguoiDungs")
                        .HasForeignKey("MaCoSoNuoiTrong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CoSoNuoiTrong");
                });

            modelBuilder.Entity("DATN.Models.NhatKyBanSanPham", b =>
                {
                    b.HasOne("DATN.Models.KhuVuc", "KhuVuc")
                        .WithMany("NhatKyBanSanPhams")
                        .HasForeignKey("MaKhuVuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhuVuc");
                });

            modelBuilder.Entity("DATN.Models.NhatKyMuaSam", b =>
                {
                    b.HasOne("DATN.Models.KhoVatTu", "KhoVatTu")
                        .WithMany("NhatKyVatTus")
                        .HasForeignKey("MaKhoVatTu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN.Models.LoaiVatTu", "LoaiVatTu")
                        .WithMany("NhatKyMuaSams")
                        .HasForeignKey("MaLoaiVatTu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN.Models.NguoiDung", "NguoiDung")
                        .WithMany("NhatKyMuaSams")
                        .HasForeignKey("MaNguoiDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhoVatTu");

                    b.Navigation("LoaiVatTu");

                    b.Navigation("NguoiDung");
                });

            modelBuilder.Entity("DATN.Models.NhatKySanXuat", b =>
                {
                    b.HasOne("DATN.Models.KhoVatTu", "KhoVatTu")
                        .WithMany("NhatKySanXuats")
                        .HasForeignKey("MaKhoVatTu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DATN.Models.KhuVuc", "KhuVuc")
                        .WithMany("NhatKySanXuats")
                        .HasForeignKey("MaKhuVuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhoVatTu");

                    b.Navigation("KhuVuc");
                });

            modelBuilder.Entity("DATN.Models.NhatKyThuHoach", b =>
                {
                    b.HasOne("DATN.Models.KhuVuc", "KhuVuc")
                        .WithMany("NhatKyThuHoachs")
                        .HasForeignKey("MaKhuVUc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhuVuc");
                });

            modelBuilder.Entity("DATN.Models.CoSoNuoiTrong", b =>
                {
                    b.Navigation("NguoiDungs");
                });

            modelBuilder.Entity("DATN.Models.KhoVatTu", b =>
                {
                    b.Navigation("NhatKySanXuats");

                    b.Navigation("NhatKyVatTus");
                });

            modelBuilder.Entity("DATN.Models.KhuVuc", b =>
                {
                    b.Navigation("NhatKyBanSanPhams");

                    b.Navigation("NhatKySanXuats");

                    b.Navigation("NhatKyThuHoachs");
                });

            modelBuilder.Entity("DATN.Models.LoaiVatTu", b =>
                {
                    b.Navigation("NhatKyMuaSams");
                });

            modelBuilder.Entity("DATN.Models.NguoiDung", b =>
                {
                    b.Navigation("KhuVucs");

                    b.Navigation("NhatKyMuaSams");
                });
#pragma warning restore 612, 618
        }
    }
}

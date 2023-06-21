using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Super.Migrations
{
    /// <inheritdoc />
    public partial class Sup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhachHang = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaCungCap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DienThoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayLap = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    MaKH = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "NhanHieu",
                columns: table => new
                {
                    MaNhanHieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenNhanHieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaCungCap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanHieu", x => x.MaNhanHieu);
                    table.ForeignKey(
                        name: "FK_NhanHieu_NhaCungCap",
                        column: x => x.MaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaCungCap");
                });

            migrationBuilder.CreateTable(
                name: "Hang",
                columns: table => new
                {
                    MaHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHang = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DonGiaHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaNhanHieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(type: "ntext", nullable: true),
                    TonKho = table.Column<int>(type: "int", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hang", x => x.MaHang);
                    table.ForeignKey(
                        name: "FK_Hang_NhanHieu",
                        column: x => x.MaNhanHieu,
                        principalTable: "NhanHieu",
                        principalColumn: "MaNhanHieu");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false),
                    MaHang = table.Column<int>(type: "int", nullable: false),
                    SoLuongMua = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    DonGiaBan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "((0))"),
                    ThanhTien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "((0))"),
                    TongTien = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDon", x => new { x.MaHD, x.MaHang });
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_Hang",
                        column: x => x.MaHang,
                        principalTable: "Hang",
                        principalColumn: "MaHang");
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_HoaDon",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_MaHang",
                table: "ChiTietHoaDon",
                column: "MaHang");

            migrationBuilder.CreateIndex(
                name: "IX_Hang_MaNhanHieu",
                table: "Hang",
                column: "MaNhanHieu");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaKH",
                table: "HoaDon",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_NhanHieu_MaCungCap",
                table: "NhanHieu",
                column: "MaCungCap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "Hang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "NhanHieu");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhaCungCap");
        }
    }
}

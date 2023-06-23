using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Super.Migrations
{
    /// <inheritdoc />
    public partial class Supcua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
          name: "Filter",
          table: "Hang",
          nullable: true,
          computedColumnSql: "(LOWER([TenHang] + ' ' + [MaNhanHieu])",
          stored: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


        }
    }
}

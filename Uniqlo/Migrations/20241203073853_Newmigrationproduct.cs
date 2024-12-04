using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uniqlo.Migrations
{
    /// <inheritdoc />
    public partial class Newmigrationproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverFile",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverFile",
                table: "Products");
        }
    }
}

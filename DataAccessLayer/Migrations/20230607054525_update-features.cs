using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updatefeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SvgIconPath",
                schema: "dbo",
                table: "PropertyFeatures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Svgd",
                schema: "dbo",
                table: "PropertyFeatures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SvgIconPath",
                schema: "dbo",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Svgd",
                schema: "dbo",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SvgIconPath",
                schema: "dbo",
                table: "PropertyFeatures");

            migrationBuilder.DropColumn(
                name: "Svgd",
                schema: "dbo",
                table: "PropertyFeatures");

            migrationBuilder.DropColumn(
                name: "SvgIconPath",
                schema: "dbo",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Svgd",
                schema: "dbo",
                table: "Features");
        }
    }
}

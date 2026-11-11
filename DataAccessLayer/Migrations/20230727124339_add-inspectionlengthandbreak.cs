using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addinspectionlengthandbreak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InspectionBreak",
                schema: "dbo",
                table: "Properties",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InspectionLength",
                schema: "dbo",
                table: "Properties",
                type: "nvarchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InspectionBreak",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "InspectionLength",
                schema: "dbo",
                table: "Properties");
        }
    }
}

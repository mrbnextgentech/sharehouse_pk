using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addinspectiontimeinpropertytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "showInspectionTime",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.AddColumn<int>(
                name: "InspectionTime",
                schema: "dbo",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NavigationInstructions",
                schema: "dbo",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InspectionTime",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "NavigationInstructions",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.AddColumn<bool>(
                name: "showInspectionTime",
                schema: "dbo",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

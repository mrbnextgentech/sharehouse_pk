using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addservicetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Servicetype",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.AddColumn<int>(
                name: "Servicetype",
                schema: "dbo",
                table: "Services",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Servicetype",
                schema: "dbo",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "Servicetype",
                schema: "dbo",
                table: "PlanServices",
                type: "int",
                nullable: true);
        }
    }
}

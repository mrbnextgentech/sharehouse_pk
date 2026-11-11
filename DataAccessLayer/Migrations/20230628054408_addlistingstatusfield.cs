using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addlistingstatusfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "listingstatus",
                schema: "dbo",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "listingstatus",
                schema: "dbo",
                table: "PersonListings",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "listingstatus",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "listingstatus",
                schema: "dbo",
                table: "PersonListings");
        }
    }
}

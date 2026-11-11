using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updatepersonlisting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Accommodationtype",
                schema: "dbo",
                table: "PersonListings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Propertytype",
                schema: "dbo",
                table: "PersonListings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPersons",
                schema: "dbo",
                table: "PersonListings",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accommodationtype",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropColumn(
                name: "Propertytype",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropColumn(
                name: "TotalPersons",
                schema: "dbo",
                table: "PersonListings");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class removeFieldspersonlistingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accommodationtype",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropColumn(
                name: "Propertytype",
                schema: "dbo",
                table: "PersonListings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

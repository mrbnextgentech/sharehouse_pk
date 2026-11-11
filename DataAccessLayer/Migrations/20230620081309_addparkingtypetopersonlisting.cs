using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addparkingtypetopersonlisting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPersons",
                schema: "dbo",
                table: "PersonListings",
                newName: "Totalpersons");

            migrationBuilder.AddColumn<int>(
                name: "ParkingType",
                schema: "dbo",
                table: "PersonListings",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingType",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.RenameColumn(
                name: "Totalpersons",
                schema: "dbo",
                table: "PersonListings",
                newName: "TotalPersons");
        }
    }
}

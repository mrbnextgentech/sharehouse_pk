using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class UpdateRoomTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Private",
                schema: "dbo",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "bond",
                schema: "dbo",
                table: "Rooms",
                newName: "BondType");

            migrationBuilder.RenameColumn(
                name: "Tenant",
                schema: "dbo",
                table: "Rooms",
                newName: "Billstatus");

            migrationBuilder.AddColumn<int>(
                name: "PreferredFlatmate",
                schema: "dbo",
                table: "Rooms",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredFlatmate",
                schema: "dbo",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "BondType",
                schema: "dbo",
                table: "Rooms",
                newName: "bond");

            migrationBuilder.RenameColumn(
                name: "Billstatus",
                schema: "dbo",
                table: "Rooms",
                newName: "Tenant");

            migrationBuilder.AddColumn<bool>(
                name: "Private",
                schema: "dbo",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

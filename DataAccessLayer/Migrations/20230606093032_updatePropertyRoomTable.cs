using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updatePropertyRoomTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Daily",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "Instant",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "None",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "Nothanks",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.AddColumn<int>(
                name: "RoomType",
                schema: "dbo",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Accommodationtype",
                schema: "dbo",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bill",
                schema: "dbo",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bond",
                schema: "dbo",
                table: "Properties",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomType",
                schema: "dbo",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Accommodationtype",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Bill",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Bond",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.AddColumn<bool>(
                name: "Daily",
                schema: "dbo",
                table: "UserNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Email",
                schema: "dbo",
                table: "UserNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Instant",
                schema: "dbo",
                table: "UserNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "None",
                schema: "dbo",
                table: "UserNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Nothanks",
                schema: "dbo",
                table: "UserNotifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

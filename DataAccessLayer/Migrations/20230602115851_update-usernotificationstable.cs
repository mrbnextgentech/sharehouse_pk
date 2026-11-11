using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updateusernotificationstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "UserNotifications");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Status",
                schema: "dbo",
                table: "UserNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

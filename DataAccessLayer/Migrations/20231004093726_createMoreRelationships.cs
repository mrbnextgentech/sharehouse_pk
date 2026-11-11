using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class createMoreRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Rooms_RoomId",
                schema: "dbo",
                table: "Enquiries");

            migrationBuilder.DropForeignKey(
                name: "FK_UserNotifications_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_UpdatedbyUserID",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "UserNotifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Rooms_RoomId",
                schema: "dbo",
                table: "Enquiries",
                column: "RoomId",
                principalSchema: "dbo",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Rooms_RoomId",
                schema: "dbo",
                table: "Enquiries");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "UserNotifications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_UpdatedbyUserID",
                schema: "dbo",
                table: "UserNotifications",
                column: "UpdatedbyUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Rooms_RoomId",
                schema: "dbo",
                table: "Enquiries",
                column: "RoomId",
                principalSchema: "dbo",
                principalTable: "Rooms",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotifications_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "UserNotifications",
                column: "UpdatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

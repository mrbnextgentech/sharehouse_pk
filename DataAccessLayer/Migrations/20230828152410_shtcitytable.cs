using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class shtcitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Users_CreatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Users_UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CreatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedbyUserId",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CreatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedbyUserId",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedbyUserId",
                schema: "dbo",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedbyUserId1",
                schema: "dbo",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "Cities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "dbo",
                table: "Cities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedbyUserId",
                schema: "dbo",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CreatedbyUserId1",
                schema: "dbo",
                table: "Cities",
                column: "CreatedbyUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities",
                column: "UpdatedbyUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Users_CreatedbyUserId1",
                schema: "dbo",
                table: "Cities",
                column: "CreatedbyUserId1",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Users_UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities",
                column: "UpdatedbyUserId1",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

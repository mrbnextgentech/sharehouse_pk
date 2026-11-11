using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class editcitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StateID",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityID",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityID",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.AddColumn<long>(
                name: "CityID",
                schema: "dbo",
                table: "Persons",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StateID",
                schema: "dbo",
                table: "Cities",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "Cities",
                type: "datetime2",
                nullable: true);

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
                name: "IX_Persons_CityID",
                schema: "dbo",
                table: "Persons",
                column: "CityID");

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
                name: "FK_Cities_States_StateID",
                schema: "dbo",
                table: "Cities",
                column: "StateID",
                principalSchema: "dbo",
                principalTable: "States",
                principalColumn: "ID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Cities_CityID",
                schema: "dbo",
                table: "Persons",
                column: "CityID",
                principalSchema: "dbo",
                principalTable: "Cities",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_States_StateID",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Users_CreatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Users_UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Cities_CityID",
                schema: "dbo",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CityID",
                schema: "dbo",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CreatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_UpdatedbyUserId1",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityID",
                schema: "dbo",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
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

            migrationBuilder.AlterColumn<long>(
                name: "StateID",
                schema: "dbo",
                table: "Cities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                schema: "dbo",
                table: "Cities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityID",
                schema: "dbo",
                table: "Users",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_States_StateID",
                schema: "dbo",
                table: "Cities",
                column: "StateID",
                principalSchema: "dbo",
                principalTable: "States",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityID",
                schema: "dbo",
                table: "Users",
                column: "CityID",
                principalSchema: "dbo",
                principalTable: "Cities",
                principalColumn: "ID");
        }
    }
}

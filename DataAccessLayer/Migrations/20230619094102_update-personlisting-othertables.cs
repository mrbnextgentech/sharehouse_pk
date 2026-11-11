using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updatepersonlistingothertables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonListings_Users_CreatedbyUserId",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonListings_Users_UpdatedbyUserId",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropColumn(
                name: "EmploymentStatusID",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.RenameColumn(
                name: "UpdatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                newName: "UpdatedbyUserID");

            migrationBuilder.RenameColumn(
                name: "CreatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                newName: "CreatedbyUserID");

            migrationBuilder.RenameIndex(
                name: "IX_PersonListings_UpdatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                newName: "IX_PersonListings_UpdatedbyUserID");

            migrationBuilder.RenameIndex(
                name: "IX_PersonListings_CreatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                newName: "IX_PersonListings_CreatedbyUserID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "ListingLifestyles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "dbo",
                table: "ListingLifestyles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSelected",
                schema: "dbo",
                table: "ListingLifestyles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "dbo",
                table: "ListingEmployments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "dbo",
                table: "ListingEmployments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSelected",
                schema: "dbo",
                table: "ListingEmployments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ListingLifestyles_CreatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingEmployments_CreatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingEmployments_UpdatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments",
                column: "UpdatedbyUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingEmployments_Users_CreatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments",
                column: "CreatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingEmployments_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments",
                column: "UpdatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingLifestyles_Users_CreatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles",
                column: "CreatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonListings_Users_CreatedbyUserID",
                schema: "dbo",
                table: "PersonListings",
                column: "CreatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonListings_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "PersonListings",
                column: "UpdatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingEmployments_Users_CreatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingEmployments_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropForeignKey(
                name: "FK_ListingLifestyles_Users_CreatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonListings_Users_CreatedbyUserID",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonListings_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropIndex(
                name: "IX_ListingLifestyles_CreatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropIndex(
                name: "IX_ListingEmployments_CreatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropIndex(
                name: "IX_ListingEmployments_UpdatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropColumn(
                name: "CreatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropColumn(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropColumn(
                name: "isSelected",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropColumn(
                name: "CreatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropColumn(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropColumn(
                name: "isSelected",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.RenameColumn(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "PersonListings",
                newName: "UpdatedbyUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedbyUserID",
                schema: "dbo",
                table: "PersonListings",
                newName: "CreatedbyUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonListings_UpdatedbyUserID",
                schema: "dbo",
                table: "PersonListings",
                newName: "IX_PersonListings_UpdatedbyUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonListings_CreatedbyUserID",
                schema: "dbo",
                table: "PersonListings",
                newName: "IX_PersonListings_CreatedbyUserId");

            migrationBuilder.AddColumn<long>(
                name: "EmploymentStatusID",
                schema: "dbo",
                table: "ListingEmployments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonListings_Users_CreatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                column: "CreatedbyUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonListings_Users_UpdatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                column: "UpdatedbyUserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

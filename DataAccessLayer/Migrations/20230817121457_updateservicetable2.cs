using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updateservicetable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_CreatedbyUserID",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "dbo",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "UpdatedbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "updatedbyUserID");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                schema: "dbo",
                table: "Services",
                newName: "updatedOn");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "dbo",
                table: "Services",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "createdbyUserID");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "Services",
                newName: "createdOn");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "Services",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "dbo",
                table: "Services",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "Services",
                newName: "title");

            migrationBuilder.RenameIndex(
                name: "IX_Services_UpdatedbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "IX_Services_updatedbyUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Services_CreatedbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "IX_Services_createdbyUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_createdbyUserID",
                schema: "dbo",
                table: "Services",
                column: "createdbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_updatedbyUserID",
                schema: "dbo",
                table: "Services",
                column: "updatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_createdbyUserID",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_updatedbyUserID",
                schema: "dbo",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "updatedbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "UpdatedbyUserID");

            migrationBuilder.RenameColumn(
                name: "updatedOn",
                schema: "dbo",
                table: "Services",
                newName: "UpdatedOn");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "dbo",
                table: "Services",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "CreatedbyUserID");

            migrationBuilder.RenameColumn(
                name: "createdOn",
                schema: "dbo",
                table: "Services",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "dbo",
                table: "Services",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "title",
                schema: "dbo",
                table: "Services",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "status",
                schema: "dbo",
                table: "Services",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_Services_updatedbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "IX_Services_UpdatedbyUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Services_createdbyUserID",
                schema: "dbo",
                table: "Services",
                newName: "IX_Services_CreatedbyUserID");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "dbo",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_CreatedbyUserID",
                schema: "dbo",
                table: "Services",
                column: "CreatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_UpdatedbyUserID",
                schema: "dbo",
                table: "Services",
                column: "UpdatedbyUserID",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}

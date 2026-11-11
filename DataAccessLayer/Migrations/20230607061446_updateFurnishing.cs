using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updateFurnishing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FurnishingType",
                schema: "dbo",
                table: "Rooms",
                newName: "Furnishing");

            migrationBuilder.AddColumn<string>(
                name: "SvgIconPath",
                schema: "dbo",
                table: "RoomFurnishings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Svgd",
                schema: "dbo",
                table: "RoomFurnishings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SvgIconPath",
                schema: "dbo",
                table: "Furnishings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Svgd",
                schema: "dbo",
                table: "Furnishings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomFurnishings_RoomID",
                schema: "dbo",
                table: "RoomFurnishings",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFurnishings_Rooms_RoomID",
                schema: "dbo",
                table: "RoomFurnishings",
                column: "RoomID",
                principalSchema: "dbo",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFurnishings_Rooms_RoomID",
                schema: "dbo",
                table: "RoomFurnishings");

            migrationBuilder.DropIndex(
                name: "IX_RoomFurnishings_RoomID",
                schema: "dbo",
                table: "RoomFurnishings");

            migrationBuilder.DropColumn(
                name: "SvgIconPath",
                schema: "dbo",
                table: "RoomFurnishings");

            migrationBuilder.DropColumn(
                name: "Svgd",
                schema: "dbo",
                table: "RoomFurnishings");

            migrationBuilder.DropColumn(
                name: "SvgIconPath",
                schema: "dbo",
                table: "Furnishings");

            migrationBuilder.DropColumn(
                name: "Svgd",
                schema: "dbo",
                table: "Furnishings");

            migrationBuilder.RenameColumn(
                name: "Furnishing",
                schema: "dbo",
                table: "Rooms",
                newName: "FurnishingType");
        }
    }
}

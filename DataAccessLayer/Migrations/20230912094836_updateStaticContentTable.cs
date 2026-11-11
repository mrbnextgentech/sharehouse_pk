using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updateStaticContentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.DropColumn(
                name: "Page",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.DropColumn(
                name: "Section",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.DropColumn(
                name: "SubHeading",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "StaticContent",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SubHeading2",
                schema: "dbo",
                table: "StaticContent",
                newName: "LowersectionHeading");

            migrationBuilder.RenameColumn(
                name: "Image",
                schema: "dbo",
                table: "StaticContent",
                newName: "Topsection");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.AddColumn<int>(
                name: "ContentType",
                schema: "dbo",
                table: "StaticContent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Imagepath",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LowerSection",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.DropColumn(
                name: "Imagepath",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.DropColumn(
                name: "LowerSection",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "StaticContent",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Topsection",
                schema: "dbo",
                table: "StaticContent",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "LowersectionHeading",
                schema: "dbo",
                table: "StaticContent",
                newName: "SubHeading2");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Page",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Section",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubHeading",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(255)",
                nullable: true);
        }
    }
}

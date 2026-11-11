using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updateshortlisttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShortlistType",
                schema: "dbo",
                table: "Shortlist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Shortlist",
                schema: "dbo",
                table: "PropertiesViewed",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortlistType",
                schema: "dbo",
                table: "Shortlist");

            migrationBuilder.DropColumn(
                name: "Shortlist",
                schema: "dbo",
                table: "PropertiesViewed");
        }
    }
}

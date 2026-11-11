using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class ModifyLifestyleEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isSelected",
                schema: "dbo",
                table: "Lifestyles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isSelected",
                schema: "dbo",
                table: "Employments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isSelected",
                schema: "dbo",
                table: "Lifestyles");

            migrationBuilder.DropColumn(
                name: "isSelected",
                schema: "dbo",
                table: "Employments");
        }
    }
}

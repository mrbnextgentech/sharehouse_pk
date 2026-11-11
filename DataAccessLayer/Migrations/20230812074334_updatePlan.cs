using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updatePlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxHeading",
                schema: "dbo",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "MonthlyCharges",
                schema: "dbo",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "UpFrontPrice",
                schema: "dbo",
                table: "Plans",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "dbo",
                table: "Plans",
                newName: "PlanType");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "Plans",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Days",
                schema: "dbo",
                table: "Plans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Guaranteed",
                schema: "dbo",
                table: "Plans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                schema: "dbo",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "Guaranteed",
                schema: "dbo",
                table: "Plans");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "dbo",
                table: "Plans",
                newName: "UpFrontPrice");

            migrationBuilder.RenameColumn(
                name: "PlanType",
                schema: "dbo",
                table: "Plans",
                newName: "Type");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AddColumn<string>(
                name: "BoxHeading",
                schema: "dbo",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyCharges",
                schema: "dbo",
                table: "Plans",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

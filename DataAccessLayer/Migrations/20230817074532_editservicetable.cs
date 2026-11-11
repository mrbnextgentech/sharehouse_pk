using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class editservicetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanIDs",
                schema: "dbo",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "InPlan3",
                schema: "dbo",
                table: "Services",
                newName: "thirtydayguarantee");

            migrationBuilder.RenameColumn(
                name: "InPlan2",
                schema: "dbo",
                table: "Services",
                newName: "sendreceivemessage");

            migrationBuilder.RenameColumn(
                name: "InPlan1",
                schema: "dbo",
                table: "Services",
                newName: "seemobilenumber");

            migrationBuilder.AddColumn<int>(
                name: "listingsize",
                schema: "dbo",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "livepersonlisting",
                schema: "dbo",
                table: "Services",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "livepropertylisting",
                schema: "dbo",
                table: "Services",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "searchranking",
                schema: "dbo",
                table: "Services",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "topsearch",
                schema: "dbo",
                table: "Services",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "listingsize",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "livepersonlisting",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "livepropertylisting",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "searchranking",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "topsearch",
                schema: "dbo",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "thirtydayguarantee",
                schema: "dbo",
                table: "Services",
                newName: "InPlan3");

            migrationBuilder.RenameColumn(
                name: "sendreceivemessage",
                schema: "dbo",
                table: "Services",
                newName: "InPlan2");

            migrationBuilder.RenameColumn(
                name: "seemobilenumber",
                schema: "dbo",
                table: "Services",
                newName: "InPlan1");

            migrationBuilder.AddColumn<string>(
                name: "PlanIDs",
                schema: "dbo",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

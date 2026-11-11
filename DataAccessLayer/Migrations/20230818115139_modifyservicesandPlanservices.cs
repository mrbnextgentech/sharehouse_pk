using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class modifyservicesandPlanservices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "seemobilenumber",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "sendreceivemessage",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "thirtydayguarantee",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "topsearch",
                schema: "dbo",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "Servicetype",
                schema: "dbo",
                table: "PlanServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "listingsize",
                schema: "dbo",
                table: "PlanServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "livepersonlisting",
                schema: "dbo",
                table: "PlanServices",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "livepropertylisting",
                schema: "dbo",
                table: "PlanServices",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "searchranking",
                schema: "dbo",
                table: "PlanServices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "seemobilenumber",
                schema: "dbo",
                table: "PlanServices",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "sendreceivemessage",
                schema: "dbo",
                table: "PlanServices",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "thirtydayguarantee",
                schema: "dbo",
                table: "PlanServices",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "topsearch",
                schema: "dbo",
                table: "PlanServices",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Servicetype",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "listingsize",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "livepersonlisting",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "livepropertylisting",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "searchranking",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "seemobilenumber",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "sendreceivemessage",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "thirtydayguarantee",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "topsearch",
                schema: "dbo",
                table: "PlanServices");

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

            migrationBuilder.AddColumn<bool>(
                name: "seemobilenumber",
                schema: "dbo",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "sendreceivemessage",
                schema: "dbo",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "thirtydayguarantee",
                schema: "dbo",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "topsearch",
                schema: "dbo",
                table: "Services",
                type: "int",
                nullable: true);
        }
    }
}

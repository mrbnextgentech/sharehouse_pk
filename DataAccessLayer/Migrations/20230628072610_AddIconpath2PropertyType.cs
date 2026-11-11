using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class AddIconpath2PropertyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "PropertyTypes",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "iconPath",
                schema: "dbo",
                table: "PropertyTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ListingPropertyTypes",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    personListingid = table.Column<long>(type: "bigint", nullable: false),
                    propertytypeid = table.Column<long>(type: "bigint", nullable: false),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingPropertyTypes", x => x.id);
                    table.ForeignKey(
                        name: "FK_ListingPropertyTypes_PersonListings_personListingid",
                        column: x => x.personListingid,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingPropertyTypes_PropertyTypes_propertytypeid",
                        column: x => x.propertytypeid,
                        principalSchema: "dbo",
                        principalTable: "PropertyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingPropertyTypes_Users_CreatedbyUserID",
                        column: x => x.CreatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingPropertyTypes_CreatedbyUserID",
                schema: "dbo",
                table: "ListingPropertyTypes",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingPropertyTypes_personListingid",
                schema: "dbo",
                table: "ListingPropertyTypes",
                column: "personListingid");

            migrationBuilder.CreateIndex(
                name: "IX_ListingPropertyTypes_propertytypeid",
                schema: "dbo",
                table: "ListingPropertyTypes",
                column: "propertytypeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingPropertyTypes",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "iconPath",
                schema: "dbo",
                table: "PropertyTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "PropertyTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class createShorlistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shortlist",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<long>(type: "bigint", nullable: true),
                    PersonlistingId = table.Column<long>(type: "bigint", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shortlist", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Shortlist_PersonListings_PersonlistingId",
                        column: x => x.PersonlistingId,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Shortlist_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Shortlist_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shortlist_PersonlistingId",
                schema: "dbo",
                table: "Shortlist",
                column: "PersonlistingId");

            migrationBuilder.CreateIndex(
                name: "IX_Shortlist_PropertyId",
                schema: "dbo",
                table: "Shortlist",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Shortlist_UserId",
                schema: "dbo",
                table: "Shortlist",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shortlist",
                schema: "dbo");
        }
    }
}

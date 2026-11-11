using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class AddCasecadeDeleteShortlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shortlist_PersonListings_PersonlistingId",
                schema: "dbo",
                table: "Shortlist");

            migrationBuilder.AddForeignKey(
                name: "FK_Shortlist_PersonListings_PersonlistingId",
                schema: "dbo",
                table: "Shortlist",
                column: "PersonlistingId",
                principalSchema: "dbo",
                principalTable: "PersonListings",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shortlist_PersonListings_PersonlistingId",
                schema: "dbo",
                table: "Shortlist");

            migrationBuilder.AddForeignKey(
                name: "FK_Shortlist_PersonListings_PersonlistingId",
                schema: "dbo",
                table: "Shortlist",
                column: "PersonlistingId",
                principalSchema: "dbo",
                principalTable: "PersonListings",
                principalColumn: "ID");
        }
    }
}

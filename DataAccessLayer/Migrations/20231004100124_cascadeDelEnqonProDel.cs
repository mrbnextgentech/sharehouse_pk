using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class cascadeDelEnqonProDel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Properties_PropertyID",
                schema: "dbo",
                table: "Enquiries");

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Properties_PropertyID",
                schema: "dbo",
                table: "Enquiries",
                column: "PropertyID",
                principalSchema: "dbo",
                principalTable: "Properties",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict); // Change this to Restrict
        }

    }
}

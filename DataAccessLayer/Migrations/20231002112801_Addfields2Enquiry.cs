using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class Addfields2Enquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentEnquiryId",
                schema: "dbo",
                table: "Enquiries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isReplied",
                schema: "dbo",
                table: "Enquiries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentEnquiryId",
                schema: "dbo",
                table: "Enquiries");

            migrationBuilder.DropColumn(
                name: "isReplied",
                schema: "dbo",
                table: "Enquiries");
        }
    }
}

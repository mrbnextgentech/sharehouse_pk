using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class updateEmailtemplatestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailTemplates_EmailTemplateTypes_EmailTemplateTypeId",
                schema: "dbo",
                table: "EmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplates_EmailTemplateTypeId",
                schema: "dbo",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "EmailTemplateTypeId",
                schema: "dbo",
                table: "EmailTemplates");

            migrationBuilder.AddColumn<int>(
                name: "EmailTemplateType",
                schema: "dbo",
                table: "EmailTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailTemplateType",
                schema: "dbo",
                table: "EmailTemplates");

            migrationBuilder.AddColumn<long>(
                name: "EmailTemplateTypeId",
                schema: "dbo",
                table: "EmailTemplates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailTemplateTypeId",
                schema: "dbo",
                table: "EmailTemplates",
                column: "EmailTemplateTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailTemplates_EmailTemplateTypes_EmailTemplateTypeId",
                schema: "dbo",
                table: "EmailTemplates",
                column: "EmailTemplateTypeId",
                principalSchema: "dbo",
                principalTable: "EmailTemplateTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

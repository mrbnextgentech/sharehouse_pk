using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class createEmailTemplateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplateTypes",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplateTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailTemplateTypes_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailTemplateTypes_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailTemplateTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_EmailTemplateTypes_EmailTemplateTypeId",
                        column: x => x.EmailTemplateTypeId,
                        principalSchema: "dbo",
                        principalTable: "EmailTemplateTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailTemplates_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_CreatedbyUserId",
                schema: "dbo",
                table: "EmailTemplates",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EmailTemplateTypeId",
                schema: "dbo",
                table: "EmailTemplates",
                column: "EmailTemplateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_UpdatedbyUserId",
                schema: "dbo",
                table: "EmailTemplates",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplateTypes_CreatedbyUserId",
                schema: "dbo",
                table: "EmailTemplateTypes",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplateTypes_UpdatedbyUserId",
                schema: "dbo",
                table: "EmailTemplateTypes",
                column: "UpdatedbyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EmailTemplateTypes",
                schema: "dbo");
        }
    }
}

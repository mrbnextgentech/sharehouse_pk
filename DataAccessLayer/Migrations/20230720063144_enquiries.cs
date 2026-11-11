using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class enquiries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyEnquiries",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "Enquiries",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: true),
                    RoomId = table.Column<long>(type: "bigint", nullable: true),
                    PersonlistingId = table.Column<long>(type: "bigint", nullable: true),
                    Enquirytype = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enquirystatus = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enquiries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enquiries_PersonListings_PersonlistingId",
                        column: x => x.PersonlistingId,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Enquiries_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Enquiries_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "dbo",
                        principalTable: "Rooms",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Enquiries_Users_FromUserID",
                        column: x => x.FromUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Enquiries_Users_ToUserID",
                        column: x => x.ToUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Enquiries_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_FromUserID",
                schema: "dbo",
                table: "Enquiries",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_PersonlistingId",
                schema: "dbo",
                table: "Enquiries",
                column: "PersonlistingId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_PropertyID",
                schema: "dbo",
                table: "Enquiries",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_RoomId",
                schema: "dbo",
                table: "Enquiries",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_ToUserID",
                schema: "dbo",
                table: "Enquiries",
                column: "ToUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_UpdatedbyUserId",
                schema: "dbo",
                table: "Enquiries",
                column: "UpdatedbyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enquiries",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "PropertyEnquiries",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FromUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyID = table.Column<long>(type: "bigint", nullable: false),
                    RoomId = table.Column<long>(type: "bigint", nullable: true),
                    ToUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyEnquiries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyEnquiries_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyEnquiries_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "dbo",
                        principalTable: "Rooms",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PropertyEnquiries_Users_CreatedbyUserID",
                        column: x => x.CreatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyEnquiries_Users_FromUserID",
                        column: x => x.FromUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyEnquiries_Users_ToUserID",
                        column: x => x.ToUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PropertyEnquiries_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEnquiries_CreatedbyUserID",
                schema: "dbo",
                table: "PropertyEnquiries",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEnquiries_FromUserID",
                schema: "dbo",
                table: "PropertyEnquiries",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEnquiries_PropertyID",
                schema: "dbo",
                table: "PropertyEnquiries",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEnquiries_RoomId",
                schema: "dbo",
                table: "PropertyEnquiries",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEnquiries_ToUserID",
                schema: "dbo",
                table: "PropertyEnquiries",
                column: "ToUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEnquiries_UpdatedbyUserId",
                schema: "dbo",
                table: "PropertyEnquiries",
                column: "UpdatedbyUserId");
        }
    }
}

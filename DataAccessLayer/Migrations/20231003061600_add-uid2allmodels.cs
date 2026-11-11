using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class adduid2allmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "UserPlans",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "UserPayments",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "UserNotificationTypes",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "UserNotifications",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Testimonials",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Suburbs",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "StaticContent",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "States",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Sitesettings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Shortlist",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Services",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Rooms",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "RoomFurnishings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PropertyTypes",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PropertyInspections",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PropertyImages",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PropertyFeatures",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PropertyAcceptings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PropertiesViewed",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Properties",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PlanServices",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Plans",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Persons",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "PersonListings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Messages",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Meetings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "ListingPropertyTypes",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "ListingLifestyles",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "ListingEmployments",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Lifestyles",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Furnishings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Features",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "ErrorLogs",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Enquiries",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Employments",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "EmailTemplateTypes",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "EmailTemplates",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Cities",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                schema: "dbo",
                table: "Acceptings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonListingViewed",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    PersonlistingId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ViewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Shortlist = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonListingViewed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonListingViewed_PersonListings_PersonlistingId",
                        column: x => x.PersonlistingId,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PersonListingViewed_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPlans_UId",
                schema: "dbo",
                table: "UserPlans",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_UId",
                schema: "dbo",
                table: "UserPayments",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationTypes_UId",
                schema: "dbo",
                table: "UserNotificationTypes",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_UId",
                schema: "dbo",
                table: "UserNotifications",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_UId",
                schema: "dbo",
                table: "Testimonials",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Suburbs_UId",
                schema: "dbo",
                table: "Suburbs",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContent_UId",
                schema: "dbo",
                table: "StaticContent",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_States_UId",
                schema: "dbo",
                table: "States",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sitesettings_UId",
                schema: "dbo",
                table: "Sitesettings",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Shortlist_UId",
                schema: "dbo",
                table: "Shortlist",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UId",
                schema: "dbo",
                table: "Services",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_UId",
                schema: "dbo",
                table: "Rooms",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFurnishings_UId",
                schema: "dbo",
                table: "RoomFurnishings",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTypes_UId",
                schema: "dbo",
                table: "PropertyTypes",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspections_UId",
                schema: "dbo",
                table: "PropertyInspections",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_UId",
                schema: "dbo",
                table: "PropertyImages",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_UId",
                schema: "dbo",
                table: "PropertyFeatures",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAcceptings_UId",
                schema: "dbo",
                table: "PropertyAcceptings",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesViewed_UId",
                schema: "dbo",
                table: "PropertiesViewed",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UId",
                schema: "dbo",
                table: "Properties",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PlanServices_UId",
                schema: "dbo",
                table: "PlanServices",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_UId",
                schema: "dbo",
                table: "Plans",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UId",
                schema: "dbo",
                table: "Persons",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListings_UId",
                schema: "dbo",
                table: "PersonListings",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UId",
                schema: "dbo",
                table: "Messages",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_UId",
                schema: "dbo",
                table: "Meetings",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListingPropertyTypes_UId",
                schema: "dbo",
                table: "ListingPropertyTypes",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListingLifestyles_UId",
                schema: "dbo",
                table: "ListingLifestyles",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ListingEmployments_UId",
                schema: "dbo",
                table: "ListingEmployments",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Lifestyles_UId",
                schema: "dbo",
                table: "Lifestyles",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Furnishings_UId",
                schema: "dbo",
                table: "Furnishings",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Features_UId",
                schema: "dbo",
                table: "Features",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_UId",
                schema: "dbo",
                table: "ErrorLogs",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_UId",
                schema: "dbo",
                table: "Enquiries",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employments_UId",
                schema: "dbo",
                table: "Employments",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplateTypes_UId",
                schema: "dbo",
                table: "EmailTemplateTypes",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_UId",
                schema: "dbo",
                table: "EmailTemplates",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_UId",
                schema: "dbo",
                table: "Cities",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Acceptings_UId",
                schema: "dbo",
                table: "Acceptings",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListingViewed_PersonlistingId",
                schema: "dbo",
                table: "PersonListingViewed",
                column: "PersonlistingId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListingViewed_UId",
                schema: "dbo",
                table: "PersonListingViewed",
                column: "UId",
                unique: true,
                filter: "[UId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListingViewed_UserId",
                schema: "dbo",
                table: "PersonListingViewed",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonListingViewed",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_UserPlans_UId",
                schema: "dbo",
                table: "UserPlans");

            migrationBuilder.DropIndex(
                name: "IX_UserPayments_UId",
                schema: "dbo",
                table: "UserPayments");

            migrationBuilder.DropIndex(
                name: "IX_UserNotificationTypes_UId",
                schema: "dbo",
                table: "UserNotificationTypes");

            migrationBuilder.DropIndex(
                name: "IX_UserNotifications_UId",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.DropIndex(
                name: "IX_Testimonials_UId",
                schema: "dbo",
                table: "Testimonials");

            migrationBuilder.DropIndex(
                name: "IX_Suburbs_UId",
                schema: "dbo",
                table: "Suburbs");

            migrationBuilder.DropIndex(
                name: "IX_StaticContent_UId",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.DropIndex(
                name: "IX_States_UId",
                schema: "dbo",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Sitesettings_UId",
                schema: "dbo",
                table: "Sitesettings");

            migrationBuilder.DropIndex(
                name: "IX_Shortlist_UId",
                schema: "dbo",
                table: "Shortlist");

            migrationBuilder.DropIndex(
                name: "IX_Services_UId",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_UId",
                schema: "dbo",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RoomFurnishings_UId",
                schema: "dbo",
                table: "RoomFurnishings");

            migrationBuilder.DropIndex(
                name: "IX_PropertyTypes_UId",
                schema: "dbo",
                table: "PropertyTypes");

            migrationBuilder.DropIndex(
                name: "IX_PropertyInspections_UId",
                schema: "dbo",
                table: "PropertyInspections");

            migrationBuilder.DropIndex(
                name: "IX_PropertyImages_UId",
                schema: "dbo",
                table: "PropertyImages");

            migrationBuilder.DropIndex(
                name: "IX_PropertyFeatures_UId",
                schema: "dbo",
                table: "PropertyFeatures");

            migrationBuilder.DropIndex(
                name: "IX_PropertyAcceptings_UId",
                schema: "dbo",
                table: "PropertyAcceptings");

            migrationBuilder.DropIndex(
                name: "IX_PropertiesViewed_UId",
                schema: "dbo",
                table: "PropertiesViewed");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UId",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_PlanServices_UId",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropIndex(
                name: "IX_Plans_UId",
                schema: "dbo",
                table: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Persons_UId",
                schema: "dbo",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_PersonListings_UId",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UId",
                schema: "dbo",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_UId",
                schema: "dbo",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_ListingPropertyTypes_UId",
                schema: "dbo",
                table: "ListingPropertyTypes");

            migrationBuilder.DropIndex(
                name: "IX_ListingLifestyles_UId",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropIndex(
                name: "IX_ListingEmployments_UId",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropIndex(
                name: "IX_Lifestyles_UId",
                schema: "dbo",
                table: "Lifestyles");

            migrationBuilder.DropIndex(
                name: "IX_Furnishings_UId",
                schema: "dbo",
                table: "Furnishings");

            migrationBuilder.DropIndex(
                name: "IX_Features_UId",
                schema: "dbo",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_UId",
                schema: "dbo",
                table: "ErrorLogs");

            migrationBuilder.DropIndex(
                name: "IX_Enquiries_UId",
                schema: "dbo",
                table: "Enquiries");

            migrationBuilder.DropIndex(
                name: "IX_Employments_UId",
                schema: "dbo",
                table: "Employments");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplateTypes_UId",
                schema: "dbo",
                table: "EmailTemplateTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmailTemplates_UId",
                schema: "dbo",
                table: "EmailTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Cities_UId",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Acceptings_UId",
                schema: "dbo",
                table: "Acceptings");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "UserPlans");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "UserPayments");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "UserNotificationTypes");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "UserNotifications");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Suburbs");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "StaticContent");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "States");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Sitesettings");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Shortlist");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "RoomFurnishings");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PropertyTypes");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PropertyInspections");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PropertyImages");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PropertyFeatures");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PropertyAcceptings");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PropertiesViewed");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PlanServices");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "PersonListings");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "ListingPropertyTypes");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "ListingLifestyles");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "ListingEmployments");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Lifestyles");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Furnishings");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Enquiries");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Employments");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "EmailTemplateTypes");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "EmailTemplates");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "UId",
                schema: "dbo",
                table: "Acceptings");
        }
    }
}

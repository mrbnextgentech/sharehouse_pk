using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class createSharehousePkDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Acceptings",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acceptings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employments",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lifestyles",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lifestyles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Medium = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MediumID = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateID",
                        column: x => x.StateID,
                        principalSchema: "dbo",
                        principalTable: "States",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suburbs",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    CityID1 = table.Column<long>(type: "bigint", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suburbs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suburbs_Cities_CityID1",
                        column: x => x.CityID1,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreetoMessage = table.Column<bool>(type: "bit", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobileverified = table.Column<bool>(type: "bit", nullable: true),
                    Responserate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Lastloggedin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: true),
                    CityID = table.Column<long>(type: "bigint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CNIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AgreedtoTerms = table.Column<bool>(type: "bit", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityID",
                        column: x => x.CityID,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_States_StateID",
                        column: x => x.StateID,
                        principalSchema: "dbo",
                        principalTable: "States",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "DemoRequests",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DemoRequests_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemoRequests_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ErrorLogs",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoggedInUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    FiexedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorLogs_Users_FiexedByUserId",
                        column: x => x.FiexedByUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ErrorLogs_Users_LoggedInUserId",
                        column: x => x.LoggedInUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Features",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Features_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Features_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Furnishings",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnishings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Furnishings_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Furnishings_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpFrontPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoxHeading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tagline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Plans_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Plans_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyTypes_Users_CreatedbyUserID",
                        column: x => x.CreatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyTypes_Users_UpdatedbyUserID",
                        column: x => x.UpdatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanIDs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InPlan1 = table.Column<bool>(type: "bit", nullable: false),
                    InPlan2 = table.Column<bool>(type: "bit", nullable: false),
                    InPlan3 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Services_Users_CreatedbyUserID",
                        column: x => x.CreatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Users_UpdatedbyUserID",
                        column: x => x.UpdatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaticContent",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Page = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Heading = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    SubHeading = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    SubHeading2 = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StaticContent_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticContent_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Testimonials",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiniImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsShown = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testimonials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Testimonials_Users_CreatedbyUserID",
                        column: x => x.CreatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Testimonials_Users_UpdatedbyUserID",
                        column: x => x.UpdatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Toolkit",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsReplied = table.Column<bool>(type: "bit", nullable: false),
                    NeedFor = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toolkit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Toolkit_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBankAccounts",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AccountTitle = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    AccountNo = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CardHolderName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    CardNo = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IsSelectedForPayment = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBankAccounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBankAccounts_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBankAccounts_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBankAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonListings",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamsUp = table.Column<bool>(type: "bit", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyBudget = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MoveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Length = table.Column<int>(type: "int", nullable: true),
                    FurnishingType = table.Column<int>(type: "int", nullable: true),
                    InternetStatus = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    MaxNo = table.Column<int>(type: "int", nullable: true),
                    PlaceFor = table.Column<int>(type: "int", nullable: true),
                    Profilepic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: true),
                    EmpStatusID = table.Column<long>(type: "bigint", nullable: true),
                    Introduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserClicks = table.Column<long>(type: "bigint", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PlanId = table.Column<long>(type: "bigint", nullable: true),
                    PlanStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanExpired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonListings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonListings_Employments_EmpStatusID",
                        column: x => x.EmpStatusID,
                        principalSchema: "dbo",
                        principalTable: "Employments",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PersonListings_Plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "dbo",
                        principalTable: "Plans",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PersonListings_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonListings_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PersonListings_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyCode = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateID = table.Column<long>(type: "bigint", nullable: true),
                    CityID = table.Column<long>(type: "bigint", nullable: true),
                    Suburb = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Propertytype = table.Column<int>(type: "int", nullable: true),
                    TotalArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bed = table.Column<int>(type: "int", nullable: true),
                    Bathroom = table.Column<int>(type: "int", nullable: true),
                    Yearbuilt = table.Column<int>(type: "int", nullable: true),
                    VideoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopularTags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BondValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Rent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalParkings = table.Column<int>(type: "int", nullable: true),
                    ParkingSize = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    AdTitle = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    HidePrice = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Totalflatmates = table.Column<int>(type: "int", nullable: true),
                    AboutFlatmates = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    GoogleMapEmbedAPIURL = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    HideAddress = table.Column<bool>(type: "bit", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ownermobile = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Owneremail = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRented = table.Column<bool>(type: "bit", nullable: false),
                    RoomAvailable = table.Column<bool>(type: "bit", nullable: false),
                    WomenOnlyProperty = table.Column<bool>(type: "bit", nullable: false),
                    IsOnwnerOrAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    ShowPhoneNo = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAdlive = table.Column<bool>(type: "bit", nullable: false),
                    SharePhone = table.Column<bool>(type: "bit", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserClicks = table.Column<long>(type: "bigint", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Parkingtype = table.Column<int>(type: "int", nullable: true),
                    Flatmatepreference = table.Column<int>(type: "int", nullable: true),
                    MinStay = table.Column<int>(type: "int", nullable: true),
                    MaxStay = table.Column<int>(type: "int", nullable: true),
                    InternetStatus = table.Column<int>(type: "int", nullable: true),
                    PlanId = table.Column<long>(type: "bigint", nullable: true),
                    PlanStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanExpired = table.Column<bool>(type: "bit", nullable: false),
                    showInspectionTime = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdCreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Properties_Cities_CityID",
                        column: x => x.CityID,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Properties_Plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "dbo",
                        principalTable: "Plans",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Properties_States_StateID",
                        column: x => x.StateID,
                        principalSchema: "dbo",
                        principalTable: "States",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Properties_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Properties_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanServices",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanID = table.Column<long>(type: "bigint", nullable: false),
                    ServiceID = table.Column<long>(type: "bigint", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlanServices_Plans_PlanID",
                        column: x => x.PlanID,
                        principalSchema: "dbo",
                        principalTable: "Plans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanServices_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalSchema: "dbo",
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanServices_Users_CreatedbyUserID",
                        column: x => x.CreatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlanServices_Users_UpdatedbyUserID",
                        column: x => x.UpdatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ListingEmployments",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonListingID = table.Column<long>(type: "bigint", nullable: false),
                    EmploymentStatusID = table.Column<long>(type: "bigint", nullable: false),
                    EmploymentID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingEmployments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListingEmployments_Employments_EmploymentID",
                        column: x => x.EmploymentID,
                        principalSchema: "dbo",
                        principalTable: "Employments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingEmployments_PersonListings_PersonListingID",
                        column: x => x.PersonListingID,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListingLifestyles",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonListingID = table.Column<long>(type: "bigint", nullable: false),
                    LifestyleID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingLifestyles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ListingLifestyles_Lifestyles_LifestyleID",
                        column: x => x.LifestyleID,
                        principalSchema: "dbo",
                        principalTable: "Lifestyles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingLifestyles_PersonListings_PersonListingID",
                        column: x => x.PersonListingID,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonListingId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Partner = table.Column<bool>(type: "bit", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Persons_PersonListings_PersonListingId",
                        column: x => x.PersonListingId,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sent = table.Column<bool>(type: "bit", nullable: false),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    PropertyListingId = table.Column<long>(type: "bigint", nullable: true),
                    PersonListingId = table.Column<long>(type: "bigint", nullable: true),
                    PropertyID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_PersonListings_PersonListingId",
                        column: x => x.PersonListingId,
                        principalSchema: "dbo",
                        principalTable: "PersonListings",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Messages_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Messages_Users_FromUserID",
                        column: x => x.FromUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ToUserID",
                        column: x => x.ToUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertiesViewed",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ViewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertiesViewed", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertiesViewed_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertiesViewed_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PropertyAcceptings",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: false),
                    AcceptingID = table.Column<long>(type: "bigint", nullable: true),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyAcceptings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyAcceptings_Acceptings_AcceptingID",
                        column: x => x.AcceptingID,
                        principalSchema: "dbo",
                        principalTable: "Acceptings",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PropertyAcceptings_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyAcceptings_Users_CreatedbyUserID",
                        column: x => x.CreatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyAcceptings_Users_UpdatedbyUserID",
                        column: x => x.UpdatedbyUserID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertyFeatures",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: false),
                    FeatureID = table.Column<long>(type: "bigint", nullable: true),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeatures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Features_FeatureID",
                        column: x => x.FeatureID,
                        principalSchema: "dbo",
                        principalTable: "Features",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImageType = table.Column<int>(type: "int", nullable: true),
                    FloorNo = table.Column<int>(type: "int", nullable: true),
                    isCover = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyImages_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertyInspections",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    From = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyInspections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyInspections_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyInspections_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PropertyInspections_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoomFurnishings",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: true),
                    RoomID = table.Column<long>(type: "bigint", nullable: false),
                    FurnishingID = table.Column<long>(type: "bigint", nullable: true),
                    isSelected = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomFurnishings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoomFurnishings_Furnishings_FurnishingID",
                        column: x => x.FurnishingID,
                        principalSchema: "dbo",
                        principalTable: "Furnishings",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RoomFurnishings_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_RoomFurnishings_Users_CreatedbyUserId",
                        column: x => x.CreatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoomFurnishings_Users_UpdatedbyUserId",
                        column: x => x.UpdatedbyUserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    FurnishingType = table.Column<int>(type: "int", nullable: false),
                    BathroomType = table.Column<int>(type: "int", nullable: false),
                    BedSize = table.Column<int>(type: "int", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rent = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinStay = table.Column<int>(type: "int", nullable: true),
                    MaxStay = table.Column<int>(type: "int", nullable: true),
                    Tenant = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    bond = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rooms_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "dbo",
                        principalTable: "Properties",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyEnquiries",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<long>(type: "bigint", nullable: false),
                    RoomId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ToUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedbyUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedbyUserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Cities_StateID",
                schema: "dbo",
                table: "Cities",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_DemoRequests_CreatedbyUserId",
                schema: "dbo",
                table: "DemoRequests",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DemoRequests_UpdatedbyUserId",
                schema: "dbo",
                table: "DemoRequests",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_FiexedByUserId",
                schema: "dbo",
                table: "ErrorLogs",
                column: "FiexedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_LoggedInUserId",
                schema: "dbo",
                table: "ErrorLogs",
                column: "LoggedInUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_CreatedbyUserId",
                schema: "dbo",
                table: "Features",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Features_UpdatedbyUserId",
                schema: "dbo",
                table: "Features",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Furnishings_CreatedbyUserId",
                schema: "dbo",
                table: "Furnishings",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Furnishings_UpdatedbyUserId",
                schema: "dbo",
                table: "Furnishings",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingEmployments_EmploymentID",
                schema: "dbo",
                table: "ListingEmployments",
                column: "EmploymentID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingEmployments_PersonListingID",
                schema: "dbo",
                table: "ListingEmployments",
                column: "PersonListingID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingLifestyles_LifestyleID",
                schema: "dbo",
                table: "ListingLifestyles",
                column: "LifestyleID");

            migrationBuilder.CreateIndex(
                name: "IX_ListingLifestyles_PersonListingID",
                schema: "dbo",
                table: "ListingLifestyles",
                column: "PersonListingID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FromUserID",
                schema: "dbo",
                table: "Messages",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PersonListingId",
                schema: "dbo",
                table: "Messages",
                column: "PersonListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PropertyID",
                schema: "dbo",
                table: "Messages",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ToUserID",
                schema: "dbo",
                table: "Messages",
                column: "ToUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListings_CreatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListings_EmpStatusID",
                schema: "dbo",
                table: "PersonListings",
                column: "EmpStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListings_PlanId",
                schema: "dbo",
                table: "PersonListings",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListings_UpdatedbyUserId",
                schema: "dbo",
                table: "PersonListings",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonListings_UserId",
                schema: "dbo",
                table: "PersonListings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonListingId",
                schema: "dbo",
                table: "Persons",
                column: "PersonListingId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CreatedbyUserId",
                schema: "dbo",
                table: "Plans",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_UpdatedbyUserId",
                schema: "dbo",
                table: "Plans",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanServices_CreatedbyUserID",
                schema: "dbo",
                table: "PlanServices",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanServices_PlanID",
                schema: "dbo",
                table: "PlanServices",
                column: "PlanID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanServices_ServiceID",
                schema: "dbo",
                table: "PlanServices",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_PlanServices_UpdatedbyUserID",
                schema: "dbo",
                table: "PlanServices",
                column: "UpdatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CityID",
                schema: "dbo",
                table: "Properties",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CreatedbyUserId",
                schema: "dbo",
                table: "Properties",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                schema: "dbo",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_PlanId",
                schema: "dbo",
                table: "Properties",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_StateID",
                schema: "dbo",
                table: "Properties",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UpdatedbyUserId",
                schema: "dbo",
                table: "Properties",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesViewed_PropertyID",
                schema: "dbo",
                table: "PropertiesViewed",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertiesViewed_UserID",
                schema: "dbo",
                table: "PropertiesViewed",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAcceptings_AcceptingID",
                schema: "dbo",
                table: "PropertyAcceptings",
                column: "AcceptingID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAcceptings_CreatedbyUserID",
                schema: "dbo",
                table: "PropertyAcceptings",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAcceptings_PropertyID",
                schema: "dbo",
                table: "PropertyAcceptings",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyAcceptings_UpdatedbyUserID",
                schema: "dbo",
                table: "PropertyAcceptings",
                column: "UpdatedbyUserID");

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

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_CreatedbyUserId",
                schema: "dbo",
                table: "PropertyFeatures",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_FeatureID",
                schema: "dbo",
                table: "PropertyFeatures",
                column: "FeatureID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_PropertyID",
                schema: "dbo",
                table: "PropertyFeatures",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_UpdatedbyUserId",
                schema: "dbo",
                table: "PropertyFeatures",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_CreatedbyUserId",
                schema: "dbo",
                table: "PropertyImages",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_PropertyID",
                schema: "dbo",
                table: "PropertyImages",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_UpdatedbyUserId",
                schema: "dbo",
                table: "PropertyImages",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspections_CreatedbyUserId",
                schema: "dbo",
                table: "PropertyInspections",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspections_PropertyID",
                schema: "dbo",
                table: "PropertyInspections",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyInspections_UpdatedbyUserId",
                schema: "dbo",
                table: "PropertyInspections",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTypes_CreatedbyUserID",
                schema: "dbo",
                table: "PropertyTypes",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyTypes_UpdatedbyUserID",
                schema: "dbo",
                table: "PropertyTypes",
                column: "UpdatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "dbo",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFurnishings_CreatedbyUserId",
                schema: "dbo",
                table: "RoomFurnishings",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFurnishings_FurnishingID",
                schema: "dbo",
                table: "RoomFurnishings",
                column: "FurnishingID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFurnishings_PropertyID",
                schema: "dbo",
                table: "RoomFurnishings",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomFurnishings_UpdatedbyUserId",
                schema: "dbo",
                table: "RoomFurnishings",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PropertyId",
                schema: "dbo",
                table: "Rooms",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatedbyUserID",
                schema: "dbo",
                table: "Services",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UpdatedbyUserID",
                schema: "dbo",
                table: "Services",
                column: "UpdatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContent_CreatedbyUserId",
                schema: "dbo",
                table: "StaticContent",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContent_UpdatedbyUserId",
                schema: "dbo",
                table: "StaticContent",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Suburbs_CityID1",
                schema: "dbo",
                table: "Suburbs",
                column: "CityID1");

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_CreatedbyUserID",
                schema: "dbo",
                table: "Testimonials",
                column: "CreatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Testimonials_UpdatedbyUserID",
                schema: "dbo",
                table: "Testimonials",
                column: "UpdatedbyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Toolkit_UpdatedbyUserId",
                schema: "dbo",
                table: "Toolkit",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBankAccounts_CreatedbyUserId",
                schema: "dbo",
                table: "UserBankAccounts",
                column: "CreatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBankAccounts_UpdatedbyUserId",
                schema: "dbo",
                table: "UserBankAccounts",
                column: "UpdatedbyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBankAccounts_UserId",
                schema: "dbo",
                table: "UserBankAccounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "dbo",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "dbo",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "dbo",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityID",
                schema: "dbo",
                table: "Users",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StateID",
                schema: "dbo",
                table: "Users",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoRequests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ErrorLogs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ListingEmployments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ListingLifestyles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Meetings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PlanServices",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertiesViewed",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyAcceptings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyEnquiries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyFeatures",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyImages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyInspections",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PropertyTypes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoomFurnishings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StaticContent",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Suburbs",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Testimonials",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Toolkit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserBankAccounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Lifestyles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonListings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Acceptings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Features",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Furnishings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Employments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Plans",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "States",
                schema: "dbo");
        }
    }
}

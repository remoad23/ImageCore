using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageCore.Migrations
{
    public partial class InitialCreateImageCoreDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    image = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ContactUserId = table.Column<string>(type: "TEXT", nullable: false),
                    RequestValidated = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contact_Users_ContactUserId",
                        column: x => x.ContactUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Views = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageLayer",
                columns: table => new
                {
                    ImageLayerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    MaskMat = table.Column<string>(type: "TEXT", nullable: true),
                    X = table.Column<float>(type: "REAL", nullable: false),
                    Y = table.Column<float>(type: "REAL", nullable: false),
                    Z = table.Column<float>(type: "REAL", nullable: false),
                    Opacity = table.Column<byte>(type: "INTEGER", nullable: false),
                    Visible = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    LayerType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageLayer", x => x.ImageLayerId);
                    table.ForeignKey(
                        name: "FK_ImageLayer_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectParticipator",
                columns: table => new
                {
                    ProjectParticipatorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectParticipator", x => x.ProjectParticipatorId);
                    table.ForeignKey(
                        name: "FK_ProjectParticipator_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectParticipator_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    FilterId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilterType = table.Column<string>(type: "TEXT", nullable: true),
                    ImageLayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.FilterId);
                    table.ForeignKey(
                        name: "FK_Filters_ImageLayer_ImageLayerId",
                        column: x => x.ImageLayerId,
                        principalTable: "ImageLayer",
                        principalColumn: "ImageLayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filters_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageComponent",
                columns: table => new
                {
                    ImageComponentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocalX = table.Column<float>(type: "REAL", nullable: false),
                    LocalY = table.Column<float>(type: "REAL", nullable: false),
                    LocalZ = table.Column<float>(type: "REAL", nullable: false),
                    ColorMat = table.Column<string>(type: "TEXT", nullable: true),
                    ImageLayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageComponent", x => x.ImageComponentId);
                    table.ForeignKey(
                        name: "FK_ImageComponent_ImageLayer_ImageLayerId",
                        column: x => x.ImageLayerId,
                        principalTable: "ImageLayer",
                        principalColumn: "ImageLayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageComponent_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5", "25405133-585c-4ea0-acda-c8f7906568d7", "ProjectOwner", "PROJECTOWNER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", "e29d246d-b665-43dd-9fac-943950913d2d", "ProjectViewer", "PROJECTVIEWER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "21cbc744-a0f7-4f11-bfe1-7dbede884626", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "d0a4f625-6aae-45d5-ad26-2399a4f04455", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4", "eb286ce9-eadd-4433-9164-f491a67da189", "ProjectEditor", "PROJECTEDITOR" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "1aad9d36-e054-4212-9101-20eb00992580", 0, "a8b5a43c-cbb2-4793-80fa-31e6312fe73d", "user20@gmail.com", false, false, null, "USER20@GMAIL.COM", "USER20", "AQAAAAEAACcQAAAAEM08NSg3nQGHHnGC/5qHFogR0E0gtR03cJ5ZFoIXaMr8pRSR5zpEJT3/wJcf4RG0pA==", null, false, "8c2c1fd1-821a-412f-835f-311ac3283434", false, "User20", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "b96b93d1-7575-43fa-87d0-6adbd365e2ac", 0, "f1bdc058-fdef-4376-9fb9-24f0589f4ae3", "user19@gmail.com", false, false, null, "USER19@GMAIL.COM", "USER19", "AQAAAAEAACcQAAAAEOMOvX6gXJMiVCUpVoULd06anA7I7LIEhQvkQwWk85RWO/z7FlGOQCllsYRbZnlurQ==", null, false, "638484b9-050e-4523-93c4-035c5fc51d3d", false, "User19", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "f016ae25-8371-41dd-af1b-17e897e992d0", 0, "90eb5e3c-0c0e-408c-b4fb-4379ea5c7a74", "user18@gmail.com", false, false, null, "USER18@GMAIL.COM", "USER18", "AQAAAAEAACcQAAAAEPZCf9pB6TCRxrChs9PizA7OLM3KuqhbyWhtbUYHzMM0xIWvwC4tWp9O6pH/7nNaoA==", null, false, "3c5e426e-7762-4dee-9683-a8ec10b4b41e", false, "User18", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "49b359a1-fe4c-4d10-a373-84e7359f4ff5", 0, "825a1a91-843e-41e6-bf72-d945ce9e9421", "user17@gmail.com", false, false, null, "USER17@GMAIL.COM", "USER17", "AQAAAAEAACcQAAAAEOTo9+Qot3g+UJm5AfxYoocZyCmRXvjFO4UODq77oV1r2rCSHi1O3Z2fw1sEQQDXsg==", null, false, "3d7900f0-8c17-4e25-bb51-dfc4b3adc6d6", false, "User17", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "e8adcd93-bffc-480f-a629-2e47e2d2e0c8", 0, "d9e2de5d-2156-42b0-8a65-010baeaedc77", "user16@gmail.com", false, false, null, "USER16@GMAIL.COM", "USER16", "AQAAAAEAACcQAAAAEORkywspYXmlETsuLzqyoW6THCVrqpY/vi2N1riClQmFS3l7xgCbZt+yIo2OMmlXHA==", null, false, "9170c13c-6872-4b52-a385-3a777c8a8ebd", false, "User16", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "8b3c7199-a432-456e-bb4e-82ddea6ff860", 0, "fec15b4c-0584-4b24-a3fa-2c5f2fda45b4", "user15@gmail.com", false, false, null, "USER15@GMAIL.COM", "USER15", "AQAAAAEAACcQAAAAEM/DSLhfAc6ykN7EhdYIuSlKzfxknes0CGtTY+dsE2OZzhb6DH8uTfYqSULHbAexcA==", null, false, "1ed99f70-e294-4ccd-8cd9-ede4efb23561", false, "User15", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "e1f9f19b-6d2e-4169-92f4-fc3043d9e4f8", 0, "2971bf5e-1de0-43ec-9ed1-2e54bdb6d05d", "user14@gmail.com", false, false, null, "USER14@GMAIL.COM", "USER14", "AQAAAAEAACcQAAAAECLeLiAi2Z+gUfq0084R1d4IfYX4FP1Qqhmj/ogCLTMqQewOLlY4ywTcb8bfVXiVLg==", null, false, "eea0ed7e-5ad3-4e5a-8473-75d6d7d62f53", false, "User14", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "3d8bbf5b-7501-4c5d-89ec-4128b13861f9", 0, "834cc435-74c3-42ad-aebe-7f0cfb46946e", "user13@gmail.com", false, false, null, "USER13@GMAIL.COM", "USER13", "AQAAAAEAACcQAAAAEHcriJ5yjqB0UtyD4bkcNW6Ouj1Tv8uzA0/o+bGsqr45FZGPeM1cIlxVncHajxkocw==", null, false, "e89aee7c-e094-4b35-aa6c-8c528482d0f0", false, "User13", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "76a351fa-e379-49dc-9262-db8df6b73c47", 0, "c9ba0164-321a-465d-9991-1bd895a2a972", "user11@gmail.com", false, false, null, "USER11@GMAIL.COM", "USER11", "AQAAAAEAACcQAAAAEM2u0yKrgE/q8gZdIUM8C9q1oEbThJGudHTq5rdDIwnNRkibVyGyUgWbbZbIxm+f4Q==", null, false, "abcf769c-3033-4504-a6e1-c03221610d4e", false, "User11", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "28566d0f-036e-4769-b293-65ddcf8b58ce", 0, "94a3236d-50e5-41b0-bbdb-47c257703765", "user10@gmail.com", false, false, null, "USER10@GMAIL.COM", "USER10", "AQAAAAEAACcQAAAAEOJeQpqsU07W1EykP1JZW5wjVlqaJiHVgnzWa8YbW8Aui5Uxpe7qlYrZt9E+VLbMPg==", null, false, "7976a5d7-e555-4bc7-94d8-5d36454c0861", false, "User10", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "d8aa6119-148d-4020-bbdd-89636079cb6b", 0, "5bff995f-21dc-46b8-a58c-4cf8a3ef8f82", "user9@gmail.com", false, false, null, "USER9@GMAIL.COM", "USER9", "AQAAAAEAACcQAAAAEH0kIhhHF/Ykk1gLt6Zb49DvECAp3kIuceP5fY6ZgL7zmTYzrlFbeZjoHLeZ2LB0FA==", null, false, "16508379-f729-4509-a97d-a15fd8081e95", false, "User9", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "aa63441a-ab37-42ec-bd14-d0e8435c9063", 0, "a49493ce-9d22-4b7a-8036-51250af29ea0", "user8@gmail.com", false, false, null, "USER8@GMAIL.COM", "USER8", "AQAAAAEAACcQAAAAEAOZ6kw1CzkjqCBz6V0V4SVnYRtznz4k3IbUQHksq8C6viksCjAEjXIUucTqkwtEbQ==", null, false, "f5bb3572-894c-4e40-8526-5c6ccfb119d6", false, "User8", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "f51f52bc-dc76-4e94-ab1d-042207652a36", 0, "ba0d1060-794a-4c41-b84b-f1efb9723173", "user7@gmail.com", false, false, null, "USER7@GMAIL.COM", "USER7", "AQAAAAEAACcQAAAAEMKdQ5y+PrsHXBbhxDMLjkuClHAqyFz01Izl0jLxSAV9m/w3b9iU9RiNzq8+uTsO/g==", null, false, "7bb6128b-41cb-4cac-8bc3-97e9e763f4be", false, "User7", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "fe53f15b-16a7-4909-8357-7859d1249914", 0, "42e3918f-dd5b-4cc4-9813-d7103670ef9b", "user6@gmail.com", false, false, null, "USER6@GMAIL.COM", "USER6", "AQAAAAEAACcQAAAAEPGpci0MOwTzSWO/uzUGlCkyu6L5jxq9oPVkxFqcCg8N622D0w3ZJ7YLqNXM7PBTjA==", null, false, "df4189db-766b-4617-8184-9a1d7d93e56c", false, "User6", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "2af9bcf3-fc5c-4f46-aaab-c345a42bc7f0", 0, "17443070-1052-4b26-915e-0095b8548aa3", "user5@gmail.com", false, false, null, "USER5@GMAIL.COM", "USER5", "AQAAAAEAACcQAAAAEHwcuuX8486oV0F4tJ8nHOskvljOFtTCCAjHUmg2z5nFPHrddZ2k9QaM3UN8Uv3L2Q==", null, false, "052bae42-2473-45ef-8ffb-0b221be4c256", false, "User5", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "96780c5c-df36-44db-a1cd-69b1f4dce2a5", 0, "f9081657-d92b-4326-9e2a-8e3e28643f71", "user4@gmail.com", false, false, null, "USER4@GMAIL.COM", "USER4", "AQAAAAEAACcQAAAAEGU3NebGjSVXhJeBmLAlkII/jCI3+t2IYKynq1BZj2i5dn+wUJnhEbTyXAzUK1E0Kw==", null, false, "b6f4a44c-7092-442c-b3ee-da144de0947e", false, "User4", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "efacdc5e-9dc6-4d69-90b7-421dd35e7f12", 0, "62116ff1-741e-46ec-bd39-29fbdcbfcd54", "imagecoreuser@gmail.com", false, false, null, "IMAGECOREUSER@GMAIL.COM", "USER3", "AQAAAAEAACcQAAAAENTw+ZZFhrDPj80qt2arFHXRLfb0d1m8pLonkRkgaorUe4PBMr+/952H0zFQUzMvgA==", null, false, "fc72ffc6-e71c-45ad-b726-f6d0b5eaa6c6", false, "User3", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "2c8d5bb0-1ad3-4046-babe-aac833d12390", 0, "53ae892b-0658-4d04-8a92-b388cd842ac3", "imagecore24@gmail.com", false, false, null, "IMAGECORE24@GMAIL.COM", "USER2", "AQAAAAEAACcQAAAAEDcftLqaZVec3WgfGQ61lbzdnlavdK/1Esk8MsZI/1kNSg1GBYbmxDhGsmVlxDzc7w==", null, false, "6520c2bb-7981-4083-9b34-b03990264f1a", false, "User2", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "32990fc4-6d50-431b-a07a-070d0807ff23", 0, "54e3a98e-bd1d-4832-ac2f-61ac9d461bba", "user12@gmail.com", false, false, null, "USER12@GMAIL.COM", "USER12", "AQAAAAEAACcQAAAAEBd4L/YoTYVIM6ShPcwcZx52oFUm+hOHmfrDH9n3pPddAf7VL/DtqRNjPN7vGN4BjA==", null, false, "9cc61e5d-1c2f-4094-9085-d113368e692e", false, "User12", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "f418e844-e717-4435-b034-8c4a20d9f8c7", 0, "bf537cf3-c567-4aff-9a44-854a3fbfe4b7", "imagecore23@gmail.com", false, false, null, "IMAGECORE23@GMAIL.COM", "USER1", "AQAAAAEAACcQAAAAEOgxtFgtQdj26I8oScDdEItkqLK+eGtlG4Jmol6c+Pa7AxsavuKXNbVja3kJdVUdng==", null, false, "35618aa7-398b-4b82-8fef-3f4a11bddf93", false, "User1", null });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 1, "Projekt 1", "f418e844-e717-4435-b034-8c4a20d9f8c7", 4355 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 3, "Projekt 3", "f418e844-e717-4435-b034-8c4a20d9f8c7", 345 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 4, "Projekt 4", "2c8d5bb0-1ad3-4046-babe-aac833d12390", 2 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 5, "Projekt 5", "efacdc5e-9dc6-4d69-90b7-421dd35e7f12", 25 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 2, "Projekt 2", "96780c5c-df36-44db-a1cd-69b1f4dce2a5", 4 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "2c8d5bb0-1ad3-4046-babe-aac833d12390" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "f418e844-e717-4435-b034-8c4a20d9f8c7" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1aad9d36-e054-4212-9101-20eb00992580" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "b96b93d1-7575-43fa-87d0-6adbd365e2ac" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "f016ae25-8371-41dd-af1b-17e897e992d0" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "49b359a1-fe4c-4d10-a373-84e7359f4ff5" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e8adcd93-bffc-480f-a629-2e47e2d2e0c8" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "8b3c7199-a432-456e-bb4e-82ddea6ff860" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e1f9f19b-6d2e-4169-92f4-fc3043d9e4f8" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "32990fc4-6d50-431b-a07a-070d0807ff23" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "efacdc5e-9dc6-4d69-90b7-421dd35e7f12" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "76a351fa-e379-49dc-9262-db8df6b73c47" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "28566d0f-036e-4769-b293-65ddcf8b58ce" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "d8aa6119-148d-4020-bbdd-89636079cb6b" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "aa63441a-ab37-42ec-bd14-d0e8435c9063" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "f51f52bc-dc76-4e94-ab1d-042207652a36" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "fe53f15b-16a7-4909-8357-7859d1249914" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "2af9bcf3-fc5c-4f46-aaab-c345a42bc7f0" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "3d8bbf5b-7501-4c5d-89ec-4128b13861f9" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "96780c5c-df36-44db-a1cd-69b1f4dce2a5" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 1, 1, "f418e844-e717-4435-b034-8c4a20d9f8c7" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 2, 1, "2c8d5bb0-1ad3-4046-babe-aac833d12390" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 3, 1, "efacdc5e-9dc6-4d69-90b7-421dd35e7f12" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 6, 3, "f418e844-e717-4435-b034-8c4a20d9f8c7" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 7, 4, "f418e844-e717-4435-b034-8c4a20d9f8c7" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 8, 4, "2c8d5bb0-1ad3-4046-babe-aac833d12390" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 9, 4, "efacdc5e-9dc6-4d69-90b7-421dd35e7f12" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 10, 4, "96780c5c-df36-44db-a1cd-69b1f4dce2a5" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 11, 5, "f418e844-e717-4435-b034-8c4a20d9f8c7" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 12, 5, "2c8d5bb0-1ad3-4046-babe-aac833d12390" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 13, 5, "efacdc5e-9dc6-4d69-90b7-421dd35e7f12" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 14, 5, "96780c5c-df36-44db-a1cd-69b1f4dce2a5" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 15, 5, "2af9bcf3-fc5c-4f46-aaab-c345a42bc7f0" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 4, 2, "2c8d5bb0-1ad3-4046-babe-aac833d12390" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { 5, 2, "96780c5c-df36-44db-a1cd-69b1f4dce2a5" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactUserId",
                table: "Contact",
                column: "ContactUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ImageLayerId",
                table: "Filters",
                column: "ImageLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ProjectId",
                table: "Filters",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageComponent_ImageLayerId",
                table: "ImageComponent",
                column: "ImageLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageComponent_ProjectId",
                table: "ImageComponent",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageLayer_ProjectId",
                table: "ImageLayer",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectParticipator_ProjectId",
                table: "ProjectParticipator",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectParticipator_UserId",
                table: "ProjectParticipator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "ImageComponent");

            migrationBuilder.DropTable(
                name: "ProjectParticipator");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "ImageLayer");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

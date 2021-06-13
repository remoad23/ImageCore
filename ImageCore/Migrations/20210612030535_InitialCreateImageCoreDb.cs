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
                values: new object[] { "5", "c0db0042-3f63-4c00-83fd-6eee2a7c4d38", "ProjectOwner", "PROJECTOWNER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", "2dab0604-3b3d-444b-b8f0-cb7ad7268003", "ProjectViewer", "PROJECTVIEWER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2", "ec0599a2-3af7-4554-bde9-1721bf7e1447", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", "c7b37ddf-1b26-489d-be33-b31dfda151d7", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4", "35f8324f-5f4d-47ed-8fbf-e18a6d44bccf", "ProjectEditor", "PROJECTEDITOR" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "63291419-68d5-4000-bd2e-f7d38005e8a7", 0, "53c67bc2-d672-47f6-975d-3bb3b21e1f05", "user20@gmail.com", false, false, null, "USER20@GMAIL.COM", "USER20", "AQAAAAEAACcQAAAAEPSPrym5sBihp8QKwgSOy2nWMfmxepmS4xsXAxPBoVvR0Wdte1w5czQ2ut2jJsPf/Q==", null, false, "6f54ba71-1d99-4f0f-8aea-46206503bdb6", false, "User20" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ea67ea48-d657-45aa-99d7-30f6ad9fe6cb", 0, "52dc94d8-9691-4864-b18d-5be6f46b7a0a", "user19@gmail.com", false, false, null, "USER19@GMAIL.COM", "USER19", "AQAAAAEAACcQAAAAEPB1mmDttgxAXBuy0p+BEIKLWp+bzTmoVJaCVqCwAc6kUSku/nVRGABp8HUGggMLhg==", null, false, "7dc013a7-3826-4191-b146-c644adbb731d", false, "User19" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9d73cad7-aa64-4fae-90bd-7e926b1ac09a", 0, "8db78bc8-3b70-49ba-a511-63b26f157927", "user18@gmail.com", false, false, null, "USER18@GMAIL.COM", "USER18", "AQAAAAEAACcQAAAAEBVHd0dAuHCTIO8xKoCNM9h1M8rZHsnPnw3Hukt+g3CmrebBm6HEUqMHyfZnJ/BbcQ==", null, false, "338b3e9a-8a1e-461b-af31-b2af14cdffaf", false, "User18" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "edd27651-f08b-4fcb-b468-7efbef598937", 0, "904eb3c7-9839-466b-8bd2-c59a7594afd0", "user17@gmail.com", false, false, null, "USER17@GMAIL.COM", "USER17", "AQAAAAEAACcQAAAAEDzjCILNvF6t780RMjVL5arBxDFthRy1YFbfx95t2yz6ZuqlLda4viiM5TJzjC6eNg==", null, false, "a87f0e2b-418b-4fa7-9228-7ada578d3bd3", false, "User17" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dc965086-2802-4f99-a257-b7a8b8d98442", 0, "e5c09c1e-79b6-4b0a-ad25-d8266af079b9", "user16@gmail.com", false, false, null, "USER16@GMAIL.COM", "USER16", "AQAAAAEAACcQAAAAEHpo+UQTwITGHTkQAFnkPj4eINV/W11ZnQen/4N4kgGvpNOubW2CDv22HpG19YPC8w==", null, false, "44d652f2-9b7f-4b26-bb28-bab45920f343", false, "User16" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d3cfe5b2-ac95-4791-8615-765f49ea6707", 0, "cd62d6e3-d930-4a81-9a9d-fd03e86f2360", "user15@gmail.com", false, false, null, "USER15@GMAIL.COM", "USER15", "AQAAAAEAACcQAAAAEKbpOPOFrHmFKEhZFciRmXH751Z4L/CePqSv1fLJULWgLl4X4C8smRsQiQt6eXAmwA==", null, false, "85d1fa8c-7c4e-47a1-a7be-7b9dfd0fadd8", false, "User15" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1dbc2279-bf37-4959-991d-14388103cddf", 0, "208ad9c1-02d8-458a-af0d-32638913e826", "user14@gmail.com", false, false, null, "USER14@GMAIL.COM", "USER14", "AQAAAAEAACcQAAAAEKKfshRqLMVOGaIz1hlCoCdtdKDDRuEu3EkfBMq23IU8Edh5zdLiH8RDXp5A1Qrtcw==", null, false, "80a54990-de1c-44e4-9953-87bad3fd6394", false, "User14" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a2596045-b0f0-4a27-b600-830e966f74fc", 0, "27d97a59-286f-47fc-8dfc-d8ec9d952f54", "user13@gmail.com", false, false, null, "USER13@GMAIL.COM", "USER13", "AQAAAAEAACcQAAAAEPFr3Q3vHBweIHLeYyA/1T6aJIs9WbpXXIYGIDQ/YcmhlAGqR3QTut2az+pb/1fNIg==", null, false, "8370256d-1f3c-4119-a10d-5f9bfa245369", false, "User13" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6f603342-c842-4210-9dce-97b119ce85f4", 0, "983797a3-805a-4417-a38e-bcbd0a1553f2", "user11@gmail.com", false, false, null, "USER11@GMAIL.COM", "USER11", "AQAAAAEAACcQAAAAEGxxhb7VpHn8L6+ikfmA7MAcOJkuNhx283Uxo5lxqYEOeLUgMV69sG9M8YWF/UlIzg==", null, false, "3b40b0a2-4d44-480a-b155-cded613af0d6", false, "User11" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cb29c276-00c1-467a-b079-dacbcf8ee3a2", 0, "7f7a8887-6cd6-40e8-a29f-e46a7c82b534", "user10@gmail.com", false, false, null, "USER10@GMAIL.COM", "USER10", "AQAAAAEAACcQAAAAEMJaeBkGKCbBtSU3P0wySu5POse4aK5TmEBddFblIe6lBfhm83HvKesfhrLUp+L+3g==", null, false, "495e6dbb-e424-4f18-8e5a-9208c3c20d73", false, "User10" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2c7d2a35-e853-41b0-b098-ce1ca27e06be", 0, "fe9e2a7b-a3f9-4fa7-9755-947f8b7c7f11", "user9@gmail.com", false, false, null, "USER9@GMAIL.COM", "USER9", "AQAAAAEAACcQAAAAEEjCh2LebUKM5ZBVKq4BvOILmump0jQvEwOc2x1MINqsKK8hO+DZnhAxpd+Q6/p0Sg==", null, false, "db87c711-4349-4a31-af57-1cd74a680d8a", false, "User9" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "879e2584-ea1a-4811-9746-d07926c3df33", 0, "e273e5db-6e9c-41ff-9527-517a237bb6e7", "user8@gmail.com", false, false, null, "USER8@GMAIL.COM", "USER8", "AQAAAAEAACcQAAAAEAuAaw5O0wyCd8hMv+urMU2/WlifuhgTjp0PkOSZP1p/XntVzvwgGOBQ9fuDKVf2Kg==", null, false, "54798ad3-2eb8-4410-9883-7f4f809200b0", false, "User8" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aacc55a6-c07c-43a7-9417-49f9e23d32d0", 0, "71e4aca9-e0bd-4a2d-ae20-9f2d05d78612", "user7@gmail.com", false, false, null, "USER7@GMAIL.COM", "USER7", "AQAAAAEAACcQAAAAEAaUuLEzrtKca83hfru1N9qa5mtpIvXIfEXeHShAI8P7wALvMjRFYHAN/2G8nKJHGQ==", null, false, "f790ac34-3cfd-418a-a1fd-908da14a2e7d", false, "User7" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3ef559c9-0f7a-4dce-b287-9b5525379d68", 0, "9f10ba2c-7f51-436b-b3ba-50760e41a87e", "user6@gmail.com", false, false, null, "USER6@GMAIL.COM", "USER6", "AQAAAAEAACcQAAAAEH2CBt+RzU2E/daPM97hhnyXe1+yNn+MEsc95IMyhXqEqZcBzZb56P6IQiSuhP0SvA==", null, false, "f9151d10-89b9-4748-bff6-18e52c5d57d8", false, "User6" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4523c412-16ed-4602-9bd2-f09b7debd462", 0, "90f55457-1a9c-4339-a379-b16b4d61ed43", "user5@gmail.com", false, false, null, "USER5@GMAIL.COM", "USER5", "AQAAAAEAACcQAAAAEOpMZ8TVAIWEjkHp5qG39UEBxOM8o6y616umfrxm/MPf+jVfPWM9cv83kMqCE+D6nA==", null, false, "eb77a177-469a-4551-ac0f-5d7d06f8c4ed", false, "User5" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b7a51167-e025-492e-92d0-addf051e9915", 0, "87a3099d-8d97-4f4c-9795-35585d2f74dd", "user4@gmail.com", false, false, null, "USER4@GMAIL.COM", "USER4", "AQAAAAEAACcQAAAAEGUMf+v27nwkRpAzJ1NRc0y89NqbqpXrh3ZOy6XOBM6XEgCDgQAntVrD5Nzm+fOLDw==", null, false, "f57ce926-2390-41c9-8452-43c04f1987a2", false, "User4" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8cce7a59-56e8-4ea9-b304-22d9921a12eb", 0, "dc31cc3d-732c-45fd-bc24-1ea6f311ecc3", "imagecoreuser@gmail.com", false, false, null, "IMAGECOREUSER@GMAIL.COM", "USER3", "AQAAAAEAACcQAAAAEPlZWP4ds8jBW315yFuYFq/zjF+uQASxBZm9iqs5CCP43c/phx+ZVgFp+i3tV1S77w==", null, false, "c5cd1822-ff88-4fd8-8604-bf0fe0f426bb", false, "User3" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8afd028d-4b23-4f39-81b9-8d7119684f91", 0, "7c12e657-bbc8-46a6-ad11-fef71c271f9c", "imagecore24@gmail.com", false, false, null, "IMAGECORE24@GMAIL.COM", "USER2", "AQAAAAEAACcQAAAAELMQMabduJrB0SS2j5ZQac1DZdVEnKo6Kto+/NcjrFaUFw9MH+qpQ4YwtM9Gwoc98g==", null, false, "a35ec097-e56a-4344-a968-a07d215dfcf4", false, "User2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "37862d31-3f3e-4474-b4f6-65962f02dda8", 0, "1d19b9bb-e31b-47cc-a551-a6e97929a621", "user12@gmail.com", false, false, null, "USER12@GMAIL.COM", "USER12", "AQAAAAEAACcQAAAAEKGzKmcG3B0w6No9UYyuv1oF59L1rig9S4Bz3HWC/UH6aL0Z0/a5idVJqXZ9B4bEMg==", null, false, "d92176bb-2140-46fa-96b5-d92b704a5049", false, "User12" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8dcb9ccb-2ab5-4adf-9605-6b8804276370", 0, "a98771ae-397e-4c3a-a88d-705c78860c96", "imagecore23@gmail.com", false, false, null, "IMAGECORE23@GMAIL.COM", "USER1", "AQAAAAEAACcQAAAAEG3/Q1lyxZXX3TY2D6VUVuNLd6NBE1Svvqja3aPkH4KCDtKWCEvVMH+IoFvxKGIo0Q==", null, false, "52156f2a-db7b-4a4f-9501-6c71e22f1217", false, "User1" });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 1, "Projekt 1", "8dcb9ccb-2ab5-4adf-9605-6b8804276370", 4355 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 3, "Projekt 3", "8dcb9ccb-2ab5-4adf-9605-6b8804276370", 345 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 4, "Projekt 4", "8afd028d-4b23-4f39-81b9-8d7119684f91", 2 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 5, "Projekt 5", "8cce7a59-56e8-4ea9-b304-22d9921a12eb", 25 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 2, "Projekt 2", "b7a51167-e025-492e-92d0-addf051e9915", 4 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "8afd028d-4b23-4f39-81b9-8d7119684f91" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "8dcb9ccb-2ab5-4adf-9605-6b8804276370" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "63291419-68d5-4000-bd2e-f7d38005e8a7" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "ea67ea48-d657-45aa-99d7-30f6ad9fe6cb" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "9d73cad7-aa64-4fae-90bd-7e926b1ac09a" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "edd27651-f08b-4fcb-b468-7efbef598937" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "dc965086-2802-4f99-a257-b7a8b8d98442" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "d3cfe5b2-ac95-4791-8615-765f49ea6707" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1dbc2279-bf37-4959-991d-14388103cddf" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "37862d31-3f3e-4474-b4f6-65962f02dda8" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "8cce7a59-56e8-4ea9-b304-22d9921a12eb" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "6f603342-c842-4210-9dce-97b119ce85f4" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "cb29c276-00c1-467a-b079-dacbcf8ee3a2" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "2c7d2a35-e853-41b0-b098-ce1ca27e06be" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "879e2584-ea1a-4811-9746-d07926c3df33" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "aacc55a6-c07c-43a7-9417-49f9e23d32d0" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "3ef559c9-0f7a-4dce-b287-9b5525379d68" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "4523c412-16ed-4602-9bd2-f09b7debd462" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "a2596045-b0f0-4a27-b600-830e966f74fc" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "b7a51167-e025-492e-92d0-addf051e9915" });

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

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageCore.Migrations
{
    public partial class InitialCreateImageCoreDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: true),
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
                        name: "FK_RoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    ContactUserId = table.Column<string>(type: "TEXT", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_UserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "563b4687-5de7-4b52-80e6-4f30cb764e27", "af9860ac-b5db-4231-9c13-1652b9cfd102", "ProjectOwner", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99a6540e-5af8-40cf-8ba3-7aeb73f2ddce", "004e433d-4ad1-48ef-b354-eca379ef3cd1", "ProjectViewer", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b29f95bf-ea2b-4055-8104-7b394e3547b2", "51773621-4040-4f32-9182-6b3eb4ecb426", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8088f9b3-ad28-400e-8e3a-1a71800817d2", "81c03471-dd51-4aa3-8f3b-63c0d170efaa", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b58f9fa3-0247-484e-9021-198aee4d8f8c", "1ae0a862-48da-49e2-9221-cbccf4280804", "ProjectEditor", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ab6fb151-a460-4255-a05c-c2d7b736af0e", 0, "deee2b1d-d731-4659-bb45-8c288eec4bc0", "user20@gmail.com", false, false, null, "USER20@GMAIL.COM", "USER20", "AQAAAAEAACcQAAAAEDyznMo8yclNQ6h7vJFNn0LQ4mNT2Q8Q4ENZvT+vQUW6p3LD3mBAW7BG+GBld33RMQ==", null, false, "80e1ea77-bd1c-411b-8ddf-16111a08ac0c", false, "User20" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "607374f1-193f-4aa6-986f-5e243e98d8df", 0, "fab4c24c-9dfc-4071-a64c-cf766241db56", "user19@gmail.com", false, false, null, "USER19@GMAIL.COM", "USER19", "AQAAAAEAACcQAAAAEA8ppr3jB8eOJGIH3opqjIBdbD9jvMohqKvlBt9UfhgT6LnXDXUUDtwtd6KK+LDF+g==", null, false, "7388043f-b9a1-44c2-93c5-cd40b9d462ad", false, "User19" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "304e023a-d0af-4efc-8642-8e171ece240d", 0, "90f618bc-58fc-4067-b484-bee6328f7dac", "user18@gmail.com", false, false, null, "USER18@GMAIL.COM", "USER18", "AQAAAAEAACcQAAAAECr0v6cNaAQvqN2o/80eyZm5x2KU8/vthNd/qrcAxCUdDzaFuKUTMTIAMYqD2IY2hQ==", null, false, "9eda10ed-6a39-4c3c-bc10-f14e148fbede", false, "User18" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "909b9741-71c0-4d38-b891-de05ffebf4b0", 0, "c5ddf34e-d73f-4021-94d2-1e4b2300e1d4", "user17@gmail.com", false, false, null, "USER17@GMAIL.COM", "USER17", "AQAAAAEAACcQAAAAEPiH6xRg526x32cMqL8bTNN06HzjOupAPeHUXQOuAg2JOW5XsuzmfzLktHuxw/l7bg==", null, false, "d580ca42-9231-4e19-b74d-d4b0a4f1cc31", false, "User17" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "72ef660d-897b-42bb-a5c1-449d03c163f0", 0, "88872068-2f5a-42f1-8f39-58bad905d8ee", "user16@gmail.com", false, false, null, "USER16@GMAIL.COM", "USER16", "AQAAAAEAACcQAAAAEGqB/P+Y7gkNVgAhLZz2HGZ4jeYVczTQr5k62shZ5j2f8voWEQZHg89TDze5ANmikw==", null, false, "c09f91d6-d7be-44e5-b275-514d1c6e6485", false, "User16" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e0643e72-2c84-412b-944b-747aba2cff5d", 0, "395334ca-7a1a-4a5f-8674-d95002b4af71", "user15@gmail.com", false, false, null, "USER15@GMAIL.COM", "USER15", "AQAAAAEAACcQAAAAEMY47W59V9wvif4GDUTsUHtm4nbpWkhKVJwlxKRBhp5zeE4cYiW3lTYZ+df1H/jTdQ==", null, false, "520ad57d-b3ab-4b1e-becc-4d0169860f25", false, "User15" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1007ae44-de0a-44fb-81be-d3f224b10f45", 0, "9bb80738-a91b-402e-8370-7987757c8d84", "user14@gmail.com", false, false, null, "USER14@GMAIL.COM", "USER14", "AQAAAAEAACcQAAAAEPT+d/b4/P34CwFJ5oxGxpFs3+BWgirWoL8UGUVpfjaBEFt6TPXZhStk1V05Mxp8fg==", null, false, "d83c4614-11db-401d-ad01-313329be39dc", false, "User14" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "310cc86e-88c7-48de-92d6-49747ec1dacf", 0, "d8c86c52-498c-4102-a7a7-1387463415e1", "user13@gmail.com", false, false, null, "USER13@GMAIL.COM", "USER13", "AQAAAAEAACcQAAAAEP1jrcX++YGZE/ea5IyTckf7nHUQZquXQY6d4JTayXswKZtU0PbEuJWs1yVlVwwEuA==", null, false, "95c684fe-e48b-4d17-9137-ba7df57fd6e5", false, "User13" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0c285106-0692-4e0d-86d6-092361c66644", 0, "ec19d90d-ea10-4aed-8582-9d26f07f2e21", "user11@gmail.com", false, false, null, "USER11@GMAIL.COM", "USER11", "AQAAAAEAACcQAAAAEH1q2tcpkE5tZeM18Ms8yH0lJ7eUO6JKKl46C5rrvF6zFavHjb23ZkuW6Gebq2Qhiw==", null, false, "c7e905e5-c2d3-44aa-af38-fbaa010ded33", false, "User11" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "989df044-e8b1-4767-800a-0d1aeaada93c", 0, "b19c7dde-cae2-437a-926c-80b6984267fb", "user10@gmail.com", false, false, null, "USER10@GMAIL.COM", "USER10", "AQAAAAEAACcQAAAAEFmDvyyBq66YxWbiEyqp5TqYtWTApdf0l2gHBbw92+o9/JuLOB3enrWLB8kRwSTWQw==", null, false, "3c28e5c7-c18e-43dd-b597-dba9bbc9fcb9", false, "User10" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "01c5dd2e-f095-4a08-a088-34121bf795bc", 0, "d1e5ae18-52b9-4d41-af41-a8cc6c52e41b", "user9@gmail.com", false, false, null, "USER9@GMAIL.COM", "USER9", "AQAAAAEAACcQAAAAECJ2aIYBmHILFb4x9YnmiBytyuAEK0op8NGbZ0nGeGfAOWTOdR3uUiy+C0/2k5jRJA==", null, false, "98b00d00-1567-49d4-b597-1f53cd911dbf", false, "User9" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ae5043b1-efd0-4d0e-803e-784b7c52ce20", 0, "c7daab3e-c165-4264-95da-883ce9bec635", "user8@gmail.com", false, false, null, "USER8@GMAIL.COM", "USER8", "AQAAAAEAACcQAAAAEP5FWkrFDNlCphTjI/5MqV4VsPRdW7XQowOwhyfFnmM7RHWQVqHPxhrbmxPIOEHVxQ==", null, false, "3bfe926c-9a9f-450a-9d0f-96199ac2eaad", false, "User8" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1e15461a-d1c2-496c-b3fa-614e33aa640d", 0, "d8168c18-9532-4438-bbbd-1966d6c34bb6", "user7@gmail.com", false, false, null, "USER7@GMAIL.COM", "USER7", "AQAAAAEAACcQAAAAEMQdXYUFInHQ3RgspllHuWV2A+97XcTXs1BdPEA2ztUju8Qv9V+a2oxqmZRDUi24fw==", null, false, "0bd7018b-46af-40d7-ac1c-f4402f873729", false, "User7" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5bfb394f-159f-415d-b702-c6839520d8e8", 0, "2a285491-77e9-45b1-91c2-39a98de4d233", "user6@gmail.com", false, false, null, "USER6@GMAIL.COM", "USER6", "AQAAAAEAACcQAAAAEBJDGqiNOs9tSqZR9z7TQWE4DZii7xACgPETGZz3c2Bv85I8lp6GwSB1niuNIt/QNQ==", null, false, "1ef2885f-79c5-49a3-b864-39a9ef4e7586", false, "User6" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "af4d1331-dff4-4127-99a4-019b657ec30f", 0, "f20996f8-3fb9-47e2-b6f0-d6d8f2a693c0", "user5@gmail.com", false, false, null, "USER5@GMAIL.COM", "USER5", "AQAAAAEAACcQAAAAEIPrYvDSUFYCsgh1J8LEyWWXVuL5YZ6q/hn0Sy/1FrNGnqbXyTiIt/liqmnlEZNPvA==", null, false, "bc569ba2-f4e6-4837-a33c-c8b89a6e7dfa", false, "User5" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ab2f57aa-a773-40d7-ba6b-6e89c4c8c157", 0, "835115b1-9efc-4435-9171-bc900503cb84", "user4@gmail.com", false, false, null, "USER4@GMAIL.COM", "USER4", "AQAAAAEAACcQAAAAEL6obAldxhAxUBkcKOR1L24moG+x8iEYnyi+66BMGlErtbR1VZL8gcCkHwJcnP2YNg==", null, false, "5897c782-6ccb-4c7f-88d8-b6da9d68ed2e", false, "User4" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5a5c9052-558b-4f9b-9ca8-598c5d878c49", 0, "cc250068-71ad-404c-af2d-69aad4baeff2", "user3@gmail.com", false, false, null, "USER3@GMAIL.COM", "USER3", "AQAAAAEAACcQAAAAEFzRyC0uHeVd/c69xTTVvpHIreZu2gBxRcqfZIvJSWGxHq/9v4LkBhVR7eojjQ/5Ng==", null, false, "d20d2cf3-0d9b-4bf2-a6cc-5ad6c9a5b5b8", false, "User3" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cd538641-f8d5-4639-a5e0-e4a8f1d9c0f8", 0, "a3e3f8d3-e480-408a-9e0f-298d5a9bf01d", "use2r@gmail.com", false, false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAEAACcQAAAAEMwqBuLJ0M1oiGPZmmgFv63KlSgYeVJREr+sCNl5+D2kRpVoSkYPEpTFRZHwnvIZjw==", null, false, "d06f3ee2-4659-4e72-ab8f-cff86a6f4eb2", false, "User2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "06144c21-8eac-43c4-8874-cd24f6c0b041", 0, "54a01ecb-e16e-45b2-a1a5-007fd2ea79c4", "user12@gmail.com", false, false, null, "USER12@GMAIL.COM", "USER12", "AQAAAAEAACcQAAAAEEZMtMtTjJxshwObmvuNuM22fm6sPE4cNGdljeVEWhQx78S+1N2BREKm7yxhr+we4A==", null, false, "ddaa4a6c-7248-4f90-9eed-1690466318fe", false, "User12" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "89207ea8-db3c-4805-9c52-eecea3821874", 0, "13f6f0a1-bea0-4f06-9a9a-cc07099265db", "user1@gmail.com", false, false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAEAACcQAAAAEDIvF68dylfb5SSwhirQuMDijO7K39Uk1jdh6e0tTTVGBCBHu/CtDynZ7U9WHPJMPg==", null, false, "9cd37f36-0b3a-4745-9c13-eebd84521f32", false, "User1" });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 1, "Projekt 1", "89207ea8-db3c-4805-9c52-eecea3821874", 4355 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 3, "Projekt 3", "89207ea8-db3c-4805-9c52-eecea3821874", 345 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 4, "Projekt 4", "cd538641-f8d5-4639-a5e0-e4a8f1d9c0f8", 2 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 5, "Projekt 5", "5a5c9052-558b-4f9b-9ca8-598c5d878c49", 25 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 2, "Projekt 2", "ab2f57aa-a773-40d7-ba6b-6e89c4c8c157", 4 });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "Roles");

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

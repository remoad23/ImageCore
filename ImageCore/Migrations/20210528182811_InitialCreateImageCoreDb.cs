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
                values: new object[] { "0f492921-3825-4d81-bb2d-67e2c8ba4fd5", "60ebdd2c-7c0f-43ba-96e9-2df47347c6ff", "ProjectOwner", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8e508d04-08a2-489f-9892-66df70e4eb39", "0c1a7e3e-671e-4813-a48f-781f916ecce6", "ProjectViewer", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6e21b649-eea3-4772-986a-41cd9eee8885", "1f12929a-87a5-490e-9eb6-e16bae7970b8", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5dc53032-fc30-439e-a1c6-d80652338882", "cfb33c2a-bcc1-4d0c-9f77-c6c1791c5278", "User", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9b8842fe-8352-4da6-9a74-d511ef4b7c01", "b7a26851-c164-41cf-9e1b-fb8a5d53f4bc", "ProjectEditor", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f4fdafb7-9e0d-42a2-ba53-8ac2494c9412", 0, "8cde2adc-5cbd-4111-bc30-fe8739b02ee2", "user20@gmail.com", false, false, null, "USER20@GMAIL.COM", "USER20", "AQAAAAEAACcQAAAAEN0XPev/Xgli4UiaD0jumjfhZhtjMICkMgm/pEP0343vLohVje5EMwBLMI6JRjHLyw==", null, false, "c6b50374-eea8-4fa1-bbb1-57f55941851d", false, "User20" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2e599054-ef98-4b28-b170-d9011308d2be", 0, "daec90e1-562c-47ff-ac3a-a4df0d05af59", "user19@gmail.com", false, false, null, "USER19@GMAIL.COM", "USER19", "AQAAAAEAACcQAAAAEF0M99ZCCVJ1bG7Dz9jFTLmVoEdzkLAKnRaY2uMroUwFjoyI1Ok9iNC5Xw/pXBnmwA==", null, false, "fc404139-3e33-4bb5-8682-7edf5d2baf50", false, "User19" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "48429336-dfe9-4f29-b31e-613c4ca5a948", 0, "044f6056-2f79-4205-be02-81c3476367e4", "user18@gmail.com", false, false, null, "USER18@GMAIL.COM", "USER18", "AQAAAAEAACcQAAAAEOE1lB1g727hwtiYsEPM9WyFOFtsI46KSchWniVdP5YjtLqMpyNEyRIqSN82tw1/QA==", null, false, "b9959de9-9b11-4d72-af32-13a61be6f39d", false, "User18" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d46001b2-2bac-4067-af2a-6d60028465e2", 0, "cf04fe38-ce8b-4153-8a79-62082b7ba57d", "user17@gmail.com", false, false, null, "USER17@GMAIL.COM", "USER17", "AQAAAAEAACcQAAAAEO571OfSN5f37QAI7xcWVLEsZapnG7EsY9HKl/UWSkcPsI29K2JfMdpzEs8QyA/VqQ==", null, false, "6044b383-7304-46ac-ba17-46331e10e800", false, "User17" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a0e86955-6ee1-4b77-b9d5-020dc6495427", 0, "e2b90510-f69e-4301-8017-cf2d38ff3673", "user16@gmail.com", false, false, null, "USER16@GMAIL.COM", "USER16", "AQAAAAEAACcQAAAAELOpDnKqctCpr+MU0cTqvbg947NlGqLiGfu+9h1tSe0pCaBvyAEa6dZEZqaaLw1rGA==", null, false, "07983e8b-79f5-498e-94e9-62242e3c92d7", false, "User16" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5b52264e-4380-4b6e-848c-bfcb607ff534", 0, "1091adb6-c836-4a53-ac0d-0367f38f5d87", "user15@gmail.com", false, false, null, "USER15@GMAIL.COM", "USER15", "AQAAAAEAACcQAAAAEEY5zmrZ7/ThCn+flR7QXHWYabU1b+Ak1lHk/czR3JZEHwWxnfch+Ezb/rHKRBHYOg==", null, false, "043e65d2-df6b-4352-8cb5-c1cf4eee072f", false, "User15" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0218045a-dba4-420b-b24d-12b642afeb89", 0, "b7c9e5c6-0877-4a65-8a2f-1c395ec44171", "user14@gmail.com", false, false, null, "USER14@GMAIL.COM", "USER14", "AQAAAAEAACcQAAAAEDs1CBZSt3WXwq9GbrHN+0bS511ArYy75CgJs8x3Ql39exAL2sWBPNZjXZYVABtlpA==", null, false, "31e2b05a-dab7-4a3e-87ed-f9a51c338e1b", false, "User14" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "09f1c1c1-71e7-46d9-8d3c-ed5f06da28c5", 0, "e958ef78-eae9-4d42-ae9f-4319cd3e4222", "user13@gmail.com", false, false, null, "USER13@GMAIL.COM", "USER13", "AQAAAAEAACcQAAAAEDpNW2o0jBjLdDp/kfhpG6VvQm5BCZaNiNUdHY2iwLco3jibPIu7i4h/vMR+GW+vGw==", null, false, "8259f044-5c94-4b9c-8de2-cd634597cf14", false, "User13" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fc978743-347f-4c33-a5bb-4b5a591d3bc9", 0, "98c9b569-df39-4fee-a1c8-9d58f856febd", "user11@gmail.com", false, false, null, "USER11@GMAIL.COM", "USER11", "AQAAAAEAACcQAAAAEGKG7uGygistgGjs0cn1gO0SRrZ4pIvutkoLIVaV/IIuFeaIdczEvS+w4iomlPJRxw==", null, false, "066cae3b-764a-4048-a6b0-099caa3b8c51", false, "User11" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e60aa7b0-e21a-4058-965c-54e83996a163", 0, "9dcbc84b-099b-4148-a968-34f3a9f93341", "user10@gmail.com", false, false, null, "USER10@GMAIL.COM", "USER10", "AQAAAAEAACcQAAAAEEbWm19C5M8Wp+cDej4bT3KZY1fmSMHnreV+8n0QIDzT/P1LtTZqWWh9w8Q30D22gA==", null, false, "66c45811-0af0-49cc-9e5b-b26f93ba313d", false, "User10" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9032338b-4db8-4619-8281-54ddc78a7f97", 0, "2e40adca-d884-47b1-84b2-467136a8ae5c", "user9@gmail.com", false, false, null, "USER9@GMAIL.COM", "USER9", "AQAAAAEAACcQAAAAEOlts6MYeA0eXWx+t8EnnUdqeQc0c9gZSlQ8IR4BPIeMREKB+3P7MbQhcf4D5CwDrQ==", null, false, "e4bfa662-880c-456a-a388-17e017e48e17", false, "User9" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "74db1bcd-e448-4ee9-8c2c-feac5d8c4915", 0, "92b20139-e999-44a4-962a-569c40c48610", "user8@gmail.com", false, false, null, "USER8@GMAIL.COM", "USER8", "AQAAAAEAACcQAAAAEDyypcfpkt2gK4gFF6Xx03ZfO33rCwZPJPxvU6SvnXiWvfKBWwNeZMu4hvvFrtAzZA==", null, false, "057696be-32e3-47cc-a85d-125370a45d8a", false, "User8" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "efa81a76-98d5-43fc-b3f9-6ea5dd561aa8", 0, "22b67c0e-695d-4acb-b7a6-e42fe544480f", "user7@gmail.com", false, false, null, "USER7@GMAIL.COM", "USER7", "AQAAAAEAACcQAAAAELgPWZIjUOBUfCnLUKsUYYMy93QCf2I1DFhYTXJ6ex2hOrH0r0ml8pZIXlmGsUvUZw==", null, false, "eb37c1ce-7b57-4e4e-bdbf-e9cf9247cff0", false, "User7" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "92639a84-7cb7-480e-b57a-78fdee5f7f94", 0, "12fe1971-14c2-4073-9aa1-d8614240564c", "user6@gmail.com", false, false, null, "USER6@GMAIL.COM", "USER6", "AQAAAAEAACcQAAAAECfbBEL9xtvKHQiKHPa7TKzSEGh+N9hLt8xB5yq79lr5sRXKIDujawchblHKBPkvlQ==", null, false, "41ff040b-8b3d-4630-a2b6-8c3bba9c1af4", false, "User6" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "de7c6493-a81f-4f9e-bd3a-3e5545c6e420", 0, "13cce161-11e1-469e-83e5-fb4e884486d0", "user5@gmail.com", false, false, null, "USER5@GMAIL.COM", "USER5", "AQAAAAEAACcQAAAAEP7j133F7Y0j7G6UXDomJX8QgKUFRnbfoEO3O0uh79uj9AdwULdeWkG6f07IdNkBSA==", null, false, "dd7bf6b0-7d2f-480f-a003-01841d678a6f", false, "User5" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "15168994-d69c-4336-bc6f-4c79f589863c", 0, "50036f3e-bcc4-42d1-8de7-980ce0b052bf", "user4@gmail.com", false, false, null, "USER4@GMAIL.COM", "USER4", "AQAAAAEAACcQAAAAEAg5jwGL6smTUqTiyWmD6CV9Qo3l0azGSo7e2UCDo4wIe9jsp9IbuuKeKeYCaP8eyw==", null, false, "edb0a736-31bb-4f03-a163-47ecf525a26c", false, "User4" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b39aac06-3197-41d6-96a3-0bf789b03d93", 0, "7806a21d-11d6-4ff4-8481-3b193a4e956c", "user3@gmail.com", false, false, null, "USER3@GMAIL.COM", "USER3", "AQAAAAEAACcQAAAAEBPN5Et23dKWplYhdTC30IuYAPvudG4uX5tLqGRwqGlcur6CWMxfNEL/ygeL75mZ9w==", null, false, "3d2eac96-8aed-4bcc-8d81-f9351ed1b7a3", false, "User3" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9205d848-10af-4f44-93ea-556192ba6621", 0, "2a7d6b90-87cf-4c6f-8a40-0d80761ea9cc", "use2r@gmail.com", false, false, null, "USER2@GMAIL.COM", "USER2", "AQAAAAEAACcQAAAAENXGKjmwyJfM4LvsbstoWzm/mheuEU6WfU50RN+/eIBoPrlnV3m41rzJA1qxVotzSg==", null, false, "4926b42b-143b-4bdf-89ed-aa3a078e9713", false, "User2" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "20b674be-9340-476e-a57a-b86b9442dcf8", 0, "ebadffd9-f5a9-4907-94e5-196cb02a5c4c", "user12@gmail.com", false, false, null, "USER12@GMAIL.COM", "USER12", "AQAAAAEAACcQAAAAEL3ky48Dvh4WWWb8+Pb7wwVj6GYYqrTxQ07HH2633cGnkD0I6w69HETeIaorLRNixg==", null, false, "a9f35169-0486-4fda-a8c3-49ca3d49e683", false, "User12" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f0aec08c-bd88-43d3-b642-bcc8d07373d9", 0, "7348229f-58f5-4ba2-8192-fa2a0ca0a623", "user1@gmail.com", false, false, null, "USER1@GMAIL.COM", "USER1", "AQAAAAEAACcQAAAAENkkveX6Mnt2tc9ZYJyJjA1SIzdYK0yyU5PGtEmfnaht1wscFgcnxVuu/g3+/mZAyg==", null, false, "1f121719-e1a0-444a-bff6-c6f5a018954f", false, "User1" });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 1, "Projekt 1", "f0aec08c-bd88-43d3-b642-bcc8d07373d9", 4355 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 3, "Projekt 3", "f0aec08c-bd88-43d3-b642-bcc8d07373d9", 345 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 4, "Projekt 4", "9205d848-10af-4f44-93ea-556192ba6621", 2 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 5, "Projekt 5", "b39aac06-3197-41d6-96a3-0bf789b03d93", 25 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { 2, "Projekt 2", "15168994-d69c-4336-bc6f-4c79f589863c", 4 });

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

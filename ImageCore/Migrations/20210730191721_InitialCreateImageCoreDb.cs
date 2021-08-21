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
                    Description = table.Column<string>(type: "TEXT", nullable: true),
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
                    ContactId = table.Column<string>(type: "TEXT", nullable: false),
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
                    ProjectId = table.Column<string>(type: "TEXT", nullable: false),
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
                    ImageLayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    MaskMat = table.Column<string>(type: "TEXT", nullable: true),
                    X = table.Column<float>(type: "REAL", nullable: false),
                    Y = table.Column<float>(type: "REAL", nullable: false),
                    Z = table.Column<float>(type: "REAL", nullable: false),
                    Opacity = table.Column<byte>(type: "INTEGER", nullable: false),
                    Visible = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectParticipator",
                columns: table => new
                {
                    ProjectParticipatorId = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
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
                    FilterId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FilterType = table.Column<string>(type: "TEXT", nullable: true),
                    ImageLayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageLayerId1 = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId1 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.FilterId);
                    table.ForeignKey(
                        name: "FK_Filters_ImageLayer_ImageLayerId1",
                        column: x => x.ImageLayerId1,
                        principalTable: "ImageLayer",
                        principalColumn: "ImageLayerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filters_Project_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageComponent",
                columns: table => new
                {
                    ImageComponentId = table.Column<string>(type: "TEXT", nullable: false),
                    LocalX = table.Column<float>(type: "REAL", nullable: false),
                    LocalY = table.Column<float>(type: "REAL", nullable: false),
                    LocalZ = table.Column<float>(type: "REAL", nullable: false),
                    ColorMat = table.Column<string>(type: "TEXT", nullable: true),
                    ImageLayerId = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: true),
                    ImageLayerId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageComponent", x => x.ImageComponentId);
                    table.ForeignKey(
                        name: "FK_ImageComponent_ImageLayer_ImageLayerId1",
                        column: x => x.ImageLayerId1,
                        principalTable: "ImageLayer",
                        principalColumn: "ImageLayerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageComponent_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a5b6fc4-ae21-480a-ab3e-9339b9050db4", "bacf3b1e-c0d0-436e-9df3-304b10d1b0eb", "ProjectOwner", "PROJECTOWNER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "524a2e32-7188-4c17-b651-f606493acdae", "00c94c49-ce67-4979-ab76-3b1cb6f89663", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "95e7f3f8-9d5b-4400-a61b-156bf54cff42", "User", "USER" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e4b10fa7-5093-411c-85b2-b3daf021a2af", "b921256c-7a8e-4604-b960-df26018bae39", "ProjectEditor", "PROJECTEDITOR" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "ee43ce07-8251-48e5-ba73-00d5a0513728", 0, "40b5f16d-9324-4abc-9781-e4c8bd4c36d3", null, "user20@gmail.com", false, false, null, "USER20@GMAIL.COM", "USER20", "AQAAAAEAACcQAAAAEOAy20Vah1RSq6CELUInioRBn02IGTlo/xXLLP9asSu6t27NvxhpPlQ03UcCWS8ZYQ==", null, false, "ee8a709c-affd-46b6-a0b2-97f6547c0b31", false, "User20", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "f649d216-e4e9-473d-b14d-71b6257722f2", 0, "efbe1ed7-d16e-4c75-b899-05211bc3bccd", null, "user19@gmail.com", false, false, null, "USER19@GMAIL.COM", "USER19", "AQAAAAEAACcQAAAAEClKZ7zMrdOtcu3WV0Z6yf7rJ7KAJvrYOlQKb34wp3RzP9b/MrYI5lSu3VQbdi2kzQ==", null, false, "e38f99c3-be51-47d7-af58-c9c0fd255469", false, "User19", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "de07c5e4-8495-49bd-a4b8-5e93dd78695e", 0, "2db98f09-90b9-498b-8d5b-6af3de782ded", null, "user18@gmail.com", false, false, null, "USER18@GMAIL.COM", "USER18", "AQAAAAEAACcQAAAAEDbT/lwjmlcJSNY/8iOdJHk5E57dpo61pNl1ZrUk50GHO2tUtVM9InJGslsKm51X1g==", null, false, "2ea1b4c9-5ca3-43b1-891e-9b6b33a86f08", false, "User18", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "0d0b9758-9139-4e5d-96c6-d275c0f9313a", 0, "10551a91-16d3-4a3a-8910-2ae0960a9092", null, "user17@gmail.com", false, false, null, "USER17@GMAIL.COM", "USER17", "AQAAAAEAACcQAAAAEIy2SV7SqiYvnj7Ykg79NC0QZCaHIHBIUQuJJUyM9DqyzQXXxksyyLZuVqV42g/1kQ==", null, false, "fa0cc80f-ca10-471e-b4df-f8a9ad2c068a", false, "User17", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "57fe5c73-2790-457e-b4cf-d482d6fc2305", 0, "d261269f-c1d4-468f-b5bb-15d171e3dee6", null, "user16@gmail.com", false, false, null, "USER16@GMAIL.COM", "USER16", "AQAAAAEAACcQAAAAEBT9r7hJ7RPOehIvzybRtj1JmwG5YVllnUuNFzwjhkHU2jvzkUQ0F3ZfWF36Bs2ucA==", null, false, "59097ac1-d2d9-4f30-a07f-3c2c4bed05c2", false, "User16", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "6a89b3de-c4ba-464a-ac79-b0119d115226", 0, "72562874-1c17-473a-a9a6-6db80b3b72bf", null, "user15@gmail.com", false, false, null, "USER15@GMAIL.COM", "USER15", "AQAAAAEAACcQAAAAEEFo6QcYpOWZUIXJuRdPnl0oIyEfc/Np1xRvmmxq0pMFc3qaAkTNO3COOaFnvU8WLA==", null, false, "262ff9a0-8968-44ec-a42e-a159ee157386", false, "User15", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "c3ac58c0-3b19-4a22-b457-ce31c1cb973a", 0, "c1892872-7c95-4a9c-a585-d07300ac8636", null, "user14@gmail.com", false, false, null, "USER14@GMAIL.COM", "USER14", "AQAAAAEAACcQAAAAEEr6X519CA9dTy2H/NAowsm5j5KjosWKj5Hqni3dq+QL9cJVZbokNDwy4UAyyo+8IQ==", null, false, "89f4568a-23ae-4fe0-bfb6-abd3b6997c14", false, "User14", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "37dbbcba-c045-416f-8b2b-9819b440d8f8", 0, "a7749121-57bd-4eca-8bea-4067346aca26", null, "user13@gmail.com", false, false, null, "USER13@GMAIL.COM", "USER13", "AQAAAAEAACcQAAAAENeXCCj93SUPIyEnCkLi74BZInuZvwnHwhq5sUqHzTYY2XByjAdVaLOdTu9cvtontw==", null, false, "b5a0a3df-1fa1-4ded-99b8-0e3180f2edc6", false, "User13", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "52b298a4-5a1d-41c7-89b8-d99c69c575c6", 0, "5cb6921c-7ccc-4dbe-902c-18d749c6c3ee", null, "user12@gmail.com", false, false, null, "USER12@GMAIL.COM", "USER12", "AQAAAAEAACcQAAAAEAnnd335NHrSMlgrud/FrWt6j65vzSiagiIlwcHjfeprduZ+ZSsskC6bVr/dOJgkiQ==", null, false, "02119aed-d8e1-4beb-b667-2197b773f85e", false, "User12", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "afcf5ef7-0655-4171-8104-2e867372e634", 0, "3a5fa618-8b0d-4cc9-aa9e-903e91048183", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "user10@gmail.com", false, false, null, "USER10@GMAIL.COM", "USER10", "AQAAAAEAACcQAAAAEA6ud7BtEk6z25uKcXadsmp1AtfdDVg/ysdhBtMo+NoooOStGSV19BrIBDVJmEUAYA==", null, false, "b0619c66-8e78-49a8-bb8d-1b90595c8d9d", false, "User10", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "21f0d2e2-9d77-4690-936c-b15f9f8f88c8", 0, "efc239ac-622b-4e0b-abe1-8e8f23c9fe47", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "user9@gmail.com", false, false, null, "USER9@GMAIL.COM", "USER9", "AQAAAAEAACcQAAAAEEQXBjyfHT8ytR1jDkzUlZEoGOJFGMHpQTmCu3bsyLkJfXNFq+8UcK3JcO39gnsE7A==", null, false, "4b11d68b-ab4a-44b6-9435-bb198cbaa097", false, "User9", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "12ed9430-d8f5-43d0-9e86-6aa1017c206c", 0, "a94b8092-cc72-48c3-87e6-fd3a651a0da7", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "user8@gmail.com", false, false, null, "USER8@GMAIL.COM", "USER8", "AQAAAAEAACcQAAAAEKDiYo7KC4ArKQhuZi6ziS5uQVirmNBiimF8Og31orEyroNuD1A46lMns83KtZDDPA==", null, false, "b11a6560-6475-41e7-8980-f2a70068c259", false, "User8", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "fe123dd2-d47f-4b14-a737-a2de36aeb6c1", 0, "8f20194b-9f61-4be5-8af9-50a6b03e190a", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "user7@gmail.com", false, false, null, "USER7@GMAIL.COM", "USER7", "AQAAAAEAACcQAAAAEPw62BuMxsyPwZhs2CA3MtlC0MVCNQerHxYWxeHRF17+ndfeCWj2vWhWWR4Qzzn//w==", null, false, "bd045c5b-b950-47c4-8a2d-b2ab5ddf8340", false, "User7", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "d272dbd6-ed3e-42b7-810c-50f02097fed0", 0, "24499532-5044-4c01-98c1-e5531892c0d6", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "user6@gmail.com", false, false, null, "USER6@GMAIL.COM", "USER6", "AQAAAAEAACcQAAAAEMLNmYcqm/C7Io4Yg0zgBkMEEbyCMzNbFWQvbYW5MM7YEI5Xo8f+WwhDBdDJIrLu7g==", null, false, "28de2e40-5508-4000-9309-cbd45ee51d20", false, "User6", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "9676444a-bc74-4a91-860a-cd8ca8df05ba", 0, "8bd9bdb0-be02-467f-a527-98274f8601af", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "user5@gmail.com", false, false, null, "USER5@GMAIL.COM", "USER5", "AQAAAAEAACcQAAAAEFuRhUfOdAZJALHu5cDhxErkg8Z3CVSkkpG4J9z2dqEtC9GRZNo31wD3bGxFTQZ+9w==", null, false, "1523fa79-52c4-4ff2-b4a6-de74ae95678c", false, "User5", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "b10fa5b1-9c5c-4e91-9515-4ecc09e4c7df", 0, "b54bc602-2e30-4409-9e47-2a851697431c", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "user4@gmail.com", false, false, null, "USER4@GMAIL.COM", "USER4", "AQAAAAEAACcQAAAAEDOj7xd5Ncm8gguR3SMLVvAOtkYPf5E/6+dWNRNK73gHgSOHs8gqO7BNB/fiF1U8+A==", null, false, "79b6247d-e4c7-4d3d-98dc-6c210c4bfb6d", false, "User4", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "c374c344-1e19-4f77-ac61-56382277f014", 0, "b92aad54-cd83-46c5-9c78-6ca890b81591", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "imagecoreuser@gmail.com", false, false, null, "IMAGECOREUSER@GMAIL.COM", "USER3", "AQAAAAEAACcQAAAAEEZuE+Y0ZSFbAzR2n7U++cutC4zcQSxw2pBXjVo9Y+D4tru2NJa2d+FeoGRqjdtrMA==", null, false, "d9a361f0-6ef2-4758-8143-e5165d0a2c92", false, "User3", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "630beaf3-d8a9-475b-8001-5ec08db5ad12", 0, "8b428f23-5980-41b0-a084-c4f45f327aa7", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "imagecore24@gmail.com", false, false, null, "IMAGECORE24@GMAIL.COM", "USER2", "AQAAAAEAACcQAAAAEPAtLX/pS0aZB4cBegGwjEnTQ4VIAgs5DploE4nxXwajW4IrQXK/2OvRj3Vm/yTB/A==", null, false, "30920943-3ae9-43b2-89a1-f2c59c4ca1e0", false, "User2", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "93d10d56-9275-4f1d-b77a-c4f2d2b986d5", 0, "a32795c9-52a2-44ad-bb90-dcc510daef1d", null, "user11@gmail.com", false, false, null, "USER11@GMAIL.COM", "USER11", "AQAAAAEAACcQAAAAEOmLCLz/BIewdpeJSIMDBU2CAGvBN4S8VDU3UnU2IPunDfv2Ye+fM2A/0jpe1dZUSg==", null, false, "6eac6a03-371b-447e-a104-386cafb8de01", false, "User11", null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "image" },
                values: new object[] { "714e6d91-1df9-494e-898e-591655a2ff5d", 0, "7b0d0f6b-a337-46f3-9457-0c38662d13a0", "Hallo! Ich bin eine Beschreibung über dieses Profil!", "imagecore23@gmail.com", false, false, null, "IMAGECORE23@GMAIL.COM", "USER1", "AQAAAAEAACcQAAAAEKslS51/GD/bRopl7r91ibWxjpjdIOtq+1E3Mc1MlPEDs9srKvWcmGNNsBuu3dayEA==", null, false, "1950492f-96f5-4eac-ae1b-2b46d3ec7932", false, "User1", null });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { "7e398450-7314-4703-a8f6-20c322c956a4", "Projekt 1", "714e6d91-1df9-494e-898e-591655a2ff5d", 4355 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { "806327e1-109f-4ee9-aef9-8e8cb686de51", "Projekt 3", "714e6d91-1df9-494e-898e-591655a2ff5d", 345 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { "8ad5f37f-57b0-4728-8d4f-c635b582fad4", "Projekt 4", "630beaf3-d8a9-475b-8001-5ec08db5ad12", 2 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { "669aaad2-ab0e-44cd-b12b-fdce4e746063", "Projekt 5", "c374c344-1e19-4f77-ac61-56382277f014", 25 });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "ProjectId", "Name", "UserId", "Views" },
                values: new object[] { "12ea7872-2966-42ae-9101-67708c2f62ef", "Projekt 2", "b10fa5b1-9c5c-4e91-9515-4ecc09e4c7df", 4 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "524a2e32-7188-4c17-b651-f606493acdae", "630beaf3-d8a9-475b-8001-5ec08db5ad12" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "524a2e32-7188-4c17-b651-f606493acdae", "714e6d91-1df9-494e-898e-591655a2ff5d" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "ee43ce07-8251-48e5-ba73-00d5a0513728" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "f649d216-e4e9-473d-b14d-71b6257722f2" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "de07c5e4-8495-49bd-a4b8-5e93dd78695e" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "0d0b9758-9139-4e5d-96c6-d275c0f9313a" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "57fe5c73-2790-457e-b4cf-d482d6fc2305" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "6a89b3de-c4ba-464a-ac79-b0119d115226" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "c3ac58c0-3b19-4a22-b457-ce31c1cb973a" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "52b298a4-5a1d-41c7-89b8-d99c69c575c6" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "524a2e32-7188-4c17-b651-f606493acdae", "c374c344-1e19-4f77-ac61-56382277f014" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "93d10d56-9275-4f1d-b77a-c4f2d2b986d5" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "afcf5ef7-0655-4171-8104-2e867372e634" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "21f0d2e2-9d77-4690-936c-b15f9f8f88c8" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "12ed9430-d8f5-43d0-9e86-6aa1017c206c" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "fe123dd2-d47f-4b14-a737-a2de36aeb6c1" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "d272dbd6-ed3e-42b7-810c-50f02097fed0" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "9676444a-bc74-4a91-860a-cd8ca8df05ba" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7c069d3a-070e-4f50-bb9a-bef2a4290cf0", "37dbbcba-c045-416f-8b2b-9819b440d8f8" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "524a2e32-7188-4c17-b651-f606493acdae", "b10fa5b1-9c5c-4e91-9515-4ecc09e4c7df" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "f575420a-7643-4865-ac23-cbf1f386d3f5", "7e398450-7314-4703-a8f6-20c322c956a4", "714e6d91-1df9-494e-898e-591655a2ff5d" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "91298e3e-3326-4e3c-8b57-7d0bd1e85f7d", "7e398450-7314-4703-a8f6-20c322c956a4", "630beaf3-d8a9-475b-8001-5ec08db5ad12" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "030bee91-0ffb-4fcc-ae17-d9724330f2df", "7e398450-7314-4703-a8f6-20c322c956a4", "c374c344-1e19-4f77-ac61-56382277f014" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "bfcce3e3-d123-484c-9928-7c60dc8f60a8", "806327e1-109f-4ee9-aef9-8e8cb686de51", "714e6d91-1df9-494e-898e-591655a2ff5d" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "e4bbf3c5-5b27-473f-90d4-88c439de1e41", "8ad5f37f-57b0-4728-8d4f-c635b582fad4", "714e6d91-1df9-494e-898e-591655a2ff5d" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "21c82b6a-1de0-4843-a9d6-b41a205ac2ce", "8ad5f37f-57b0-4728-8d4f-c635b582fad4", "630beaf3-d8a9-475b-8001-5ec08db5ad12" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "df383848-be59-42a6-ae54-ee83afeedbe4", "8ad5f37f-57b0-4728-8d4f-c635b582fad4", "c374c344-1e19-4f77-ac61-56382277f014" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "a39f1aff-2653-43ee-a65f-d05915cba671", "8ad5f37f-57b0-4728-8d4f-c635b582fad4", "b10fa5b1-9c5c-4e91-9515-4ecc09e4c7df" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "af7826bd-5a7e-4ecd-891e-47d186a1663d", "669aaad2-ab0e-44cd-b12b-fdce4e746063", "714e6d91-1df9-494e-898e-591655a2ff5d" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "f94319a0-f700-432a-891f-02fcfe1ed8b3", "669aaad2-ab0e-44cd-b12b-fdce4e746063", "630beaf3-d8a9-475b-8001-5ec08db5ad12" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "5674f547-1159-4223-bfa3-c4ae48884c73", "669aaad2-ab0e-44cd-b12b-fdce4e746063", "c374c344-1e19-4f77-ac61-56382277f014" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "bd9508a6-7ba2-495b-bf7e-477b35f4fbc9", "669aaad2-ab0e-44cd-b12b-fdce4e746063", "b10fa5b1-9c5c-4e91-9515-4ecc09e4c7df" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "ddd4e971-4c36-4009-84ae-555ac2bebca0", "669aaad2-ab0e-44cd-b12b-fdce4e746063", "9676444a-bc74-4a91-860a-cd8ca8df05ba" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "553bee97-6101-4bfe-bb3c-8f6ca8d8f3cc", "12ea7872-2966-42ae-9101-67708c2f62ef", "630beaf3-d8a9-475b-8001-5ec08db5ad12" });

            migrationBuilder.InsertData(
                table: "ProjectParticipator",
                columns: new[] { "ProjectParticipatorId", "ProjectId", "UserId" },
                values: new object[] { "9f22b06b-7311-48eb-b346-653c2d2245e7", "12ea7872-2966-42ae-9101-67708c2f62ef", "b10fa5b1-9c5c-4e91-9515-4ecc09e4c7df" });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactUserId",
                table: "Contact",
                column: "ContactUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ImageLayerId1",
                table: "Filters",
                column: "ImageLayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ProjectId1",
                table: "Filters",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_ImageComponent_ImageLayerId1",
                table: "ImageComponent",
                column: "ImageLayerId1");

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

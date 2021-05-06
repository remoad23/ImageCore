﻿// <auto-generated />
using System;
using ImageCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImageCore.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20210506133801_InitialCreateImageCoreDb")]
    partial class InitialCreateImageCoreDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("ImageCore.Models.FilterModel", b =>
                {
                    b.Property<int>("FilterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilterType")
                        .HasColumnType("TEXT");

                    b.Property<int>("ImageLayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FilterId");

                    b.ToTable("FilterModels");
                });

            modelBuilder.Entity("ImageCore.Models.ImageComponentModel", b =>
                {
                    b.Property<int>("ImageComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ColorMat")
                        .HasColumnType("TEXT");

                    b.Property<int>("ImageLayerId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("LocalX")
                        .HasColumnType("REAL");

                    b.Property<float>("LocalY")
                        .HasColumnType("REAL");

                    b.Property<float>("LocalZ")
                        .HasColumnType("REAL");

                    b.HasKey("ImageComponentId");

                    b.ToTable("ImageComponent");
                });

            modelBuilder.Entity("ImageCore.Models.ImageLayerModel", b =>
                {
                    b.Property<int>("ImageLayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LayerType")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaskMat")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Opacity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Visible")
                        .HasColumnType("INTEGER");

                    b.Property<float>("X")
                        .HasColumnType("REAL");

                    b.Property<float>("Y")
                        .HasColumnType("REAL");

                    b.Property<float>("Z")
                        .HasColumnType("REAL");

                    b.HasKey("ImageLayerId");

                    b.ToTable("ImageLayer");
                });

            modelBuilder.Entity("ImageCore.Models.ProjectModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("participatornumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("views")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ImageCore.Models.ProjectParticipatorModel", b =>
                {
                    b.Property<int>("ProjectParticipatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProjectParticipatorId");

                    b.ToTable("ProjectParticipator");
                });

            modelBuilder.Entity("ImageCore.Models.RoleModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ImageCore.Models.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "e1dc1314-8e6c-4f84-9960-aeac4fe27649",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a0db205a-3547-4268-91b2-b2d4a53b07c6",
                            Email = "user1@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User1",
                            PasswordHash = "AQAAAAEAACcQAAAAEEb9M9/0AyvSk3/Ar1HGjTBYetxMWV3zcrcGWMXLqP7HsNRUJIVX+xDH7Mp9kJD8eA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d1056b4b-49d1-409d-a5d8-5d0b3bc652db",
                            TwoFactorEnabled = false,
                            UserName = "User1"
                        },
                        new
                        {
                            Id = "99b57491-85de-4a1c-9ac7-f5444f65d553",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6fdd7b48-33f3-4137-972c-650e46da5a41",
                            Email = "use2r@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User2",
                            PasswordHash = "AQAAAAEAACcQAAAAEIQMjos+dfg+FqIKW0xqVAbY2p/4Wt+8mUrWpWn05ikwIr7m6Klg3YniObEQdB2F6g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "16f8f8e5-7d7e-4ff8-961d-7f55846f4725",
                            TwoFactorEnabled = false,
                            UserName = "User2"
                        },
                        new
                        {
                            Id = "984316ed-a76d-4368-ac18-73e3cc67b3ef",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b18799f4-553c-41ac-9149-5c026e6b32b4",
                            Email = "user3@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User3",
                            PasswordHash = "AQAAAAEAACcQAAAAENUEfXFygQ8sbBqGQllgX9oILCrjIPU7FsHnK5efHxU/zGpLt3+gX/7LFoe6xYu4ig==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ca91d41a-35ed-4f61-aad3-97b75b629ace",
                            TwoFactorEnabled = false,
                            UserName = "User3"
                        },
                        new
                        {
                            Id = "35185199-d837-4c6c-be23-47019f325990",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1c24f32e-e1e3-4832-9384-695bad36e711",
                            Email = "user4@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User4",
                            PasswordHash = "AQAAAAEAACcQAAAAEOuRYCo3jKjJCQjxngCkgEN3Q1qV41fF38pHdV/4Zz/6TtLyNJUVKr26nmdpdIMLbw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fdc60e06-6271-4089-8c59-475655aba7c6",
                            TwoFactorEnabled = false,
                            UserName = "User4"
                        },
                        new
                        {
                            Id = "7786428f-079f-4317-ad29-2953525ae956",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "66e3adba-a8d1-45b1-b300-b9fc5a2a2216",
                            Email = "user5@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User5",
                            PasswordHash = "AQAAAAEAACcQAAAAEKCyAVBHJmJV/yB/6LOELk6w+ZWvQrHbrPIYApxqO6j9QKaCGys0tfP+noDPps+u1g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8ad640d9-173f-42f1-ad6d-c2cf9641548e",
                            TwoFactorEnabled = false,
                            UserName = "User5"
                        },
                        new
                        {
                            Id = "6b51cbd3-580d-4b34-9db0-a720cda73e50",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1e92c64f-3ea9-4e3b-9101-d419c5534033",
                            Email = "user6@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User6",
                            PasswordHash = "AQAAAAEAACcQAAAAEF0mfsf56RJ+8R8vEBXabJWL5OTBo+HH46mpWzbf1Mcr7S0mzv6fsOA39kfFjxuZrQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "007648f8-e036-43ba-91c8-acf46901a14f",
                            TwoFactorEnabled = false,
                            UserName = "User6"
                        },
                        new
                        {
                            Id = "21e2a3c0-72a7-429a-9d08-a6669d8b2110",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9d1de325-fb92-4354-87fb-4d50d20dece7",
                            Email = "user7@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User7",
                            PasswordHash = "AQAAAAEAACcQAAAAEDLaKyipi4eaP5c4qGaAyFgg6re3JLfunmGqesjB5movrvHUDn0sNaX5+Ihfi1O2Qg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6b919a39-6f6f-49cc-8607-623f9ec242fa",
                            TwoFactorEnabled = false,
                            UserName = "User7"
                        },
                        new
                        {
                            Id = "c87f363b-2cd1-413f-bcdf-0ff204e0c53a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d358ee21-363d-40ca-8d5f-bf5150d63d93",
                            Email = "user8@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User8",
                            PasswordHash = "AQAAAAEAACcQAAAAEICIlexBTbj20NxtVX/wTwv5c5xTmPyxnVlD7CqeXlgez54HNxZg73YfCGRjXUJyIA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0f73f84a-0ad4-4228-9f01-e1da990cc2ab",
                            TwoFactorEnabled = false,
                            UserName = "User8"
                        },
                        new
                        {
                            Id = "78eb9407-8bca-4dc6-9b32-82b1457bc11d",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f5dc7b7f-5370-4e35-a4f4-5915e4a6c710",
                            Email = "user9@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User9",
                            PasswordHash = "AQAAAAEAACcQAAAAEKnznuM3BR2X/E1Do2mAvDyXS6eKXfWvfMKsgE46OO+N4rzBvbVufF3TybN5NwRcFQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f650b0d4-8092-4e62-ba2a-f8ec42d62006",
                            TwoFactorEnabled = false,
                            UserName = "User9"
                        },
                        new
                        {
                            Id = "1b271a65-8b9e-4d9c-af7e-84ea51e2cbce",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "76036878-6c0b-48fe-b3f9-f8fa3d1ebcdd",
                            Email = "user10@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "User10",
                            PasswordHash = "AQAAAAEAACcQAAAAEHU9HiYKfuvsaoDUDZhfks/cXn92B55v0puqU+1y0lbU36+yadP+QNtWoKFhyDqHvw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e56990d9-d9b0-43b6-a3cb-1d0431c67260",
                            TwoFactorEnabled = false,
                            UserName = "User10"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "f942bc84-460a-44b9-8535-831e585edc7a",
                            ConcurrencyStamp = "3596cc05-77fd-4224-a4bc-d2207388abef",
                            Name = "User"
                        },
                        new
                        {
                            Id = "1eeb71c5-83a9-4184-91ef-b1cd4f6f0fd3",
                            ConcurrencyStamp = "ccf873ac-7ab6-4dcf-8afc-7466da528e37",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = "b77908b0-56e1-47a8-9712-f62f8bf392d6",
                            ConcurrencyStamp = "00fa86d9-04d7-4c2a-a0b1-86c83810c5c9",
                            Name = "ProjectViewer"
                        },
                        new
                        {
                            Id = "ce19923f-cfd5-43a7-974a-2796299d5423",
                            ConcurrencyStamp = "8da9e6a5-a64b-40ba-8a75-9e5741ed65e5",
                            Name = "ProjectEditor"
                        },
                        new
                        {
                            Id = "03d970b1-2ac2-48e4-83a2-fcf11771770b",
                            ConcurrencyStamp = "16b41d73-7218-486e-a7cd-0ab055d4ad2b",
                            Name = "ProjectOwner"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ImageCore.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ImageCore.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ImageCore.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ImageCore.Models.UserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

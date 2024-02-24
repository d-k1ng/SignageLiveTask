﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignageLivePlayer.Api.Data.Db;

#nullable disable

namespace SignageLivePlayer.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240224231852_InitializeDb")]
    partial class InitializeDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("SignageLivePlayer.Api.Data.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("CheckInFrequency")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayerUniqueId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = "5ae61ad4-62f0-455a-991a-b1baa412004c",
                            CheckInFrequency = 60,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(4769),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(4771),
                            PlayerName = "Reception Large Screen",
                            PlayerUniqueId = "RECEPT-0987",
                            SiteId = "72c77116-711b-449b-b57c-2b03239247b2"
                        },
                        new
                        {
                            Id = "e350e0f9-c6af-4ed9-a81c-ac18d8648768",
                            CheckInFrequency = 60,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7882),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7884),
                            PlayerName = "Reception Small Screen 1",
                            PlayerUniqueId = "RECEPT-1273",
                            SiteId = "72c77116-711b-449b-b57c-2b03239247b2"
                        },
                        new
                        {
                            Id = "9810477b-4a99-4bd0-bfa1-d0022ab2f7cd",
                            CheckInFrequency = 60,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7908),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7909),
                            PlayerName = "Reception Small Screen 2",
                            PlayerUniqueId = "RECEPT-1986",
                            SiteId = "72c77116-711b-449b-b57c-2b03239247b2"
                        },
                        new
                        {
                            Id = "e13a59c8-09e7-4b4b-a926-2f4313218b45",
                            CheckInFrequency = 120,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7940),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7940),
                            PlayerName = "Sales Office",
                            PlayerUniqueId = "SALESO-5459",
                            SiteId = "72c77116-711b-449b-b57c-2b03239247b2"
                        },
                        new
                        {
                            Id = "23e52508-2077-4927-802f-d5082475755a",
                            CheckInFrequency = 180,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7961),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7961),
                            PlayerName = "Marketing Office 1",
                            PlayerUniqueId = "MARKET-2278",
                            SiteId = "d2cecbcb-9c4a-4890-af02-9854c11e402f"
                        },
                        new
                        {
                            Id = "0570c4cd-40f8-427e-8ec1-c93bb214adaa",
                            CheckInFrequency = 100,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7982),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7982),
                            PlayerName = "Marketing Office 2",
                            PlayerUniqueId = "MARKET-3424",
                            SiteId = "d2cecbcb-9c4a-4890-af02-9854c11e402f"
                        },
                        new
                        {
                            Id = "9b56b7e5-5c1c-41e3-bf92-82b3ddc3e2b3",
                            CheckInFrequency = 30,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8007),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8008),
                            PlayerName = "Warehouse Building 1",
                            PlayerUniqueId = "WAREHO-3751",
                            SiteId = "85965d81-c045-4a35-8523-82b64fdc0e87"
                        },
                        new
                        {
                            Id = "f9454690-fc43-4a23-b1c5-6440946e599e",
                            CheckInFrequency = 30,
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8028),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8029),
                            PlayerName = "Warehouse Building 2",
                            PlayerUniqueId = "WAREHO-7364",
                            SiteId = "85965d81-c045-4a35-8523-82b64fdc0e87"
                        });
                });

            modelBuilder.Entity("SignageLivePlayer.Api.Data.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            RoleName = "SITEADMIN"
                        },
                        new
                        {
                            Id = "3",
                            RoleName = "USER"
                        });
                });

            modelBuilder.Entity("SignageLivePlayer.Api.Data.Models.Site", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteAddress1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteAddress2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteCountry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteCounty")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SitePostcode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SiteTown")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sites");

                    b.HasData(
                        new
                        {
                            Id = "72c77116-711b-449b-b57c-2b03239247b2",
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 792, DateTimeKind.Utc).AddTicks(9985),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 792, DateTimeKind.Utc).AddTicks(9990),
                            SiteAddress1 = "30 South Street",
                            SiteAddress2 = "",
                            SiteCountry = "",
                            SiteCounty = "",
                            SiteName = "Headquarters",
                            SitePostcode = "CB85 1RA",
                            SiteTown = "Cambridge"
                        },
                        new
                        {
                            Id = "d2cecbcb-9c4a-4890-af02-9854c11e402f",
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2715),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2718),
                            SiteAddress1 = "58 Grove Road",
                            SiteAddress2 = "",
                            SiteCountry = "",
                            SiteCounty = "",
                            SiteName = "London Branch",
                            SitePostcode = "CB85 1RA",
                            SiteTown = "East Central London"
                        },
                        new
                        {
                            Id = "85965d81-c045-4a35-8523-82b64fdc0e87",
                            DateCreated = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2735),
                            DateModified = new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2736),
                            SiteAddress1 = "54 York Road",
                            SiteAddress2 = "",
                            SiteCountry = "",
                            SiteCounty = "",
                            SiteName = "Manchester Branch",
                            SitePostcode = "M52 3RK",
                            SiteTown = "Manchester"
                        });
                });

            modelBuilder.Entity("SignageLivePlayer.Api.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "c0a955c4-5be6-477e-81b0-19edfaa32fbd",
                            Email = "admin",
                            FirstName = "",
                            LastName = "",
                            Password = "admin"
                        });
                });

            modelBuilder.Entity("SignageLivePlayer.Api.Data.Models.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "1",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "1",
                            RoleId = "3"
                        });
                });

            modelBuilder.Entity("SignageLivePlayer.Api.Data.Models.Player", b =>
                {
                    b.HasOne("SignageLivePlayer.Api.Data.Models.Site", "Site")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Site");
                });

            modelBuilder.Entity("SignageLivePlayer.Api.Data.Models.UserRole", b =>
                {
                    b.HasOne("SignageLivePlayer.Api.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}

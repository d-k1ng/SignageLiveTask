using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SignageLivePlayer.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SiteName = table.Column<string>(type: "TEXT", nullable: false),
                    SiteAddress1 = table.Column<string>(type: "TEXT", nullable: false),
                    SiteAddress2 = table.Column<string>(type: "TEXT", nullable: false),
                    SiteTown = table.Column<string>(type: "TEXT", nullable: false),
                    SiteCounty = table.Column<string>(type: "TEXT", nullable: false),
                    SitePostcode = table.Column<string>(type: "TEXT", nullable: false),
                    SiteCountry = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerUniqueId = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerName = table.Column<string>(type: "TEXT", nullable: false),
                    SiteId = table.Column<string>(type: "TEXT", nullable: true),
                    CheckInFrequency = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Sites_Id",
                        column: x => x.Id,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CheckInFrequency", "DateCreated", "DateModified", "PlayerName", "PlayerUniqueId", "SiteId" },
                values: new object[,]
                {
                    { "0570c4cd-40f8-427e-8ec1-c93bb214adaa", 100, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7982), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7982), "Marketing Office 2", "MARKET-3424", "d2cecbcb-9c4a-4890-af02-9854c11e402f" },
                    { "23e52508-2077-4927-802f-d5082475755a", 180, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7961), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7961), "Marketing Office 1", "MARKET-2278", "d2cecbcb-9c4a-4890-af02-9854c11e402f" },
                    { "5ae61ad4-62f0-455a-991a-b1baa412004c", 60, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(4769), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(4771), "Reception Large Screen", "RECEPT-0987", "72c77116-711b-449b-b57c-2b03239247b2" },
                    { "9810477b-4a99-4bd0-bfa1-d0022ab2f7cd", 60, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7908), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7909), "Reception Small Screen 2", "RECEPT-1986", "72c77116-711b-449b-b57c-2b03239247b2" },
                    { "9b56b7e5-5c1c-41e3-bf92-82b3ddc3e2b3", 30, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8007), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8008), "Warehouse Building 1", "WAREHO-3751", "85965d81-c045-4a35-8523-82b64fdc0e87" },
                    { "e13a59c8-09e7-4b4b-a926-2f4313218b45", 120, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7940), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7940), "Sales Office", "SALESO-5459", "72c77116-711b-449b-b57c-2b03239247b2" },
                    { "e350e0f9-c6af-4ed9-a81c-ac18d8648768", 60, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7882), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(7884), "Reception Small Screen 1", "RECEPT-1273", "72c77116-711b-449b-b57c-2b03239247b2" },
                    { "f9454690-fc43-4a23-b1c5-6440946e599e", 30, new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8028), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(8029), "Warehouse Building 2", "WAREHO-7364", "85965d81-c045-4a35-8523-82b64fdc0e87" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { "1", "ADMIN" },
                    { "2", "SITEADMIN" },
                    { "3", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "Id", "DateCreated", "DateModified", "SiteAddress1", "SiteAddress2", "SiteCountry", "SiteCounty", "SiteName", "SitePostcode", "SiteTown" },
                values: new object[,]
                {
                    { "72c77116-711b-449b-b57c-2b03239247b2", new DateTime(2024, 2, 24, 23, 18, 50, 792, DateTimeKind.Utc).AddTicks(9985), new DateTime(2024, 2, 24, 23, 18, 50, 792, DateTimeKind.Utc).AddTicks(9990), "30 South Street", "", "", "", "Headquarters", "CB85 1RA", "Cambridge" },
                    { "85965d81-c045-4a35-8523-82b64fdc0e87", new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2735), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2736), "54 York Road", "", "", "", "Manchester Branch", "M52 3RK", "Manchester" },
                    { "d2cecbcb-9c4a-4890-af02-9854c11e402f", new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2715), new DateTime(2024, 2, 24, 23, 18, 50, 793, DateTimeKind.Utc).AddTicks(2718), "58 Grove Road", "", "", "", "London Branch", "CB85 1RA", "East Central London" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { "c0a955c4-5be6-477e-81b0-19edfaa32fbd", "admin", "", "", "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "1" },
                    { "3", "1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

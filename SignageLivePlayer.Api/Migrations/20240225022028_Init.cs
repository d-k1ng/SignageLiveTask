using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SignageLivePlayer.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                    SiteAddress1 = table.Column<string>(type: "TEXT", nullable: true),
                    SiteAddress2 = table.Column<string>(type: "TEXT", nullable: true),
                    SiteTown = table.Column<string>(type: "TEXT", nullable: true),
                    SiteCounty = table.Column<string>(type: "TEXT", nullable: true),
                    SitePostcode = table.Column<string>(type: "TEXT", nullable: true),
                    SiteCountry = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: true)
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
                        name: "FK_Players_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id");
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
                    { "33c842e3-5b4e-41ba-aa26-21d4e8d1f48b", new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(5859), new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(5861), "58 Grove Road", "", "", "", "London Branch", "CB85 1RA", "East Central London" },
                    { "ed1d40d2-a23a-4c49-a2db-9ac21ac3c07f", new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(3188), new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(3196), "30 South Street", "", "", "", "Headquarters", "CB85 1RA", "Cambridge" },
                    { "fc841305-958b-4ce9-b0f6-a01467fca116", new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(5878), new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(5878), "54 York Road", "", "", "", "Manchester Branch", "M52 3RK", "Manchester" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { "29c183f5-054d-45ef-a5a9-7046562be1f3", "admin", "", "", "admin" },
                    { "877cd4eb-2c99-4166-8945-8904a3233bdc", "user", "", "", "user" },
                    { "c43662b0-1fdb-44a1-8873-182a1b4f6865", "siteadmin", "", "", "siteadmin" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CheckInFrequency", "DateCreated", "DateModified", "PlayerName", "PlayerUniqueId", "SiteId" },
                values: new object[,]
                {
                    { "0db5c8aa-8f41-41b7-ba7a-1bda237fae3f", 60, new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(609), new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(610), "Reception Small Screen 2", "RECEPT-1986", "ed1d40d2-a23a-4c49-a2db-9ac21ac3c07f" },
                    { "3fc0432f-0ab3-4243-ac2c-78e10db73ec3", 100, new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(678), new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(678), "Marketing Office 2", "MARKET-3424", "33c842e3-5b4e-41ba-aa26-21d4e8d1f48b" },
                    { "42ecce60-c4e7-4849-b6c8-f0ecbee4a699", 120, new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(632), new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(632), "Sales Office", "SALESO-5459", "ed1d40d2-a23a-4c49-a2db-9ac21ac3c07f" },
                    { "4d348e46-3363-403e-869a-dbd183e8a1ec", 30, new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(698), new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(699), "Warehouse Building 1", "WAREHO-3751", "fc841305-958b-4ce9-b0f6-a01467fca116" },
                    { "75d3cef6-7ec6-4d35-941e-58651f761118", 30, new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(719), new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(719), "Warehouse Building 2", "WAREHO-7364", "fc841305-958b-4ce9-b0f6-a01467fca116" },
                    { "9f50f77b-9ca2-4bd5-bcfa-571753efb640", 60, new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(7610), new DateTime(2024, 2, 25, 2, 20, 27, 455, DateTimeKind.Utc).AddTicks(7611), "Reception Large Screen", "RECEPT-0987", "ed1d40d2-a23a-4c49-a2db-9ac21ac3c07f" },
                    { "a76fe79b-aa76-427d-a0d5-b1e668e44698", 180, new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(653), new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(654), "Marketing Office 1", "MARKET-2278", "33c842e3-5b4e-41ba-aa26-21d4e8d1f48b" },
                    { "b4052722-99ed-4ffe-81ab-0d1cd923478b", 60, new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(571), new DateTime(2024, 2, 25, 2, 20, 27, 456, DateTimeKind.Utc).AddTicks(572), "Reception Small Screen 1", "RECEPT-1273", "ed1d40d2-a23a-4c49-a2db-9ac21ac3c07f" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "29c183f5-054d-45ef-a5a9-7046562be1f3" },
                    { "2", "29c183f5-054d-45ef-a5a9-7046562be1f3" },
                    { "3", "29c183f5-054d-45ef-a5a9-7046562be1f3" },
                    { "3", "877cd4eb-2c99-4166-8945-8904a3233bdc" },
                    { "2", "c43662b0-1fdb-44a1-8873-182a1b4f6865" },
                    { "3", "c43662b0-1fdb-44a1-8873-182a1b4f6865" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_SiteId",
                table: "Players",
                column: "SiteId");

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

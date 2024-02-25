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
                    { "72bc9fb6-ac2d-4747-9bc4-741633217a7b", new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(2754), new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(2759), "30 South Street", "", "", "", "Headquarters", "CB85 1RA", "Cambridge" },
                    { "75f2fef8-eac0-4e2b-8d6b-d761454c3630", new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(6131), new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(6131), "54 York Road", "", "", "", "Manchester Branch", "M52 3RK", "Manchester" },
                    { "aa1e0f26-20e4-4a28-be6e-c84dbc2834fe", new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(6114), new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(6116), "58 Grove Road", "", "", "", "London Branch", "CB85 1RA", "East Central London" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { "6d6a41e6-5e9a-4f43-8129-01a426944d52", "admin", "", "", "admin" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CheckInFrequency", "DateCreated", "DateModified", "PlayerName", "PlayerUniqueId", "SiteId" },
                values: new object[,]
                {
                    { "4a8de9eb-fa69-4572-8b6f-db434dda35d7", 60, new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(8194), new DateTime(2024, 2, 25, 0, 11, 20, 267, DateTimeKind.Utc).AddTicks(8196), "Reception Large Screen", "RECEPT-0987", "72bc9fb6-ac2d-4747-9bc4-741633217a7b" },
                    { "5446d3ae-df13-4a0c-9b13-6e38a6044735", 30, new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1571), new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1571), "Warehouse Building 2", "WAREHO-7364", "75f2fef8-eac0-4e2b-8d6b-d761454c3630" },
                    { "5f8f5244-055e-4be3-bbe3-33c232b07ea0", 60, new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1456), new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1457), "Reception Small Screen 2", "RECEPT-1986", "72bc9fb6-ac2d-4747-9bc4-741633217a7b" },
                    { "8a64d7e1-9c10-477b-aa83-edb1955f1702", 180, new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1503), new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1504), "Marketing Office 1", "MARKET-2278", "aa1e0f26-20e4-4a28-be6e-c84dbc2834fe" },
                    { "9a3f4818-035b-40ce-932a-f4a4bb356f8f", 100, new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1524), new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1525), "Marketing Office 2", "MARKET-3424", "aa1e0f26-20e4-4a28-be6e-c84dbc2834fe" },
                    { "e18500c9-1a4c-4cf3-94d9-f87ff6073519", 60, new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1431), new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1433), "Reception Small Screen 1", "RECEPT-1273", "72bc9fb6-ac2d-4747-9bc4-741633217a7b" },
                    { "ed4f2224-74ce-48b9-894c-616568ed9c5b", 120, new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1478), new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1478), "Sales Office", "SALESO-5459", "72bc9fb6-ac2d-4747-9bc4-741633217a7b" },
                    { "f889af3b-9978-4375-bbd8-4e223050c59e", 30, new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1549), new DateTime(2024, 2, 25, 0, 11, 20, 268, DateTimeKind.Utc).AddTicks(1550), "Warehouse Building 1", "WAREHO-3751", "75f2fef8-eac0-4e2b-8d6b-d761454c3630" }
                });

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

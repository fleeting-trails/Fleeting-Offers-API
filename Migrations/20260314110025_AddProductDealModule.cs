using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FleetingOffers.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDealModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "22a67824-c660-4970-b735-1efcb12785fa");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2ae008ad-60c5-497c-91b8-68c501f7f908");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "b2f0b6e7-ef35-45cc-9b64-8e90df8dce26");

            migrationBuilder.AddColumn<string>(
                name: "DealId",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductDeals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    ImageId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDeals_Uploads_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Uploads",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsPasswordSet", "LastLoggedIn", "OtpId", "PasswordId", "RestrictedUserSubRoleId", "Role", "UserSubRoleId", "Username" },
                values: new object[,]
                {
                    { "7580487a-a051-49d8-a92c-e8978d8a6e64", new DateTime(2026, 3, 14, 11, 0, 24, 458, DateTimeKind.Utc).AddTicks(8740), "fleetingtrails@gmail.com", "Fleeting Trails", false, null, null, null, null, 2, null, "fleeting_trails" },
                    { "9a5a9ee4-c4f7-40af-88a4-578d0d7f5555", new DateTime(2026, 3, 14, 11, 0, 24, 458, DateTimeKind.Utc).AddTicks(8733), "samaheerzameel@gmail.com", "Samaheer Zameel", false, null, null, null, null, 1, null, "samaheer_zameel" },
                    { "e537011b-7e56-496d-89df-83d735d1cf1b", new DateTime(2026, 3, 14, 11, 0, 24, 458, DateTimeKind.Utc).AddTicks(8670), "abtahitajwar@gmail.com", "Abtahi Tajwar", false, null, null, null, null, 0, null, "abtahi_tajwar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DealId",
                table: "Products",
                column: "DealId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDeals_ImageId",
                table: "ProductDeals",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductDeals_DealId",
                table: "Products",
                column: "DealId",
                principalTable: "ProductDeals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductDeals_DealId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductDeals");

            migrationBuilder.DropIndex(
                name: "IX_Products_DealId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "7580487a-a051-49d8-a92c-e8978d8a6e64");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "9a5a9ee4-c4f7-40af-88a4-578d0d7f5555");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "e537011b-7e56-496d-89df-83d735d1cf1b");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsPasswordSet", "LastLoggedIn", "OtpId", "PasswordId", "RestrictedUserSubRoleId", "Role", "UserSubRoleId", "Username" },
                values: new object[,]
                {
                    { "22a67824-c660-4970-b735-1efcb12785fa", new DateTime(2026, 1, 9, 10, 40, 34, 312, DateTimeKind.Utc).AddTicks(9267), "samaheerzameel@gmail.com", "Samaheer Zameel", false, null, null, null, null, 1, null, "samaheer_zameel" },
                    { "2ae008ad-60c5-497c-91b8-68c501f7f908", new DateTime(2026, 1, 9, 10, 40, 34, 312, DateTimeKind.Utc).AddTicks(9197), "abtahitajwar@gmail.com", "Abtahi Tajwar", false, null, null, null, null, 0, null, "abtahi_tajwar" },
                    { "b2f0b6e7-ef35-45cc-9b64-8e90df8dce26", new DateTime(2026, 1, 9, 10, 40, 34, 312, DateTimeKind.Utc).AddTicks(9274), "fleetingtrails@gmail.com", "Fleeting Trails", false, null, null, null, null, 2, null, "fleeting_trails" }
                });
        }
    }
}

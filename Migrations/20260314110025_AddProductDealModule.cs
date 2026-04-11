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

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Products");
        }
    }
}

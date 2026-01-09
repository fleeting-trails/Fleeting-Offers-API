using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FleetingOffers.Migrations
{
    /// <inheritdoc />
    public partial class AddProductModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "5265c6ed-1d84-4488-9a26-03f38559861b");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "55e1b244-226b-4b2e-8706-50843f99debc");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "5afb18fe-bfae-444c-bd3c-2df9dc12497a");

            migrationBuilder.CreateTable(
                name: "ProductCategories",
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
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Uploads_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Uploads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductIndustries",
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
                    table.PrimaryKey("PK_ProductIndustries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductIndustries_Uploads_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Uploads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Subtitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CoverImageId = table.Column<string>(type: "text", nullable: true),
                    ThumbnailImageId = table.Column<string>(type: "text", nullable: true),
                    CategoryId = table.Column<string>(type: "text", nullable: true),
                    SubCategoryId = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductIndustries_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "ProductIndustries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Uploads_CoverImageId",
                        column: x => x.CoverImageId,
                        principalTable: "Uploads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Uploads_ThumbnailImageId",
                        column: x => x.ThumbnailImageId,
                        principalTable: "Uploads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdditionalImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    ImageId = table.Column<string>(type: "text", nullable: false),
                    ProductEntityId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdditionalImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAdditionalImages_Products_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductAdditionalImages_Uploads_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Uploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOwners",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    OwnershipType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOwners_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOwners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    ProductEntityId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsPasswordSet", "LastLoggedIn", "OtpId", "PasswordId", "RestrictedUserSubRoleId", "Role", "UserSubRoleId", "Username" },
                values: new object[,]
                {
                    { "22a67824-c660-4970-b735-1efcb12785fa", new DateTime(2026, 1, 9, 10, 40, 34, 312, DateTimeKind.Utc).AddTicks(9267), "samaheerzameel@gmail.com", "Samaheer Zameel", false, null, null, null, null, 1, null, "samaheer_zameel" },
                    { "2ae008ad-60c5-497c-91b8-68c501f7f908", new DateTime(2026, 1, 9, 10, 40, 34, 312, DateTimeKind.Utc).AddTicks(9197), "abtahitajwar@gmail.com", "Abtahi Tajwar", false, null, null, null, null, 0, null, "abtahi_tajwar" },
                    { "b2f0b6e7-ef35-45cc-9b64-8e90df8dce26", new DateTime(2026, 1, 9, 10, 40, 34, 312, DateTimeKind.Utc).AddTicks(9274), "fleetingtrails@gmail.com", "Fleeting Trails", false, null, null, null, null, 2, null, "fleeting_trails" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalImages_ImageId",
                table: "ProductAdditionalImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdditionalImages_ProductEntityId",
                table: "ProductAdditionalImages",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ImageId",
                table: "ProductCategories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIndustries_ImageId",
                table: "ProductIndustries",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOwners_ProductId",
                table: "ProductOwners",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOwners_UserId",
                table: "ProductOwners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CoverImageId",
                table: "Products",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ThumbnailImageId",
                table: "Products",
                column: "ThumbnailImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductEntityId",
                table: "ProductTags",
                column: "ProductEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAdditionalImages");

            migrationBuilder.DropTable(
                name: "ProductOwners");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductIndustries");

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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsPasswordSet", "LastLoggedIn", "OtpId", "PasswordId", "RestrictedUserSubRoleId", "Role", "UserSubRoleId", "Username" },
                values: new object[,]
                {
                    { "5265c6ed-1d84-4488-9a26-03f38559861b", new DateTime(2025, 10, 24, 11, 33, 15, 777, DateTimeKind.Utc).AddTicks(2466), "samaheerzameel@gmail.com", "Samaheer Zameel", false, null, null, null, null, 1, null, "samaheer_zameel" },
                    { "55e1b244-226b-4b2e-8706-50843f99debc", new DateTime(2025, 10, 24, 11, 33, 15, 777, DateTimeKind.Utc).AddTicks(2473), "fleetingtrails@gmail.com", "Fleeting Trails", false, null, null, null, null, 2, null, "fleeting_trails" },
                    { "5afb18fe-bfae-444c-bd3c-2df9dc12497a", new DateTime(2025, 10, 24, 11, 33, 15, 777, DateTimeKind.Utc).AddTicks(2391), "abtahitajwar@gmail.com", "Abtahi Tajwar", false, null, null, null, null, 0, null, "abtahi_tajwar" }
                });
        }
    }
}

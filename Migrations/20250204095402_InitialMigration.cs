using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetingOffers.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertiseDealTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseDealTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthOtps",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    OtpValue = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpireAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthOtps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    OriginalName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    URL = table.Column<string>(type: "TEXT", nullable: false),
                    Storage = table.Column<int>(type: "INTEGER", nullable: false),
                    IsUsed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Lat = table.Column<double>(type: "REAL", nullable: false),
                    Long = table.Column<double>(type: "REAL", nullable: false),
                    AddressText = table.Column<string>(type: "TEXT", nullable: true),
                    District = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Upazila = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Union = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Village = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Area = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passwords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    HashValue = table.Column<string>(type: "TEXT", nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passwords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberAuthProviders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Provider = table.Column<int>(type: "INTEGER", nullable: false),
                    ProviderId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberAuthProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSubRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    ImageId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseCategories_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseIndustries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    ImageId = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseIndustries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseIndustries_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CoverImageId = table.Column<string>(type: "TEXT", nullable: true),
                    ThumbnailImageId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_Files_CoverImageId",
                        column: x => x.CoverImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Campaigns_Files_ThumbnailImageId",
                        column: x => x.ThumbnailImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Subtitle = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CoverImageId = table.Column<string>(type: "TEXT", nullable: true),
                    ProfileImageId = table.Column<string>(type: "TEXT", nullable: true),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    LocationId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationProfiles_Files_CoverImageId",
                        column: x => x.CoverImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationProfiles_Files_ProfileImageId",
                        column: x => x.ProfileImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrganizationProfiles_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ProfilePicture = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PasswordId = table.Column<string>(type: "TEXT", nullable: true),
                    AuthProviderUsed = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProvideInfoId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Passwords_PasswordId",
                        column: x => x.PasswordId,
                        principalTable: "Passwords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subscribers_SubscriberAuthProviders_ProvideInfoId",
                        column: x => x.ProvideInfoId,
                        principalTable: "SubscriberAuthProviders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubRoleId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionString = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_UserSubRoles_SubRoleId",
                        column: x => x.SubRoleId,
                        principalTable: "UserSubRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    RestrictedUserSubRoleId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserSubRoleId = table.Column<string>(type: "TEXT", nullable: true),
                    LastLoggedIn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsPasswordSet = table.Column<bool>(type: "INTEGER", nullable: false),
                    OtpId = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_AuthOtps_OtpId",
                        column: x => x.OtpId,
                        principalTable: "AuthOtps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Passwords_PasswordId",
                        column: x => x.PasswordId,
                        principalTable: "Passwords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UserSubRoles_UserSubRoleId",
                        column: x => x.UserSubRoleId,
                        principalTable: "UserSubRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Advertises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Subtitle = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CoverImageId = table.Column<string>(type: "TEXT", nullable: true),
                    ThumbnailImageId = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: true),
                    SubCategoryId = table.Column<string>(type: "TEXT", nullable: true),
                    DealTypeId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertises_AdvertiseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AdvertiseCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Advertises_AdvertiseDealTypes_DealTypeId",
                        column: x => x.DealTypeId,
                        principalTable: "AdvertiseDealTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Advertises_AdvertiseIndustries_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "AdvertiseIndustries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Advertises_Files_CoverImageId",
                        column: x => x.CoverImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Advertises_Files_ThumbnailImageId",
                        column: x => x.ThumbnailImageId,
                        principalTable: "Files",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProfileEmails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileId = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProfileEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationProfileEmails_OrganizationProfiles_OrganizationProfileEntityId",
                        column: x => x.OrganizationProfileEntityId,
                        principalTable: "OrganizationProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProfileExtraImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileId = table.Column<string>(type: "TEXT", nullable: false),
                    ImageId = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProfileExtraImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationProfileExtraImages_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationProfileExtraImages_OrganizationProfiles_OrganizationProfileEntityId",
                        column: x => x.OrganizationProfileEntityId,
                        principalTable: "OrganizationProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationProfilePhones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileId = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationProfilePhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationProfilePhones_OrganizationProfiles_OrganizationProfileEntityId",
                        column: x => x.OrganizationProfileEntityId,
                        principalTable: "OrganizationProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSocialMedias",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Link = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileId = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationProfileEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSocialMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationSocialMedias_OrganizationProfiles_OrganizationProfileEntityId",
                        column: x => x.OrganizationProfileEntityId,
                        principalTable: "OrganizationProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriberInitialPreferenceCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubscriberId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: false),
                    SubscriberEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberInitialPreferenceCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberInitialPreferenceCategories_Subscribers_SubscriberEntityId",
                        column: x => x.SubscriberEntityId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriberInitialPreferenceIndustries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubscriberId = table.Column<string>(type: "TEXT", nullable: false),
                    IndustryId = table.Column<string>(type: "TEXT", nullable: false),
                    SubscriberEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberInitialPreferenceIndustries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberInitialPreferenceIndustries_Subscribers_SubscriberEntityId",
                        column: x => x.SubscriberEntityId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuthTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceSignature = table.Column<string>(type: "TEXT", nullable: true),
                    Expiration = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthTokens_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseAdditionalImageEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    ImageId = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseAdditionalImageEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseAdditionalImageEntity_Advertises_AdvertiseEntityId",
                        column: x => x.AdvertiseEntityId,
                        principalTable: "Advertises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdvertiseAdditionalImageEntity_Files_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseAnalytics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    Views = table.Column<int>(type: "INTEGER", nullable: false),
                    Clicks = table.Column<int>(type: "INTEGER", nullable: false),
                    Conversions = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseAnalytics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseAnalytics_Advertises_AdvertiseId",
                        column: x => x.AdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseLocations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    LocationId = table.Column<string>(type: "TEXT", nullable: true),
                    AdvertiseEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseLocations_Advertises_AdvertiseEntityId",
                        column: x => x.AdvertiseEntityId,
                        principalTable: "Advertises",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdvertiseLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseOwners",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    OwnershipType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseOwners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseOwners_Advertises_AdvertiseId",
                        column: x => x.AdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertiseOwners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseRelatedAdvertises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    RelatedAdvertiseId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseRelatedAdvertises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseRelatedAdvertises_Advertises_RelatedAdvertiseId",
                        column: x => x.RelatedAdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvertiseTags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiseTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertiseTags_Advertises_AdvertiseEntityId",
                        column: x => x.AdvertiseEntityId,
                        principalTable: "Advertises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CampaignAdvertises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CampaignId = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    CampaignEntityId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignAdvertises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignAdvertises_Advertises_AdvertiseId",
                        column: x => x.AdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignAdvertises_Campaigns_CampaignEntityId",
                        column: x => x.CampaignEntityId,
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriberFavouriteAdvertises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SubscriberId = table.Column<string>(type: "TEXT", nullable: false),
                    AdvertiseId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberFavouriteAdvertises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberFavouriteAdvertises_Advertises_AdvertiseId",
                        column: x => x.AdvertiseId,
                        principalTable: "Advertises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriberFavouriteAdvertises_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseAdditionalImageEntity_AdvertiseEntityId",
                table: "AdvertiseAdditionalImageEntity",
                column: "AdvertiseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseAdditionalImageEntity_ImageId",
                table: "AdvertiseAdditionalImageEntity",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseAnalytics_AdvertiseId",
                table: "AdvertiseAnalytics",
                column: "AdvertiseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseCategories_ImageId",
                table: "AdvertiseCategories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseIndustries_ImageId",
                table: "AdvertiseIndustries",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseLocations_AdvertiseEntityId",
                table: "AdvertiseLocations",
                column: "AdvertiseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseLocations_LocationId",
                table: "AdvertiseLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseOwners_AdvertiseId",
                table: "AdvertiseOwners",
                column: "AdvertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseOwners_UserId",
                table: "AdvertiseOwners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseRelatedAdvertises_RelatedAdvertiseId",
                table: "AdvertiseRelatedAdvertises",
                column: "RelatedAdvertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_CategoryId",
                table: "Advertises",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_CoverImageId",
                table: "Advertises",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_DealTypeId",
                table: "Advertises",
                column: "DealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_SubCategoryId",
                table: "Advertises",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertises_ThumbnailImageId",
                table: "Advertises",
                column: "ThumbnailImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertiseTags_AdvertiseEntityId",
                table: "AdvertiseTags",
                column: "AdvertiseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_UserEntityId",
                table: "AuthTokens",
                column: "UserEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignAdvertises_AdvertiseId",
                table: "CampaignAdvertises",
                column: "AdvertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignAdvertises_CampaignEntityId",
                table: "CampaignAdvertises",
                column: "CampaignEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CoverImageId",
                table: "Campaigns",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ThumbnailImageId",
                table: "Campaigns",
                column: "ThumbnailImageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfileEmails_OrganizationProfileEntityId",
                table: "OrganizationProfileEmails",
                column: "OrganizationProfileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfileExtraImages_ImageId",
                table: "OrganizationProfileExtraImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfileExtraImages_OrganizationProfileEntityId",
                table: "OrganizationProfileExtraImages",
                column: "OrganizationProfileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfilePhones_OrganizationProfileEntityId",
                table: "OrganizationProfilePhones",
                column: "OrganizationProfileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfiles_CoverImageId",
                table: "OrganizationProfiles",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfiles_LocationId",
                table: "OrganizationProfiles",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationProfiles_ProfileImageId",
                table: "OrganizationProfiles",
                column: "ProfileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationSocialMedias_OrganizationProfileEntityId",
                table: "OrganizationSocialMedias",
                column: "OrganizationProfileEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberFavouriteAdvertises_AdvertiseId",
                table: "SubscriberFavouriteAdvertises",
                column: "AdvertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberFavouriteAdvertises_SubscriberId",
                table: "SubscriberFavouriteAdvertises",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberInitialPreferenceCategories_SubscriberEntityId",
                table: "SubscriberInitialPreferenceCategories",
                column: "SubscriberEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberInitialPreferenceIndustries_SubscriberEntityId",
                table: "SubscriberInitialPreferenceIndustries",
                column: "SubscriberEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_PasswordId",
                table: "Subscribers",
                column: "PasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_ProvideInfoId",
                table: "Subscribers",
                column: "ProvideInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_SubRoleId",
                table: "UserPermissions",
                column: "SubRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OtpId",
                table: "Users",
                column: "OtpId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordId",
                table: "Users",
                column: "PasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserSubRoleId",
                table: "Users",
                column: "UserSubRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertiseAdditionalImageEntity");

            migrationBuilder.DropTable(
                name: "AdvertiseAnalytics");

            migrationBuilder.DropTable(
                name: "AdvertiseLocations");

            migrationBuilder.DropTable(
                name: "AdvertiseOwners");

            migrationBuilder.DropTable(
                name: "AdvertiseRelatedAdvertises");

            migrationBuilder.DropTable(
                name: "AdvertiseTags");

            migrationBuilder.DropTable(
                name: "AuthTokens");

            migrationBuilder.DropTable(
                name: "CampaignAdvertises");

            migrationBuilder.DropTable(
                name: "OrganizationProfileEmails");

            migrationBuilder.DropTable(
                name: "OrganizationProfileExtraImages");

            migrationBuilder.DropTable(
                name: "OrganizationProfilePhones");

            migrationBuilder.DropTable(
                name: "OrganizationSocialMedias");

            migrationBuilder.DropTable(
                name: "SubscriberFavouriteAdvertises");

            migrationBuilder.DropTable(
                name: "SubscriberInitialPreferenceCategories");

            migrationBuilder.DropTable(
                name: "SubscriberInitialPreferenceIndustries");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "OrganizationProfiles");

            migrationBuilder.DropTable(
                name: "Advertises");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "AuthOtps");

            migrationBuilder.DropTable(
                name: "UserSubRoles");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "AdvertiseCategories");

            migrationBuilder.DropTable(
                name: "AdvertiseDealTypes");

            migrationBuilder.DropTable(
                name: "AdvertiseIndustries");

            migrationBuilder.DropTable(
                name: "Passwords");

            migrationBuilder.DropTable(
                name: "SubscriberAuthProviders");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}

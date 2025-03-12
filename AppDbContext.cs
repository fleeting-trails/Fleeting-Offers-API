using System;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Module.Auth;
using FleetingOffers.Module.Campaign;
using FleetingOffers.Module.File;
using FleetingOffers.Module.Location;
using FleetingOffers.Module.Subscriber;
using FleetingOffers.Module.User;
using Microsoft.EntityFrameworkCore;

namespace FleetingOffers;

public class AppDbContext(DbContextOptions<AppDbContext> options)  : DbContext(options)
{

    // User Module
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserSubRoleEntity> UserSubRoles { get; set; }
    public DbSet<OrganizationProfileEntity> OrganizationProfiles { get; set; }
    public DbSet<OrganizationProfileExtraImageEntity> OrganizationProfileExtraImages { get; set; }
    public DbSet<OrganizationProfilePhoneEntity> OrganizationProfilePhones { get; set; }
    public DbSet<OrganizationProfileEmailEntity> OrganizationProfileEmails { get; set; }
    public DbSet<OrganizationSocialMediaEntity> OrganizationSocialMedias { get; set; }

    // Auth Module
    public DbSet<AuthOtpEntity> AuthOtps { get; set; }
    public DbSet<AuthTokenEntity> AuthTokens { get; set; }
    public DbSet<PasswordEntity> Passwords { get; set; }
    public DbSet<UserPermissionEntity> UserPermissions { get; set; }


    // Advertise Module
    public DbSet<AdvertiseEntity> Advertises { get; set; }
    public DbSet<AdvertiseOwnerEntity> AdvertiseOwners { get; set; }
    public DbSet<AdvertiseLocationEntity> AdvertiseLocations { get; set; }
    public DbSet<AdvertiseDealTypeEntity> AdvertiseDealTypes { get; set; }
    public DbSet<AdvertiseRelatedAdvertiseEntity> AdvertiseRelatedAdvertises { get; set; }
    public DbSet<AdvertiseCategoryEntity> AdvertiseCategories { get; set; }
    public DbSet<AdvertiseIndustryEntity> AdvertiseIndustries { get; set; }
    public DbSet<AdvertiseTagEntity> AdvertiseTags { get; set; }
    public DbSet<AdvertiseAnalyticsEntity> AdvertiseAnalytics { get; set; }


    // Campaign Module
    public DbSet<CampaignEntity> Campaigns { get; set; }
    public DbSet<CampaignAdvertiseEntity> CampaignAdvertises { get; set; }

    // Subscriber Module
    public DbSet<SubscriberEntity> Subscribers { get; set; }
    public DbSet<SubscriberAuthProviderEntity> SubscriberAuthProviders { get; set; }
    public DbSet<SubscriberInitialPreferenceCategoryEntity> SubscriberInitialPreferenceCategories { get; set; }
    public DbSet<SubscriberInitialPreferenceIndustryEntity> SubscriberInitialPreferenceIndustries { get; set; }
    public DbSet<SubscriberFavouriteAdvertiseEntity> SubscriberFavouriteAdvertises { get; set; }

    // File Module
    public DbSet<FileEntity> Files { get; set; }

    // Location Module
    public DbSet<LocationEntity> Locations { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSeeding((context, _) =>
        {
            AdvertiseDealTypesSeeder.Seed(context);
            SuperAdminSeeder.Seed(context);
        });
}

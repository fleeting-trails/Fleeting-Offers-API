using System;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Module.Auth;
using FleetingOffers.Module.Campaign;
using FleetingOffers.Module.Upload;
using FleetingOffers.Module.Location;
using FleetingOffers.Module.Subscriber;
using FleetingOffers.Module.User;
using Microsoft.EntityFrameworkCore;

namespace FleetingOffers;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
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
    public DbSet<UploadEntity> Uploads { get; set; }

    // Location Module
    public DbSet<LocationEntity> Locations { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseSeeding((context, _) =>
        {
            AdvertiseDealTypesSeeder.Seed(context);
            SuperAdminSeeder.Seed(context);
        });


    // Update the upload count when an entity is added, deleted or modified
    public int SaveChangesWithUploads(string[] fileRefs)
    {
        var changes = ChangeTracker.Entries().ToList();

        foreach (var entry in changes)
        {
            var entity = entry.Entity;
            foreach (var fileRef in fileRefs)
            {

                var propertyInfo = entity.GetType().GetProperty(fileRef);
                if (propertyInfo == null) continue; // Skip if entity does not have this property

                var fileId = propertyInfo.GetValue(entity)?.ToString();
                if (string.IsNullOrEmpty(fileId)) continue; // Skip null or empty file IDs

                if (entry.State == EntityState.Added)
                {
                    this.Database.ExecuteSqlRaw($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} + 1 WHERE Id = {0}", fileId);
                }
                else if (entry.State == EntityState.Deleted)
                {
                    this.Database.ExecuteSqlRaw($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} - 1 WHERE Id = {0}", fileId);
                }
                else if (entry.State == EntityState.Modified)
                {
                    var oldFileId = entry.OriginalValues["FileId"] as string;
                    var newFileId = fileId;

                    if (oldFileId != newFileId)
                    {
                        if (oldFileId != null)
                        {
                            this.Database.ExecuteSqlRaw($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} - 1 WHERE Id = {0}", oldFileId);
                        }

                        if (newFileId != null)
                        {
                            this.Database.ExecuteSqlRaw($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} + 1 WHERE Id = {0}", newFileId);
                        }
                    }
                }
            }
        }

        return base.SaveChanges();
    }

}

using System;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Module.Auth;
using FleetingOffers.Module.Campaign;
using FleetingOffers.Module.Upload;
using FleetingOffers.Module.Location;
using FleetingOffers.Module.Subscriber;
using FleetingOffers.Module.User;
using FleetingOffers.Module.Product;
using Microsoft.EntityFrameworkCore;
using FleetingOffers.Util.Helper;
using System.Diagnostics;

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

    // Product Module
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductOwnerEntity> ProductOwners { get; set; }
    public DbSet<ProductAdditionalImageEntity> ProductAdditionalImages { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<ProductIndustryEntity> ProductIndustries { get; set; }
    public DbSet<ProductDealEntity> ProductDeals { get; set; }
    public DbSet<ProductTagEntity> ProductTags { get; set; }

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



    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // => optionsBuilder
    //     .UseSeeding((context, _) =>
    //     {
    //         AdvertiseDealTypesSeeder.Seed(context);
    //         UserSeeder.Seed(context);
    //     });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var users = UserSeeder.GetSeedData();
        modelBuilder.Entity<UserEntity>().HasData(users);

        // Other HasData seeding...
    }



    public record SaveChangesWithUploadsAsyncPropsDto(
        string Key,
        string[] AllowedTypes
    );
    // Update the upload count when an entity is added, deleted or modified
    public async Task<int> SaveChangesWithUploadsAsync(SaveChangesWithUploadsAsyncPropsDto[] fileRefs)
    {
        var changes = ChangeTracker.Entries().ToList();

        foreach (var entry in changes)
        {
            var entity = entry.Entity;
            foreach (var fileRef in fileRefs)
            {

                var propertyInfo = entity.GetType().GetProperty(fileRef.Key);
                if (propertyInfo == null) continue; // Skip if entity does not have this property

                var fileId = propertyInfo.GetValue(entity)?.ToString();
                if (string.IsNullOrEmpty(fileId)) continue; // Skip null or empty file IDs
                var uploadEntity = await this.Uploads.FindAsync(fileId);
                if (uploadEntity == null)
                {
                    throw new Exception($"UPLOAD_404: File {fileId} of property {fileRef.Key} is not found. Please refer a valid file"); // Skip if upload entity not found
                }

                if (!Helper.IsAllowedMimeType(uploadEntity.MimeType, fileRef.AllowedTypes))
                {
                    throw new Exception($"WRONG_MIMTYPE: MIME type not allowed for File {fileId}. Only {string.Join(",", fileRef.AllowedTypes)}");// Check if the file type is allowed
                }
                if (entry.State == EntityState.Added)
                {
                    Console.WriteLine($"Updating usage for fileId: {fileId}");
                    Debug.WriteLine($"Updating usage for fileId: {fileId}");

                    // await this.Database.ExecuteSqlRawAsync($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} + 1 WHERE Id = @p0", new object[] { fileId });
                    await this.Database.ExecuteSqlInterpolatedAsync($"UPDATE \"Uploads\" SET \"NumberOfUsage\" = \"NumberOfUsage\" + 1 WHERE \"Id\" = {fileId}");

                }
                else if (entry.State == EntityState.Deleted)
                {
                    await this.Database.ExecuteSqlRawAsync($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} - 1 WHERE Id = @p0", new object[] { fileId });
                }
                else if (entry.State == EntityState.Modified)
                {
                    Debug.WriteLine($"FileRef: {fileRef.Key}");
                    var oldFileId = entry.OriginalValues[fileRef.Key]?.ToString();
                    var newFileId = fileId;

                    if (oldFileId != newFileId)
                    {
                        if (oldFileId != null)
                        {
                            Debug.WriteLine($"Updating usage for fileId: {oldFileId}");
                            await this.Database.ExecuteSqlRawAsync($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} - 1 WHERE Id = @p0", new object[] { oldFileId });
                        }

                        if (newFileId != null)
                        {
                            Debug.WriteLine($"Updating usage for fileId: {newFileId}");
                            await this.Database.ExecuteSqlRawAsync($"UPDATE {nameof(Uploads)} SET {nameof(UploadEntity.NumberOfUsage)} = {nameof(UploadEntity.NumberOfUsage)} + 1 WHERE Id = @p0", new object[] { newFileId });
                        }
                    }
                }
            }
        }

        return base.SaveChanges();
    }

}

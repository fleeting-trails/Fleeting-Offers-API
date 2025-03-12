
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetingOffers.Module.Advertise;
using FleetingOffers.Module.Auth;

namespace FleetingOffers.Module.Subscriber;


public class SubscriberEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string? MiddleName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    public byte[]? ProfilePicture { get; set; }  // Optional profile picture as byte array

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public PasswordEntity? Password { get; set; }  // One-to-One Relationship

    [Required]
    public bool AuthProviderUsed { get; set; } = false;

    public SubscriberAuthProviderEntity? ProvideInfo { get; set; }  // One-to-One Relationship

    // Navigation Properties for Related Entities
    public ICollection<SubscriberInitialPreferenceCategoryEntity> InitialPreferenceCategories { get; set; }
    public ICollection<SubscriberInitialPreferenceIndustryEntity> InitialPreferenceIndustries { get; set; }
    public ICollection<SubscriberFavouriteAdvertiseEntity> FavouriteAdvertises { get; set; }
}

public class SubscriberAuthProviderEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }  // Auto-generated ID


    [Required]
    public AUTH_PROVIDER Provider { get; set; }  // Enum for providers (e.g., Google, Facebook)

    [Required]
    public string ProviderId { get; set; }  // Unique ID from the provider

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

}

public class SubscriberInitialPreferenceCategoryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Subscribers")]
    public string SubscriberId { get; set; }  // Foreign Key to Subscribers

    [ForeignKey("AdvertiseCategories")]
    public string CategoryId { get; set; }   // Foreign Key to AdvertiseCategories (nullable)
}


public class SubscriberInitialPreferenceIndustryEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Subscribers")]
    public string SubscriberId { get; set; }  // Foreign Key to Subscribers

    [ForeignKey("AdvertiseIndustries")]
    public string IndustryId { get; set; }   // Foreign Key to AdvertiseIndustries (nullable)
}

public class SubscriberFavouriteAdvertiseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [ForeignKey("Subscribers")]
    public string SubscriberId { get; set; }  // Foreign Key to Subscribers

    [Required]
    [ForeignKey("Advertises")]
    public string AdvertiseId { get; set; }   // Foreign Key to Advertises

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public SubscriberEntity Subscriber { get; set; }
    public AdvertiseEntity Advertise { get; set; }
}





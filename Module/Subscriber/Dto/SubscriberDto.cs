using FleetingOffers.Module.Auth;

namespace FleetingOffers.Module.Subscriber;


public class SubscriberDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public byte[]? ProfilePicture { get; set; }  // Optional profile picture as byte array

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool AuthProviderUsed { get; set; } = false;

}

public class SubscriberAuthProviderDto
{
    public string Id { get; set; }  // Auto-generated ID

    public AUTH_PROVIDER Provider { get; set; }  // Enum for providers (e.g., Google, Facebook)

    public string ProviderId { get; set; }  // Unique ID from the provider

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

}

public class SubscriberInitialPreferenceCategoryDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string SubscriberId { get; set; }  // Foreign Key to Subscribers

    public string? CategoryId { get; set; }  // Foreign Key to AdvertiseCategories (nullable)
}

public class SubscriberInitialPreferenceIndustryDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string SubscriberId { get; set; }  // Foreign Key to Subscribers

    public string? IndustryId { get; set; }  // Foreign Key to AdvertiseIndustries (nullable)
}

public class SubscriberFavouriteAdvertiseDto
{
    public string Id { get; set; }  // Auto-generated ID

    public string SubscriberId { get; set; }  // Foreign Key to Subscribers

    public string? AdvertiseId { get; set; }  // Foreign Key to Advertises (nullable)
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}





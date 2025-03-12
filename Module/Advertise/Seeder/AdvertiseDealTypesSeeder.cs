using Microsoft.EntityFrameworkCore;

namespace FleetingOffers.Module.Advertise;

class AdvertiseDealTypesSeeder
{
    public static void Seed(DbContext dbContext)
    {
        // Seeder for AdvertiseDealTypeEntity (without IDs)
        string[] advertiseDealTypes =
            [
                "Discount-Base",
                "Freebie-Based",
                "Cashback and Store",
                "Time-Based",
                "Subscription-Based",
                "Loyalty and Referral-Based",
                "Gamified",
                "Conditional",
                "Location-Based",
                "Event or Seasonal",
                "Custom or Niche"
            ];
        foreach (var type in advertiseDealTypes)
        {
            dbContext.Add(new AdvertiseDealTypeEntity { Name = type });
        }

        dbContext.SaveChanges();
    }
}
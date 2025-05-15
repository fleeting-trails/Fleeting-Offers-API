using Microsoft.EntityFrameworkCore;

namespace FleetingOffers.Module.User;

public class UserSeeder()
{
    public static void Seed(DbContext context)
    {
        var users = new List<UserEntity>
        {
            new UserEntity
            {
                Username = "abtahi_tajwar",
                FullName = "Abtahi Tajwar",
                Role = USER_ROLE.SUPER_ADMIN,
                Email = "abtahitajwar@gmail.com"
            },
            new UserEntity
            {
                Username = "samaheer_zameel",
                FullName = "Samaheer Zameel",
                Role = USER_ROLE.ADMIN,
                Email = "samaheerzameel@gmail.com"
            },
            new UserEntity
            {
                Username = "fleeting_trails",
                FullName = "Fleeting Trails",
                Role = USER_ROLE.ORGANIZATION,
                Email = "fleetingtrails@gmail.com"
            }
        };

        context.AddRange(users);
        context.SaveChanges();
    }

    public static List<UserEntity> GetSeedData()
    {
        return new List<UserEntity>
    {
        new UserEntity
        {
            Id = Guid.NewGuid().ToString(), // 👈 must be explicitly set
            Username = "abtahi_tajwar",
            FullName = "Abtahi Tajwar",
            Role = USER_ROLE.SUPER_ADMIN, // 👈 must be integer or string if enum
            Email = "abtahitajwar@gmail.com"
        },
        new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Username = "samaheer_zameel",
            FullName = "Samaheer Zameel",
            Role = USER_ROLE.ADMIN,
            Email = "samaheerzameel@gmail.com"
        },
        new UserEntity
        {
            Id = Guid.NewGuid().ToString(),
            Username = "fleeting_trails",
            FullName = "Fleeting Trails",
            Role = USER_ROLE.ORGANIZATION,
            Email = "fleetingtrails@gmail.com"
        }
    };
    }
}
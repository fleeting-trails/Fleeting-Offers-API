using Microsoft.EntityFrameworkCore;

namespace FleetingOffers.Modules.User;

public class SuperAdminSeeder () {
    public static void Seed(DbContext context) {
        var user = new UserEntity()
        {
            Username = "abtahi_tajwar",
            FullName = "Abtahi Tajwar",
            Role = USER_ROLE.SUPER_ADMIN,
            Email = "abtahitajwar@gmail.com"
        };

        context.Add(user);
        context.SaveChanges();
    }
}
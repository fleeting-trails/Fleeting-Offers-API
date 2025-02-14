namespace FleetingOffers.Modules.User;

public class UserRepository {
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public UserEntity? GetUserByEmail (string email) {
        return _dbContext.Users.FirstOrDefault(user => user.Email == email);
    }
}
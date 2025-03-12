using FleetingOffers.Attributes;
using FleetingOffers.Module.Auth;
using Microsoft.EntityFrameworkCore;

namespace FleetingOffers.Module.User;

[ScopedService]
public class UserRepository {
    private readonly AppDbContext _dbContext;
    public UserRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public UserEntity? GetUserByEmail (string email) {
        return _dbContext.Users
        .FirstOrDefault(user => user.Email == email);
    }
    public UserEntity? GetUserByEmailWithAll (string email) {
        return _dbContext.Users
        .AsQueryable()
        .FirstOrDefault(user => user.Email == email);
    }

    public void UpdateUserPassword(string userId, string password, string salt) {
        var user = _dbContext.Users.FirstOrDefault(user => user.Id == userId) ?? throw new Exception("USER404: No user found associated with this email, contact support to register");
        var passwordEntity = new PasswordEntity()
        {
            HashValue = password,
            Salt = salt,
        };

        user.Password = passwordEntity;
        _dbContext.SaveChanges();
    }
}
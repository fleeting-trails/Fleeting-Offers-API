using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetingOffers.Attributes;
using FleetingOffers.Module.Auth;
using Microsoft.EntityFrameworkCore;

namespace FleetingOffers.Module.User;

[ScopedService]
public class UserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    public UserRepository(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public UserDto? GetUserByEmail(string email)
    {
        return _dbContext
            .Users
            .Where(user => user.Email == email)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
    }
    public UserProjection_All? GetUserByEmailWithAll(string email)
    {
        return _dbContext.Users
        .AsQueryable()
        .Where(user => user.Email == email)
        .ProjectTo<UserProjection_All>(_mapper.ConfigurationProvider)
        .FirstOrDefault();
    }
    public UserProjection_PasswordDto? GetUserByEmailWithPassword(string email)
    {
        return _dbContext.Users
        .Include(u => u.Password)
        .Where(user => user.Email == email)
        .ProjectTo<UserProjection_PasswordDto>(_mapper.ConfigurationProvider)
        .FirstOrDefault();
    }

    public void UpdateUserPassword(string userId, string password, string salt)
    {
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
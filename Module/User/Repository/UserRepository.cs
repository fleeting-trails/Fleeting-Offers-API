using AutoMapper;
using AutoMapper.QueryableExtensions;
using FleetingOffers.Attributes;
using FleetingOffers.Module.Auth;
using FleetingOffers.Http;
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
    
    public UserDto? GetUserById(string userId)
    {
        return _dbContext
            .Users
            .Where(user => user.Id == userId)
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

    // Get Users Paginated
    public async Task<PaginatedResult<UserDto>> GetUsersPaginatedAsync(int page, int pageSize)
    {
        var query = _dbContext.Users
            .AsQueryable()
            .OrderByDescending(u => u.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var totalItems = await query.CountAsync();
        var items = await query.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();

        return new PaginatedResult<UserDto>
        {
            Items = items,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }

    // Get User by ID Async
    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        return await _dbContext.Users
            .Where(u => u.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    // Create User
    public async Task<UserDto> CreateUserAsync(UserDto dto)
    {
        var entity = _mapper.Map<UserEntity>(dto);
        entity.CreatedAt = DateTime.UtcNow;
        
        await _dbContext.Users.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<UserDto>(entity);
    }

    // Update User
    public async Task<UserDto> UpdateUserAsync(UserDto dto)
    {
        var entity = await _dbContext.Users
            .Where(u => u.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("User not found");

        entity.FullName = dto.FullName;
        entity.Username = dto.Username;
        entity.Email = dto.Email;
        entity.Role = dto.Role;
        entity.RestrictedUserSubRoleId = dto.RestrictedUserSubRoleId;

        _dbContext.Users.Update(entity);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<UserDto>(entity);
    }

    // Delete User
    public async Task DeleteUserAsync(string id)
    {
        var entity = await _dbContext.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
            throw new KeyNotFoundException("User not found");

        _dbContext.Users.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    // Generate Unique Username
    public string GenerateUniqueUsername(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty");

        // Generate base username
        var baseUsername = GenerateBaseUsername(fullName);
        
        // Check if base username is available
        if (!IsUsernameExists(baseUsername))
            return baseUsername;

        // If not available, append numbers
        var counter = 1;
        string uniqueUsername;
        do
        {
            uniqueUsername = $"{baseUsername}{counter}";
            counter++;
        } while (IsUsernameExists(uniqueUsername));

        return uniqueUsername;
    }

    // Generate Base Username from Full Name
    private string GenerateBaseUsername(string fullName)
    {
        return fullName
            .ToLowerInvariant()
            .Trim()
            .Replace(" ", "_")
            .Replace("-", "_")
            .Replace(".", "")
            .Replace("'", "")
            .Replace("\"", "");
    }

    // Check if Username Exists
    public bool IsUsernameExists(string username)
    {
        return _dbContext.Users.Any(u => u.Username == username);
    }
}
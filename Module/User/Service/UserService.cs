using AutoMapper;
using FleetingOffers.Attributes;
using FleetingOffers.Http;

namespace FleetingOffers.Module.User;

[ScopedService]
public class UserService
{
    private readonly UserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(
        UserRepository repository,
        IMapper mapper
    )
    {
        _repository = repository;
        _mapper = mapper;
    }

    // Get User Details
    public async Task<UserDto> GetUserAsync(string id)
    {
        var user = await _repository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new Exception("USER_404: No user found with this ID");
        }
        return user;
    }

    // User List Paginated
    public async Task<PaginatedResult<UserDto>> GetUsersPaginatedAsync(int page, int pageSize)
    {
        var result = await _repository.GetUsersPaginatedAsync(page, pageSize);
        if (result == null)
        {
            throw new Exception("FAILED: Failed to fetch list of users");
        }
        return result;
    }

    // Create User
    public async Task CreateUserAsync(CreateUserDto dto)
    {
        var existingUser = _repository.GetUserByEmail(dto.Email);
        if (existingUser != null)
        {
            throw new Exception("EMAIL_EXISTS: User with this email already exists");
        }

        var userDto = _mapper.Map<UserDto>(dto);
        // Generate unique username from fullName
        userDto.Username = _repository.GenerateUniqueUsername(dto.FullName ?? "user");
        
        await _repository.CreateUserAsync(userDto);
    }

    // Update User
    public async Task UpdateUserAsync(UpdateUserDto dto)
    {
        var existingUser = await _repository.GetUserByIdAsync(dto.Id);
        if (existingUser == null)
        {
            throw new Exception("USER_404: No user found with this ID");
        }

        // Check if email already exists for another user (only if email is provided)
        if (!string.IsNullOrEmpty(dto.Email))
        {
            var emailUser = _repository.GetUserByEmail(dto.Email);
            if (emailUser != null && emailUser.Id != dto.Id)
            {
                throw new Exception("EMAIL_EXISTS: User with this email already exists");
            }
        }

        // Map the UpdateUserDto to UserDto, preserving existing values for non-provided fields
        var userDto = _mapper.Map<UserDto>(existingUser);
        _mapper.Map(dto, userDto);
        
        await _repository.UpdateUserAsync(userDto);
    }

    // Delete User
    public async Task DeleteUserAsync(string id)
    {
        var user = await _repository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new Exception("USER_404: No user found with this ID");
        }

        await _repository.DeleteUserAsync(id);
    }
}
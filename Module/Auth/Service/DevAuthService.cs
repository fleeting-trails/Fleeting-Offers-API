using FleetingOffers.Attributes;
using FleetingOffers.Module.User;
using FleetingOffers.Util.Helper;

namespace FleetingOffers.Module.Auth;

[ScopedService]
public class DevAuthService
{
    private readonly UserRepository _userRepository;
    public DevAuthService (UserRepository userRepository) {
        _userRepository = userRepository;
    }
    public void SetPassword(string email, string password, string otp)
    {
        /**
        - First we get the user by email
        - The we get auth otp by user id
        - We check if the otp is expired
        - If not expired
            - If OTP does not match, return error
            - We generate a random salt
            - Then we hash the password with the salt
            - We save the password to the database and the salt
        **/
        var user = _userRepository.GetUserByEmail(email) ?? throw new Exception("USER 404: No user found associated with this email");

        var salt = Helper.GenerateSalt();

        var hashedPassword = Helper.HashPassword(password, salt);

        _userRepository.UpdateUserPassword(user.Id, hashedPassword, Convert.ToBase64String(salt));

    }
}
using FleetingOffers.Attributes;
using FleetingOffers.Module.User;
using FleetingOffers.Provider;
using FleetingOffers.Util.Helper;

namespace FleetingOffers.Module.Auth;

[ScopedService]
public class AuthService
{
    private readonly AppDbContext _dbContext;
    private readonly MailProvider _mailService;
    private readonly AuthRepository _repository;
    private readonly UserRepository _userRepository;
    public AuthService(AppDbContext dbContext, MailProvider mailService, AuthRepository repository, UserRepository userRepository)
    {
        _dbContext = dbContext;
        _mailService = mailService;
        _repository = repository;
        _userRepository = userRepository;
    }

    public AuthOtpDto GetOtp(string email)
    {
        var user = _userRepository.GetUserByEmail(email) ?? throw new Exception("USER404: No user found associated with this email, contact support to register");

        var OtpValue = _mailService.SendOTP(user.Username, user.Email);

        var Otp = _repository.UpsertOtp(OtpValue, user.Id);

        return Otp;
    }

    public void SetPassword(string email, string password, string otp) {
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

        var Otp = _repository.GetAuthOtp(user.Id) ?? throw new Exception("OTP_MISMATCH: Provided OTP is not valid, please provide a valid OTP to set password");

        if (Otp.ExpireAt < DateTime.Now) throw new Exception("OTP_EXPIRED: OTP Expired, please request new OTP to update password");

        if (Otp.OtpValue != otp) throw new Exception("OTP_MISMATCH: Provided OTP is not valid, please provide a valid OTP to set password");

        var salt = Helper.GenerateSalt();

        var hashedPassword = Helper.HashPassword(password, salt);

        _userRepository.UpdateUserPassword(user.Id, hashedPassword, Convert.ToBase64String(salt));
        
    }

    public void Login (string email, string password, string deviceSingature) {
        /**
        - First we get the user by email, if not found throw error
        - Then we get the password entity by user id
        - We hash the provided password with the salt
        - We compare the hashed password with the stored password
        - If the password matches, we generate a new token
        - We save the token to the database
        - We return user profile with the token
        **/

        var user = _userRepository.GetUserByEmailWithPassword(email) ?? throw new Exception("USER404: No user found associated with this email, contact support to register");

        if (user == null) throw new Exception("USER404: No user found associated with this email, contact support to register");

        var passwordEntity = user.Password ?? throw new Exception("PASSWORD404: No password found associated with this user, please set a password to login");

        if (!Helper.VerifyPassword(password, user.Password.HashValue, Convert.FromBase64String(user.Password.Salt))) throw new Exception("WRONG_CREDENTIALS: Wrong credentials, unable to login");
        
    }


}
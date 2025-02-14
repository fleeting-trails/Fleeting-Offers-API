using FleetingOffers.Attributes;
using FleetingOffers.Modules.User;
using FleetingOffers.Service;

namespace FleetingOffers.Modules.Auth;

[ScopedService]
public class AuthService
{
    private readonly AppDbContext _dbContext;
    private readonly MailService _mailService;
    private readonly AuthRepository _repository;
    private readonly UserRepository _userRepository;
    public AuthService(AppDbContext dbContext, MailService mailService, AuthRepository repository, UserRepository userRepository)
    {
        _dbContext = dbContext;
        _mailService = mailService;
        _repository = repository;
        _userRepository = userRepository;
    }

    public AuthOtpEntity GetOtp(string email)
    {
        var user = _userRepository.GetUserByEmail(email) ?? throw new Exception("USER404: No user found associated with this email, contact support to register");

        var OtpValue = _mailService.SendOTP(user.Username, user.Email);

        var Otp = _repository.UpsertOtp(OtpValue, user.Email);
        user.Otp = Otp;

        _dbContext.SaveChanges();

        return Otp;
    }
}
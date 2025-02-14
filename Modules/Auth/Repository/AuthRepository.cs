using FleetingOffers.Attributes;

namespace FleetingOffers.Modules.Auth;

[ScopedService]
public class AuthRepository
{
    private readonly AppDbContext _dbContext;
    public AuthRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public AuthOtpEntity UpsertOtp(string otpValue, string userId)
    {
        var Otp = _dbContext.AuthOtps.FirstOrDefault(otp => otp.UserId == userId);
        if (Otp == null)
        {
            Otp = new()
            {
                UserId = userId,
                OtpValue = otpValue,
            };
        } else {
          Otp.OtpValue = otpValue;
        }

        _dbContext.SaveChanges();

        return Otp;
    }

}
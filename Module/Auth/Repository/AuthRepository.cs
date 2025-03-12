using FleetingOffers;
using FleetingOffers.Attributes;
using FleetingOffers.Provider;

namespace FleetingOffers.Module.Auth;

[ScopedService]
public class AuthRepository
{
    private readonly AppDbContext _dbContext;
    private readonly CacheProvider _cacheProvider;
    public AuthRepository(AppDbContext dbContext, CacheProvider cacheProvider)
    {
        _dbContext = dbContext;
        _cacheProvider = cacheProvider;
    }

    public AuthOtpEntity UpsertOtp(string otpValue, string userId)
    {
        /**
            - First we save the OTP to the cache database
            - Then we try to get the OTP from the database
            - If not found, we create a new OTP
            - If found, we update the OTP
        **/
        AuthOtpEntity newOtp = new()
            {
                UserId = userId,
                OtpValue = otpValue,
            };

        _cacheProvider.Create($"{userId}_OTP", newOtp);

        var Otp = _dbContext.AuthOtps.FirstOrDefault(otp => otp.UserId == userId);
        if (Otp == null)
        {
            Otp = newOtp;
;        }
        else
        {
            Otp.OtpValue = otpValue;
        }

        _dbContext.SaveChanges();

        return Otp;
    }
    public AuthOtpEntity? GetAuthOtp(string userId)
    {
        /**
        - First we try to get the OTP from the cache database
        - If not found, we get the OTP from the database
        **/
        var Otp = _cacheProvider.Get<AuthOtpEntity>($"{userId}_OTP");
        if (Otp != null) return Otp;
        Otp = _dbContext.AuthOtps.FirstOrDefault(otp => otp.UserId == userId);
        return Otp;
    }

}
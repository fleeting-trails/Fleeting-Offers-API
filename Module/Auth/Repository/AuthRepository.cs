using AutoMapper;
using FleetingOffers;
using FleetingOffers.Attributes;
using FleetingOffers.Provider;
using FleetingOffers.Settings;
using Org.BouncyCastle.Bcpg;

namespace FleetingOffers.Module.Auth;

[ScopedService]
public class AuthRepository
{
    private readonly AppDbContext _dbContext;
    private readonly CacheProvider _cacheProvider;
    private readonly IMapper _mapper;
    public AuthRepository(AppDbContext dbContext, CacheProvider cacheProvider, IMapper mapper)
    {
        _dbContext = dbContext;
        _cacheProvider = cacheProvider;
        _mapper = mapper;
    }

    public AuthOtpDto UpsertOtp(string otpValue, string userId)
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
            ;
        }
        else
        {
            Otp.OtpValue = otpValue;
        }

        _dbContext.SaveChanges();

        return _mapper.Map<AuthOtpDto>(Otp);
    }
    public AuthOtpDto? GetAuthOtp(string userId)
    {
        /**
        - First we try to get the OTP from the cache database
        - If not found, we get the OTP from the database
        **/
        var Otp = _cacheProvider.Get<AuthOtpEntity>($"{userId}_OTP");
        if (Otp != null) return _mapper.Map<AuthOtpDto>(Otp);
        Otp = _dbContext.AuthOtps.FirstOrDefault(otp => otp.UserId == userId);
        return _mapper.Map<AuthOtpDto>(Otp);
    }

    public void UpsertAuthToken(string userId, string? deviceSignature, string token)
    {
        /**
        - First we generate a new token
        - Then we save the token to the cache database, the cache databasek key should be in the format {userId}_{deviceSignature}_TOKEN
        - Then we try to get the token from the database
        - If not found, we create a new token
        - If found, we update the token
        **/
        AuthTokenEntity newToken = new()
        {
            UserId = userId,
            DeviceSignature = deviceSignature,
            Token = token,
        };

        var created = _cacheProvider.Create($"{userId}_{deviceSignature ?? ""}_TOKEN", newToken);

        if (!created)
        {
            var Token = _dbContext.AuthTokens.FirstOrDefault(token => token.UserId == userId && token.DeviceSignature == deviceSignature);
            if (Token == null)
            {
                _dbContext.Add(newToken);
            }
            else
            {
                Token.Token = token;
            }

            _dbContext.SaveChanges();
        }
    }

    public AuthTokenDto? GetAuthToken(string userId, string? deviceSignature)
    {
        /**
        - First we try to get the token from the cache database, the cache databasek key should be in the format {userId}_{deviceSignature}_TOKEN
        - If not found, we get the token from the database
        **/
        var Token = _cacheProvider.Get<AuthTokenEntity>($"{userId}_{deviceSignature ?? ""}_TOKEN");
        if (Token != null) return _mapper.Map<AuthTokenDto>(Token);
        Token = _dbContext.AuthTokens.FirstOrDefault(token => token.UserId == userId && token.DeviceSignature == deviceSignature);
        return _mapper.Map<AuthTokenDto>(Token);
    }
    public bool DeleteAuthToken(string userId, string tokenString, string deviceSignature)
    {
        /**
        - First we try to get the token from the cache database, the cache databasek key should be in the format {userId}_{deviceSignature}_TOKEN
        - If found we delete the token from the cache database
        - If not found, Then we try to get the token from the database. 
        - Then we delete the token from the database
        **/

        var Token = _cacheProvider.Get<AuthTokenEntity>($"{userId}_{deviceSignature ?? ""}_TOKEN");
        if (Token != null)
        {
            var deleted = _cacheProvider.Delete($"{userId}_{deviceSignature ?? ""}_TOKEN");
            return deleted;
        }
        
        Token = _dbContext.AuthTokens.FirstOrDefault(token => token.UserId == userId && token.Token == tokenString);
        if (Token != null) {
            _dbContext.AuthTokens.Remove(Token);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }

    public object? GetRolePermissions(string? role)
    {
        try
        {
            if (string.IsNullOrEmpty(role))
                return null;

            var permissionsPath = Path.Combine(Directory.GetCurrentDirectory(), "permissions.json");
            if (!System.IO.File.Exists(permissionsPath))
                return null;

            var permissionsJson = System.IO.File.ReadAllText(permissionsPath);
            var permissionsData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(permissionsJson);
            
            if (permissionsData != null && permissionsData.ContainsKey(role))
            {
                return permissionsData[role];
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

}
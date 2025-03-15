using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FleetingOffers.Settings;
using System.Diagnostics;
using FleetingOffers.Provider.JWT;
using FleetingOffers.Module.Auth;
using FleetingOffers.Attributes;

[ScopedService]
public class JWTProvider
{
    public AuthTokenDto GenerateToken(JWTPayloadDto data)
    {
        var issuedAt = DateTimeOffset.UtcNow;
        var expiration = DateTime.UtcNow.AddHours(JWTSettings.ExpireInHour); 
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, data.UserId),
            new Claim(JwtRegisteredClaimNames.UniqueName, data.Email),
            new Claim(ClaimTypes.Role, data.Role ?? ""),
            new Claim(ClaimTypes.WindowsDeviceClaim, data.Device ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
            new Claim(JwtRegisteredClaimNames.Iat, issuedAt.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64) // Issued at time
        };
        if (JWTSettings.Secret == null)
        {
            throw new Exception("Please provide valid jwt secret");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken(
            issuer: JWTSettings.Issuer,
            audience: JWTSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return new AuthTokenDto()
        {
            UserId = data.UserId,
            Token = tokenString,
            Expiration = expiration,
            DeviceSignature = data.Device,
            CreatedAt = DateTime.UtcNow
        };
    }

    public JWTPayloadDto DecodeAndValidateToken(string token)
    {
        if (JWTSettings.Secret == null) throw new Exception("Please provide valid jwt secret");
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(JWTSettings.Secret);

        // Define token validation parameters
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = JWTSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = JWTSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // Optional: Avoid default 5-minute clock skew
        };

        try
        {
            // Validate the token and return the claims
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            // Additional check for the algorithm
            if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg != SecurityAlgorithms.HmacSha256)
            {
                throw new SecurityTokenException("Invalid token algorithm");
            }


            return new JWTPayloadDto()
            {
                UserId = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value,
                Email = principal.FindFirst(ClaimTypes.Name)!.Value,
                Role = principal.FindFirst(ClaimTypes.Role)?.Value,
                Device = principal.FindFirst(ClaimTypes.WindowsDeviceClaim)?.Value
            };
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException($"Token validation failed: {ex.Message}");
        }
    }

    public JWTPayloadDto DecodeTokenWithoutValidation(string token)
    {
        Console.WriteLine($"Decoding token: {token}");
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);

        var payload = new JWTPayloadDto
        {
            UserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? throw new Exception("UserId not found"),
            Email = jwtToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value ?? throw new Exception("Email not found"),
            Role = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role")?.Value
        };

        return payload;
    }
}
using System.Security.Cryptography;
using FleetingOffers.Attributes;

namespace FleetingOffers.Util.Helper;

[SingletonService]
public class Helper()
{
    public static byte[] GenerateSalt(int saltSize = 16)
    {
        byte[] salt = new byte[saltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt); // Fill with cryptographically secure random bytes
        }
        return salt;
    }

    // Hash password with salt using PBKDF2
    public static string HashPassword(string password, byte[] salt, int iterations = 100000, int hashSize = 32)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(hashSize);
            return Convert.ToBase64String(hash);
        }
    }

    public static bool VerifyPassword(string password, string hash, byte[]? salt)
    {
        return hash == HashPassword(password, salt);
    }

    public static bool IsAllowedMimeType(string mimeType, string[] allowedTypes)
    {
        foreach (var allowed in allowedTypes)
        {
            if (allowed == "*/*") return true;

            var parts = allowed.Split('/');
            var mimeParts = mimeType.Split('/');

            if (parts.Length != 2 || mimeParts.Length != 2)
                continue;

            bool typeMatches = parts[0] == "*" || parts[0] == mimeParts[0];
            bool subTypeMatches = parts[1] == "*" || parts[1] == mimeParts[1];

            if (typeMatches && subTypeMatches)
                return true;
        }

        return false;
    }
}
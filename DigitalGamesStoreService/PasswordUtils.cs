using System.Security.Cryptography;
using System.Text;

namespace DigitalGamesStoreService;

public static class PasswordUtils
{
    private const string SecretKey = "0ae1d8667fd11c657ff767192e176552e240cf86";
    private const int SaltBufferSize = 16;
    private const char PasswordPartsDelimiter = '?';

    public static (string salt, string hashedPassword) CreatePasswordSecuredComponents(string actualPassword)
    {
        var salt = Convert.ToBase64String(RandomNumberGenerator.GetBytes(SaltBufferSize));
        var hashedPassword = GetPasswordHash(actualPassword, salt);

        return (salt, hashedPassword);
    }

    public static bool VerifyPassword(string actualPassword, string salt, string hashedPassword)
    {
        return hashedPassword == GetPasswordHash(actualPassword, salt);
    }

    public static string GetPasswordHash(string actualPassword, string salt)
    {
        var password = actualPassword + PasswordPartsDelimiter + salt + PasswordPartsDelimiter + SecretKey;
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var hashProvider = SHA512.Create();
        var hash = hashProvider.ComputeHash(passwordBytes);

        return Convert.ToBase64String(hash);
    }
}
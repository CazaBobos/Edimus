using System.Security.Cryptography;
using System.Text;

namespace Shared.Core.Services;
public class HashService
{
    public static string CreateHash(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);

    public static bool Verify(string password, string hash) =>
        BCrypt.Net.BCrypt.Verify(password, hash);

    public static string CreateDeterministicHash(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var sb = new StringBuilder();
        foreach (var b in bytes) sb.AppendFormat("{0:x2}", b);
        return sb.ToString();
    }
}

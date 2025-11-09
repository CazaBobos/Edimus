using System.Security.Cryptography;
using System.Text;

namespace Shared.Core.Services;
public class HashService
{
    //This method allows not to Encrypt, but rather Hash the data.
    //This means that, unlike Encrytion, data can be converted only
    //one way but not the other, increasing password storage security
    public static string CreateHash(string str)
    {
        var sha256 = SHA256.Create();
        var encoding = new ASCIIEncoding();
        StringBuilder sb = new StringBuilder();
        byte[] stream = sha256.ComputeHash(encoding.GetBytes(str));

        foreach (byte b in stream) sb.AppendFormat("{0:x2}", b);

        return sb.ToString();
    }
}

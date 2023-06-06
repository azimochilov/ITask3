using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace Task3;

public class KeyHMACGenerator
{
    public string GenerateKey(string keyText)
    {
        byte[] keyBytes = RandomNumberGenerator.GetBytes(32);

        return BitConverter.ToString(keyBytes).Replace("-", "");
    }
    public string GenerateHMAC(string message, string key)
    {
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        var hmac = new HMACSHA256(keyBytes);
        byte[] hmacBytes = hmac.ComputeHash(messageBytes);
        var hmacString = BitConverter.ToString(hmacBytes).Replace("-", "");

        return hmacString;
    }
}

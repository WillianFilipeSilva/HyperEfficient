using System.Security.Cryptography;
using System.Text;

namespace HyperEfficient.Infrastructure.Criptography
{
    public class Criptography
    {
        public static string GeneratePbkdf2Hash(string plainText)
        {
            Rfc2898DeriveBytes rfc = new(plainText, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 10000);
            var key = rfc.GetBytes(32);

            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }

        public static bool VerifyPbkdf2Hash(string plainText, string hashToCompare)
        {
            var hash = GeneratePbkdf2Hash(plainText);

            return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(hash),
                Encoding.UTF8.GetBytes(hashToCompare));
        }
    }
}
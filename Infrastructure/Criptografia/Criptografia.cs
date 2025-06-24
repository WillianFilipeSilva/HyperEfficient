using System.Security.Cryptography;

namespace HyperEfficient.Infrastructure.Criptografia
{
    public class Criptografia
    {
        public static string GeneratePBKDF2Hash(string textoClaro)
        {
            var salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var rfc = new Rfc2898DeriveBytes(textoClaro, salt, 10000);
            byte[] key = rfc.GetBytes(32);
            return BitConverter.ToString(key).Replace("-", "").ToLower();
        }
    }
}

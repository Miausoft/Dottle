using System;
using System.Security.Cryptography;
using System.Text;

namespace Dottle.Helpers
{
    public class PasswordManager
    {
        private const int SALT_SIZE = 32;
        public static string HashPassword(string password, string salt)
        {
            SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.ASCII.GetBytes(password + salt));
            
            var hashed = new StringBuilder();
            foreach (var b in bytes)
            {
                hashed.Append(b.ToString("x2"));
            }

            return hashed.ToString();
        }
        
        public static bool VerifyHashedPassword(string input, string hash, string salt)
        {
            var hashOfInput = HashPassword(input, salt);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }

        public static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[SALT_SIZE];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}

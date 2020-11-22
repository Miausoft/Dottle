using System;
using System.Security.Cryptography;
using System.Text;

namespace Dottle.Helpers
{
    public class PasswordManager
    {
        public static string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            
            var hashed = new StringBuilder();
            foreach (var b in bytes)
            {
                hashed.Append(b.ToString("x2"));
            }

            return hashed.ToString();
        }
        
        public static bool VerifyHashedPassword(string input, string hash)
        {
            var hashOfInput = HashPassword(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}

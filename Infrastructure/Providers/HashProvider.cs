using Infrastructure.Interfaces;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Providers
{
    public class HashProvider : IHashProvider
    {
        public string GenerateHash(string password)
        {
            UnicodeEncoding encoding = new();
            byte[] hashByte;

            using(HashAlgorithm hash = SHA1.Create())
                hashByte = hash.ComputeHash(encoding.GetBytes(password));

            StringBuilder hashValue = new StringBuilder(hashByte.Length * 2);

            foreach (byte b in hashByte)
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);

            return hashValue.ToString();
        }
    }
}

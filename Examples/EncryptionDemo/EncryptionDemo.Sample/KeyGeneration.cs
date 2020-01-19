using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionDemo.Sample
{
    public class KeyGeneration
    {
        // see TODO
        public static readonly int SaltSizeInBits = SHA256.Create().HashSize;
        public static readonly HashAlgorithmName HashAlgorithmSha256 = HashAlgorithmName.SHA256;

        // see TODO
        public const int Iterations = 100_000;

        public static byte[] CreateRandom(int sizeInBits)
        {
            Span<byte> key =new byte[sizeInBits / 8];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(key);
            }

            return key.ToArray();
        }

        public static (byte[] key, byte[] salt) CreateFromPassword(string password, int sizeInBits, byte[] salt=null)
        {
            if(salt==null)
                salt = CreateRandom(SaltSizeInBits);
            
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, Iterations, HashAlgorithmSha256);

            var key = rfc2898DeriveBytes.GetBytes(sizeInBits/8);
            return (key, salt);
        }
    }
}

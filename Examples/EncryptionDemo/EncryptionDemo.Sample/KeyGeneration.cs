// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionDemo.Sample
{
    /// <summary>
    /// CreateRandom - create a random key
    /// CreateFromPassword - creates a key from a password
    /// All random data is generated with a cryptographic random number generators which create cryptographically strong random values.
    /// </summary>
    public class KeyGeneration
    {
        /// <summary>
        /// The salt will have the same size as the HashSize of the used HashAlgorithm SHA256. The salt shall be at least 128 bits after the NIST Special Publication 800-132.
        /// </summary>
        private static readonly int SaltSizeInBits = SHA256.Create().HashSize;
        /// <summary>
        /// SHA256 is one of the recommended hash function, see BSI TR-02102-1, 4. Hash functions, Table 4.1.: Recommended hash functions
        /// </summary>
        private static readonly HashAlgorithmName HashAlgorithmSha256 = HashAlgorithmName.SHA256;

        /// <summary>
        /// This implementation uses a iteration count of 100.000, which seems to be a number which is wildly used in strong systems. A minimum of 1.000 is recommended after the NIST Special Publication 800-132 and the RFC 2898.
        /// </summary>
        private const int Iterations = 100_000;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeInBits"></param>
        /// <returns></returns>
        public static byte[] CreateRandom(int sizeInBits)
        {
            Span<byte> key = new byte[sizeInBits / 8];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(key);
            }

            return key.ToArray();
        }

        /// <summary>
        /// We use Rfc2898 PBKDF2 (Password-Based Key Derivation Function 2) for derive a key from a password
        /// This implementation creates a new salt value for each key derivation, see BSI TR-02102-1, 7.1. Symmetric schemes, Key update.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="sizeInBits"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static (byte[] key, byte[] salt) CreateFromPassword(string password, int sizeInBits, byte[] salt = null)
        {
            if (salt == null)
                salt = CreateRandom(SaltSizeInBits);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), salt, Iterations, HashAlgorithmSha256);

            var key = rfc2898DeriveBytes.GetBytes(sizeInBits / 8);
            return (key, salt);
        }
    }
}
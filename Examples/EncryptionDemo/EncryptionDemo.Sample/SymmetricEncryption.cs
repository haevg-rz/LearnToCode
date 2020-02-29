// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EncryptionDemo.Sample
{
    /// <summary>
    /// Encrypt and decrypt with the Advanced Encryption Standard (AES) and the Galois/Counter Mode (GCM) mode of operation.
    /// This implementation allows AES-128, 192, or 256, each is recommend, see BSI TR-02102-1, 2.1. Block ciphers.
    /// The mode Galois/Counter Mode (GCM) is one of the recommended modes of operation for block ciphers. See BSI TR-02102-1, 2.1.1. Modes of operation.
    ///
    /// This implementation uses a tag size of 128 Bits. A tag size greater than 96 must be used. See BSI TR-02102-1, 2.1.2. Conditions of use, For GCM.
    /// This implementation uses a nonces size of 96 Bits, the only nonce sizes supported by this AesGcm instance. GCM requires the generation of nonces for the integrated authentication mechanism.
    /// A nonce size greater than 96 must be used. See BSI TR-02102-1, 2.1.2. Conditions of use, For GCM.
    ///
    /// All random data is generated with a cryptographic random number generators which create cryptographically strong random values.
    /// </summary>
    public class SymmetricEncryption
    {
        public class EncryptedDataContainer
        {
            public byte[] CipherText { get; set; }
            public byte[] Nonce { get; set; }
            public byte[] Tag { get; set; }
            public byte[] AssociatedData { get; set; }
        }

        /// <summary>
        /// Encrypts the plain data with AES in Mode GCM
        /// </summary>
        /// <param name="key">Must have a length of 128, 192, or 256</param>
        /// <param name="plain"></param>
        /// <param name="associatedData">associated data is authenticated and non-confidential, because it isn't encrypted!</param>
        /// <returns>A encryptedDataContainer which contains the cipher text, the nonce, the tag and the associatedData</returns>
        public static EncryptedDataContainer Encrypt(byte[] key, byte[] plain, byte[] associatedData)
        {
            var nonce = KeyGeneration.CreateRandom(System.Security.Cryptography.AesGcm.NonceByteSizes.MaxSize * 8);

            // Bug in the corefx documentation, the MaxSize is in this case is in byte not in bit, see https://github.com/dotnet/runtime/issues/1910
            var tag = new byte[System.Security.Cryptography.AesGcm.TagByteSizes.MaxSize];
            var cipherText = new byte[plain.Length];

            var aes = new System.Security.Cryptography.AesGcm(key);
            aes.Encrypt(nonce, plain, cipherText, tag, associatedData);

            return new EncryptedDataContainer
            {
                CipherText = cipherText,
                Nonce = nonce,
                Tag = tag,
                AssociatedData = associatedData
            };
        }

        /// <summary>
        /// Decrypt the cipher data with AES in Mode GCM
        /// </summary>
        /// <param name="key">Must have a length of 128, 192, or 256</param>
        /// <param name="encryptedDataContainer">Contains the cipher text, the nonce, the tag and the associatedData</param>
        /// <returns>Plain text</returns>
        public static byte[] Decrypt(byte[] key, EncryptedDataContainer encryptedDataContainer)
        {
            var plainText = new byte[encryptedDataContainer.CipherText.Length];

            var aes = new System.Security.Cryptography.AesGcm(key);
            aes.Decrypt(encryptedDataContainer.Nonce, encryptedDataContainer.CipherText, encryptedDataContainer.Tag, plainText, encryptedDataContainer.AssociatedData);

            return plainText;
        }
    }
}
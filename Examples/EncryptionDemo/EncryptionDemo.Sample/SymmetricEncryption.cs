using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EncryptionDemo.Sample
{
    public class SymmetricEncryption
    {
        public class EncryptedDataContainer
        {
            public byte[] CipherText { get; set; }
            public byte[] Nonce { get; set; }
            public byte[] Tag { get; set; }
            public byte[] AssociatedData { get; set; }
        }

        public static EncryptedDataContainer Encrypt(byte[] key, byte[] plain, byte[] associatedData)
        {
            var nonce = KeyGeneration.CreateRandom(System.Security.Cryptography.AesGcm.NonceByteSizes.MaxSize*8);

            var tag = new byte[System.Security.Cryptography.AesGcm.TagByteSizes.MaxSize];
            var cipherText= new byte[plain.Length];

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
        
        public static byte[] Decrypt(byte[] key, EncryptedDataContainer encryptedDataContainer)
        {
            var plainText= new byte[encryptedDataContainer.CipherText.Length];

            var aes = new System.Security.Cryptography.AesGcm(key);
            aes.Decrypt(encryptedDataContainer.Nonce, encryptedDataContainer.CipherText, encryptedDataContainer.Tag, plainText,encryptedDataContainer.AssociatedData);

            return plainText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EncryptionDemo.Sample
{
    public class Aes
    {

        public class Container
        {
            public byte[] CipherText { get; set; }
            public byte[] Nonce { get; set; }
            public byte[] Tag { get; set; }
        }

        public static Container Encrypt(byte[] key, byte[] plain)
        {
            var nonce = Key.CreateRandom(System.Security.Cryptography.AesGcm.NonceByteSizes.MaxSize*8);

            var tag = new byte[System.Security.Cryptography.AesGcm.TagByteSizes.MaxSize];
            var cipherText= new byte[plain.Length];

            var aes = new System.Security.Cryptography.AesGcm(key);
            aes.Encrypt(nonce, plain, cipherText, tag);

            return new Container
            {
                CipherText = cipherText,
                Nonce = nonce,
                Tag = tag,
            };
        }
    }
}

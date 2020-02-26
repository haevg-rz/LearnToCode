// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System.Security.Cryptography;

namespace EncryptionDemo.Sample
{
    public class AsymmetricCryptographyRsa
    {
        // see BSI TR-02102-1 
        private const int RsaKeySize = 1024 * 4;

        // see BSI TR-02102-1 
        private static readonly RSAEncryptionPadding RsaEncryptionPadding = RSAEncryptionPadding.OaepSHA512;

        // see BSI TR-02102-1 
        private static readonly RSASignaturePadding RsaSignaturePadding = RSASignaturePadding.Pss;

        // see BSI TR-02102-1 
        private static readonly HashAlgorithmName SignaturehashAlgorithmName = HashAlgorithmName.SHA512;

        public static string CreateRsaKeyPair()
        {
            using var rsa = RSA.Create();
            rsa.KeySize = RsaKeySize;

            return rsa.ToXmlString(true);
        }

        public static string ExtractPublicKey(string rsaXml)
        {
            using var rsa = RSA.Create();
            rsa.FromXmlString(rsaXml);

            return rsa.ToXmlString(false);
        }

        public static byte[] Encrypt(string rsaXml, byte[] data)
        {
            using var rsa = RSA.Create();
            rsa.FromXmlString(rsaXml);
            var encrypted = rsa.Encrypt(data, RsaEncryptionPadding);
            return encrypted;
        }

        public static byte[] Decrypt(string rsaXml, byte[] data)
        {
            using var rsa = RSA.Create();
            rsa.FromXmlString(rsaXml);

            // Todo chek prvivate key
            var decrypted = rsa.Decrypt(data, RsaEncryptionPadding);
            return decrypted;
        }

        public static byte[] Sign(string rsaXml, byte[] data)
        {
            using var rsa = RSA.Create();
            rsa.FromXmlString(rsaXml);
            // Todo check prvivate key
            var signature = rsa.SignData(data, SignaturehashAlgorithmName, RsaSignaturePadding);
            return signature;
        }

        public static bool Verify(string rsaXml, byte[] data, byte[] signature)
        {
            using var rsa = RSA.Create();
            rsa.FromXmlString(rsaXml);
            // Todo check private key
            var isVerifedData = rsa.VerifyData(data, signature, SignaturehashAlgorithmName, RsaSignaturePadding);
            return isVerifedData;
        }
    }
}
// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System.Security.Cryptography;

namespace EncryptionDemo.Sample
{
    public class AsymmetricCryptographyRsa
    {
        // see BSI TR-02102-1 
#if RELEASE
        private const int RsaKeySize = 1024 * 4;
#else
        private const int RsaKeySize = 1024 * 2;
#endif

        /// <summary>
        /// Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA512 hash algorithm.
        /// </summary>
        /// <remarks>
        /// see BSI TR-03116-1, 3.2 RSA, Allowed until 2024+
        /// </remarks>
        private static readonly RSAEncryptionPadding RsaEncryptionPadding = RSAEncryptionPadding.OaepSHA512;


        /// <summary>
        ///  The padding mode for RSA signature Pss - Probabilistic Signature Scheme (RSASSA-PSS) of the PKCS #1: RSA Encryption Standard.
        ///  </summary>
        ///  <remarks>
        ///  see BSI TR-03116-1, 3.2 RSA, Allowed until 2024+ and
        ///  recommended after BSI TR-02102-1, 1.4. Handling of legacy algorithm, 4. RSA with PKCS1v1.5 padding and 
        ///  BSI TR-02102-1, 5.4.1. RSA, Table 5.4.: Recommended padding schemes for the RSA signature algorithm
        ///  </remarks>
        private static readonly RSASignaturePadding RsaSignaturePadding = RSASignaturePadding.Pss;

        // see BSI TR-02102-1 
        private static readonly HashAlgorithmName SignatureHashAlgorithmName = HashAlgorithmName.SHA512;

        public static string CreateRsaKeyPair()
        {
            using var rsa = RSA.Create(RsaKeySize);

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
            var signature = rsa.SignData(data, SignatureHashAlgorithmName, RsaSignaturePadding);
            return signature;
        }

        public static bool Verify(string rsaXml, byte[] data, byte[] signature)
        {
            using var rsa = RSA.Create();
            rsa.FromXmlString(rsaXml);
            // Todo check private key
            var isVerifedData = rsa.VerifyData(data, signature, SignatureHashAlgorithmName, RsaSignaturePadding);
            return isVerifedData;
        }
    }
}
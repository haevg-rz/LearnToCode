using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace EncryptionDemo.Sample
{
    public class AsymmetricCryptographyEcc
    {
        // See
        private static readonly ECCurve NamedCurveBrainpoolP512R1 = ECCurve.NamedCurves.brainpoolP512r1;

        // See
        private static readonly HashAlgorithmName DeriveHashAlgorithmName = HashAlgorithmName.SHA512;

        // See
        private static readonly HashAlgorithmName SignHashAlgorithmName = HashAlgorithmName.SHA512;

        /// <summary>
        /// Generates and exports the current key in the PKCS#8 PrivateKeyInfo format.
        /// </summary>
        /// <returns></returns>
        public static (byte[] privateKeyInfo, byte[] subjectPublicKeyInfo) CreateKeyPair()
        {
            var ecDiffieHellman = ECDiffieHellman.Create(NamedCurveBrainpoolP512R1);
            return (ecDiffieHellman.ExportPkcs8PrivateKey(), ecDiffieHellman.ExportSubjectPublicKeyInfo());
        }

        /// <summary>
        /// Exports the public-key portion of the current key in the X.509 SubjectPublicKeyInfo format.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] GetPublicKey(byte[] key)
        {
            var ecDiffieHellman = ECDiffieHellman.Create();
            ecDiffieHellman.ImportPkcs8PrivateKey(key, out _);

            return ecDiffieHellman.ExportSubjectPublicKeyInfo();
        }

        public static byte[] DeriveSecret(byte[] privateKeyInfo, byte[] subjectPublicKeyInfo)
        {
            var curvePublic = ECDiffieHellman.Create();
            curvePublic.ImportSubjectPublicKeyInfo(subjectPublicKeyInfo, out _);

            var curvePrivate = ECDiffieHellman.Create();
            curvePrivate.ImportPkcs8PrivateKey(privateKeyInfo, out _);

            return curvePrivate.DeriveKeyFromHash(curvePublic.PublicKey, DeriveHashAlgorithmName);
        }

        public static byte[] SignData(byte[] privateKeyInfo, byte[] data)
        {
            var ecDsa = ECDsa.Create();
            ecDsa.ImportPkcs8PrivateKey(privateKeyInfo, out _);

            return ecDsa.SignData(data, SignHashAlgorithmName);
        }

        public static bool VerifyData(byte[] subjectPublicKeyInfo, byte[] data, byte[] signature)
        {
            var ecDsa = ECDsa.Create();
            ecDsa.ImportSubjectPublicKeyInfo(subjectPublicKeyInfo, out _);

            return ecDsa.VerifyData(data, signature, SignHashAlgorithmName);
        }
    }
}
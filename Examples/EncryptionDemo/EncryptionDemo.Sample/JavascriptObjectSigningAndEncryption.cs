// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Jose;
using Security.Cryptography;

namespace EncryptionDemo.Sample
{
    public class JavascriptObjectSigningAndEncryption
    {
        /// <summary>
        /// AES Key Wrap Algorithm using 256 bit keys, RFC 3394
        /// AES GCM Key Wrap Algorithm using 256 bit keys
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(byte[] secret, string plainText)
        {
            // Claims see https://tools.ietf.org/html/rfc7519#section-4.1
            var nonEncryptedHeaders = new Dictionary<string, object>
            {
                {"sub", "mr.x@contoso.com"},
                {"exp", 1300819380}
            };

            var token = JWT.Encode(plainText, secret, JweAlgorithm.A256KW, JweEncryption.A256GCM, extraHeaders: nonEncryptedHeaders);
            return token;
        }

        /// <summary>
        /// AES Key Wrap Algorithm using 256 bit keys, RFC 3394
        /// AES GCM Key Wrap Algorithm using 256 bit keys
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Decrypt(byte[] secret, string token)
        {
            var r = JWT.Decode(token, secret, JweAlgorithm.A256KW, JweEncryption.A256GCM);
            return r;
        }

        /// <summary>
        /// Password Based Jwe using PBES2 schemes with HMAC-SHA and AES Key Wrap using 256 bit key
        /// AES GCM Key Wrap Algorithm using 256 bit keys
        /// </summary>
        /// <param name="password"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string password, string plainText)
        {
            // Claims see https://tools.ietf.org/html/rfc7519#section-4.1
            var nonEncryptedHeaders = new Dictionary<string, object>
            {
                {"sub", "mr.x@contoso.com"},
                {"exp", 1300819380}
            };

            // In this implemenation the PBKDF2 iteration count is hardcoded to 8.192 which could be low regarding the BSI Standard.
            var token = JWT.Encode(plainText, password, JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A256GCM, extraHeaders: nonEncryptedHeaders);
            return token;
        }

        /// <summary>
        /// Password Based Jwe using PBES2 schemes with HMAC-SHA and AES Key Wrap using 256 bit key
        /// AES GCM Key Wrap Algorithm using 256 bit keys
        /// </summary>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Decrypt(string password, string token)
        {
            var r = JWT.Decode(token, password, JweAlgorithm.PBES2_HS512_A256KW, JweEncryption.A256GCM);
            return r;
        }

        /// <summary>
        /// Elliptic Curve Diffie Hellman key agreement with AES Key Wrap using 256 bit key
        /// AES GCM Key Wrap Algorithm using 256 bit keys
        /// </summary>
        /// <param name="password"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(ECParameters exportParameters, string plainText)
        {
            // Claims see https://tools.ietf.org/html/rfc7519#section-4.1
            var nonEncryptedHeaders = new Dictionary<string, object>
            {
                {"sub", "mr.x@contoso.com"},
                {"exp", 1300819380}
            };

            var publicKey = EccKey.New(exportParameters.Q.X, exportParameters.Q.Y, exportParameters.D, CngKeyUsages.KeyAgreement);

            // Not Implemented for NETSTANDARD 1.4
            // https://github.com/dvsekhvalnov/jose-jwt/blob/e54de3bb706edf294053b4b86f0db47333d433ef/jose-jwt/crypto/ConcatKDF.cs#L43
            var token = JWT.Encode(plainText, publicKey, JweAlgorithm.ECDH_ES, JweEncryption.A256GCM, extraHeaders: nonEncryptedHeaders);
            return token;
        }

        /// <summary>
        /// RSAES with SHA-256 using Optimal Asymmetric Jwe Padding, RFC 3447
        /// AES GCM Key Wrap Algorithm using 256 bit keys
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(RSA rsa, string plainText)
        {
            // Claims see https://tools.ietf.org/html/rfc7519#section-4.1
            var nonEncryptedHeaders = new Dictionary<string, object>
            {
                {"sub", "mr.x@contoso.com"},
                {"exp", 1300819380}
            };

            // In this implemenation the PBKDF2 iteration count is hardcoded to 8.192 which could be low regarding the BSI Standard.
            var token = JWT.Encode(plainText, rsa, JweAlgorithm.RSA_OAEP_256, JweEncryption.A256GCM, extraHeaders: nonEncryptedHeaders);
            return token;
        }

        /// <summary>
        /// RSAES with SHA-256 using Optimal Asymmetric Jwe Padding, RFC 3447
        /// AES GCM Key Wrap Algorithm using 256 bit keys
        /// </summary>
        /// <param name="rsa"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Decrypt(RSA rsa, string token)
        {
            var plain = JWT.Decode(token, rsa, JweAlgorithm.RSA_OAEP_256, JweEncryption.A256GCM);
            return plain;
        }
    }
}
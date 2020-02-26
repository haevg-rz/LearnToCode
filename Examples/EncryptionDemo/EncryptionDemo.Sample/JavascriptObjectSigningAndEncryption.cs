using System;
using System.Collections.Generic;
using System.Text;
using Jose;

namespace EncryptionDemo.Sample
{
    public class JavascriptObjectSigningAndEncryption
    {
        public static string Encrypt(byte[] secret, string plainText)
        {
            // Claims see https://tools.ietf.org/html/rfc7519#section-4.1
            var nonEncryptedHeaders = new Dictionary<string, object>()
            {
                { "sub", "mr.x@contoso.com" },
                { "exp", 1300819380 }
            };

            var token = Jose.JWT.Encode(plainText, secret, JweAlgorithm.A256KW, JweEncryption.A256GCM,extraHeaders: nonEncryptedHeaders);
            return token;
        }

        public static string Decrypt(byte[] secret, string token)
        {
            var r = Jose.JWT.Decode(token, secret, JweAlgorithm.A256KW, JweEncryption.A256GCM);
            return r;
        }
    }
}

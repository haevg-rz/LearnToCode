using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EncryptionDemo.Sample
{
    public class CryptographicMessageSyntax
    {
        public static byte[] Encrypt(byte[] data, params X509Certificate2[] certs)
        {
            var envelopedCms = new EnvelopedCms(new ContentInfo(data));
            if (envelopedCms.ContentEncryptionAlgorithm.Oid.FriendlyName != "aes256")
            {
                // After .NET Core 3 aes256 is the standard ContentEncryptionAlgorithm, before it was 3des.
                throw new Exception("Only aes256 is allowed.");
            }

            var cmsRecipientCollection = new CmsRecipientCollection();
            foreach (var x509Certificate2 in certs)
            {
                cmsRecipientCollection.Add(new CmsRecipient(x509Certificate2, RSAEncryptionPadding.OaepSHA512));
            }

            envelopedCms.Encrypt(cmsRecipientCollection);

            return envelopedCms.Encode();
        }

        public static byte[] Decrypt(byte[] data, params X509Certificate2[] certs)
        {
            var envelopedCms = new EnvelopedCms();
            envelopedCms.Decode(data);

            envelopedCms.Decrypt(new X509Certificate2Collection(certs));

            return envelopedCms.Encode();
        }
    }
}
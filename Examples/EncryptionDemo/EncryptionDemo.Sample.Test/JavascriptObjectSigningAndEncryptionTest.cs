// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EncryptionDemo.Sample.Test
{
    public class JavascriptObjectSigningAndEncryptionTest
    {
        private readonly ITestOutputHelper outputHelper;

        public JavascriptObjectSigningAndEncryptionTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Fact]
        public void Encrypt()
        {
            #region Arange

            var key = Sample.KeyGeneration.CreateRandom(256);
            var plain = Guid.NewGuid().ToString();

            #endregion

            #region Act

            var cipherText = JavascriptObjectSigningAndEncryption.Encrypt(key, plain);

            #endregion

            #region Assert

            outputHelper.WriteLine("Token: " + cipherText);
            outputHelper.WriteLine("Secret: " + Convert.ToBase64String(key) + " (Base64Encode)");
            outputHelper.WriteLine("Secret: " + Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlEncode(key) + " (Base64UrlEncode)");

            plain.Should().NotBe(cipherText);

            #endregion
        }

        [Fact]
        public void Decrypt()
        {
            #region Arange

            var key = Sample.KeyGeneration.CreateRandom(256);
            var plain = Guid.NewGuid().ToString();
            var token = JavascriptObjectSigningAndEncryption.Encrypt(key, plain);

            #endregion

            #region Act

            var decrypted = JavascriptObjectSigningAndEncryption.Decrypt(key, token);

            #endregion

            #region Assert

            outputHelper.WriteLine("Token: " + token);
            outputHelper.WriteLine("Secret: " + Convert.ToBase64String(key) + " (Base64Encode)");
            outputHelper.WriteLine("Secret: " + Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlEncode(key) + " (Base64UrlEncode)");

            plain.Should().NotBe(token);
            decrypted.Should().Be(plain);

            #endregion
        }

        [Fact]
        public void EncryptWithPassword()
        {
            #region Arange

            var password = Guid.NewGuid().ToString();
            var plain = Guid.NewGuid().ToString();

            #endregion

            #region Act

            var cipherText = JavascriptObjectSigningAndEncryption.Encrypt(password, plain);

            #endregion

            #region Assert

            outputHelper.WriteLine("Token: " + cipherText);

            plain.Should().NotBe(cipherText);

            #endregion
        }

        [Fact]
        public void DecryptWithPassword()
        {
            #region Arange

            var password = Guid.NewGuid().ToString();
            var plain = Guid.NewGuid().ToString();
            var token = JavascriptObjectSigningAndEncryption.Encrypt(password, plain);

            #endregion

            #region Act

            var decrypted = JavascriptObjectSigningAndEncryption.Decrypt(password, token);

            #endregion

            #region Assert

            outputHelper.WriteLine("Token: " + token);

            plain.Should().NotBe(token);
            decrypted.Should().Be(plain);

            #endregion
        }

        // See https://github.com/dvsekhvalnov/jose-jwt/blob/e54de3bb706edf294053b4b86f0db47333d433ef/jose-jwt/crypto/ConcatKDF.cs#L43
        [Fact(Skip = "Not yet implemented")]
        public void EncryptWithEllipticCurve()
        {
            #region Arange

            // see https://tools.ietf.org/html/rfc7518#section-6.2.1.1
            var c = ECDiffieHellman.Create(ECCurve.NamedCurves.brainpoolP512r1);
            var exportParameters = c.ExportParameters(true);
            var plain = Guid.NewGuid().ToString();

            #endregion

            #region Act

            var token = JavascriptObjectSigningAndEncryption.Encrypt(exportParameters, plain);

            #endregion

            #region Assert

            outputHelper.WriteLine("Token: " + token);

            // TODO
            plain.Should().NotBe(token);

            #endregion
        }

        [Fact()]
        public void EncryptWithRSA()
        {
            #region Arange

            var rsa = RSA.Create(1024 * 4);
            var plain = Guid.NewGuid().ToString();

            #endregion

            #region Act

            var token = JavascriptObjectSigningAndEncryption.Encrypt(rsa, plain);

            #endregion

            #region Assert

            outputHelper.WriteLine("Token: " + token);

            plain.Should().NotBe(token);

            #endregion
        }

        [Fact()]
        public void DecryptWithRSA()
        {
            #region Arange

            var rsa = RSA.Create(1024 * 4);
            var plain = Guid.NewGuid().ToString();
            var token = JavascriptObjectSigningAndEncryption.Encrypt(rsa, plain);

            #endregion

            #region Act

            var decypted = JavascriptObjectSigningAndEncryption.Decrypt(rsa, token);

            #endregion

            #region Assert

            outputHelper.WriteLine("Token: " + token);


            plain.Should().Be(decypted);

            #endregion
        }
    }
}
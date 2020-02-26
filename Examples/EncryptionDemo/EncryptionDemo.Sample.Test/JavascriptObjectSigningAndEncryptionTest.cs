using System;
using System.Collections.Generic;
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

            outputHelper.WriteLine("Token: "+ cipherText);
            outputHelper.WriteLine("Secret: "+ Convert.ToBase64String(key)+ " (Base64Encode)");
            outputHelper.WriteLine("Secret: " + Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlEncode(key)+ " (Base64UrlEncode)");

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

            outputHelper.WriteLine("Token: "+ token);
            outputHelper.WriteLine("Secret: "+ Convert.ToBase64String(key)+ " (Base64Encode)");
            outputHelper.WriteLine("Secret: " + Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlEncode(key)+ " (Base64UrlEncode)");

            plain.Should().NotBe(token);
            decrypted.Should().Be(plain);

            #endregion
        }

    }
}

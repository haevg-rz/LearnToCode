// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using Xunit;

namespace EncryptionDemo.Sample.Test
{
    public class AsymmetricCryptographyRsaTest
    {
        public string AlicePrivatePublicXml { get; }
        public string AlicePublicXml { get; }
        public string BobPrivatePublicXml { get; }
        public string BobPublicXml { get; }
        public string EvePrivatePublicXml { get; }
        public string EvePublicXml { get; }
        public string PlainString { get; }

        public byte[] GetPlainData()
        {
            return Encoding.UTF8.GetBytes(PlainString);
        }

        public AsymmetricCryptographyRsaTest()
        {
            AlicePrivatePublicXml = AsymmetricCryptographyRsa.CreateRsaKeyPair();
            AlicePublicXml = AsymmetricCryptographyRsa.ExtractPublicKey(AlicePrivatePublicXml);

            BobPrivatePublicXml = AsymmetricCryptographyRsa.CreateRsaKeyPair();
            BobPublicXml = AsymmetricCryptographyRsa.ExtractPublicKey(BobPrivatePublicXml);

            EvePrivatePublicXml = AsymmetricCryptographyRsa.CreateRsaKeyPair();
            EvePublicXml = AsymmetricCryptographyRsa.ExtractPublicKey(EvePrivatePublicXml);

            PlainString = Guid.NewGuid().ToString();
        }

        [Fact]
        public void SignAndVerifyPlainTest()
        {
            #region Arange

            var plainData = GetPlainData();

            #endregion

            #region Act

            var signature = AsymmetricCryptographyRsa.Sign(AlicePrivatePublicXml, plainData);
            var verify = AsymmetricCryptographyRsa.Verify(AlicePublicXml, plainData, signature);

            #endregion

            #region Assert

            // the RSA signature size is dependent on the key size
            signature.Should().HaveCount(i => i == 512);
            signature.Should().NotContainNulls();
            verify.Should().Be(true);

            #endregion
        }

        [Theory]
        [InlineData("NoTamper", true)]
        [InlineData("TamperSignature", false)]
        [InlineData("TamperPlainData", false)]
        public void SignAndVerifyPlainWithTamperTest(string testCase, bool isSignatureValid)
        {
            #region Arange

            var plainData = GetPlainData();

            #endregion

            #region Act

            var signature = AsymmetricCryptographyRsa.Sign(AlicePrivatePublicXml, plainData);
            switch (testCase)
            {
                case "TamperSignature":
                    signature[signature.Length / 2] ^= signature[signature.Length / 2];
                    break;
                case "TamperPlainData":
                    plainData[plainData.Length / 2] ^= plainData[plainData.Length / 2];
                    break;
            }

            var verify = AsymmetricCryptographyRsa.Verify(AlicePublicXml, plainData, signature);

            #endregion

            #region Assert

            // the RSA signature size is dependent on the key size
            signature.Should().HaveCount(i => i == 512);
            signature.Should().NotContainNulls();
            verify.Should().Be(isSignatureValid);

            #endregion
        }
    }
}
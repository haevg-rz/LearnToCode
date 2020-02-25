using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace EncryptionDemo.Sample.Test
{
    public class AsymmetricCryptographyEccTest
    {
        private readonly ITestOutputHelper outputHelper;

        public AsymmetricCryptographyEccTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Fact]
        public void CreateKeyPair()
        {
            #region Arange



            #endregion

            #region Act

            var keyPair = AsymmetricCryptographyEcc.CreateKeyPair();

            outputHelper.WriteLine(Convert.ToBase64String(keyPair.privateKeyInfo));
            outputHelper.WriteLine(Convert.ToBase64String(keyPair.subjectPublicKeyInfo));

            #endregion

            #region Assert

            keyPair.privateKeyInfo.Should().NotEqual(keyPair.subjectPublicKeyInfo);
            keyPair.privateKeyInfo.Should().HaveCountGreaterThan(keyPair.subjectPublicKeyInfo.Length);

            #endregion
        }

        [Fact]
        public void GetPublicKey()
        {
            #region Arange

            var keyPair = AsymmetricCryptographyEcc.CreateKeyPair();

            #endregion

            #region Act

            var publicKey = AsymmetricCryptographyEcc.GetPublicKey(keyPair.privateKeyInfo);

            outputHelper.WriteLine(Convert.ToBase64String(keyPair.subjectPublicKeyInfo));
            outputHelper.WriteLine(Convert.ToBase64String(publicKey));

            #endregion

            #region Assert

            publicKey.Should().Equal(keyPair.subjectPublicKeyInfo);

            #endregion
        }

        [Fact]
        public void Derive()
        {
            #region Arange

            var keyPairAlive = AsymmetricCryptographyEcc.CreateKeyPair();
            var keyPairBob = AsymmetricCryptographyEcc.CreateKeyPair();

            #endregion

            #region Act

            var deriveSecretA = AsymmetricCryptographyEcc.DeriveSecret(keyPairAlive.privateKeyInfo, keyPairBob.subjectPublicKeyInfo);
            var deriveSecretB = AsymmetricCryptographyEcc.DeriveSecret(keyPairBob.privateKeyInfo, keyPairAlive.subjectPublicKeyInfo);

            #endregion

            #region Assert

            deriveSecretA.Should().Equal(deriveSecretB);

            #endregion
        }


        [Fact]
        public void SignData()
        {
            #region Arange

            var keyPairAlive = AsymmetricCryptographyEcc.CreateKeyPair();

            var dataA = Guid.Empty.ToByteArray();

            #endregion

            #region Act

            var signatureA = AsymmetricCryptographyEcc.SignData(keyPairAlive.privateKeyInfo, dataA);
            var signatureB = AsymmetricCryptographyEcc.SignData(keyPairAlive.privateKeyInfo, dataA);

            outputHelper.WriteLine(Convert.ToBase64String(signatureA));
            outputHelper.WriteLine(Convert.ToBase64String(signatureB));
            outputHelper.WriteLine("");
            outputHelper.WriteLine(BitConverter.ToString(signatureA).Replace("-", "").ToLower());
            outputHelper.WriteLine(BitConverter.ToString(signatureB).Replace("-", "").ToLower());

            #endregion

            #region Assert

            // The signature generation algorithm includes the selecting of a cryptographically secure random integer
            // https://en.wikipedia.org/wiki/Elliptic_Curve_Digital_Signature_Algorithm

            signatureA.Should().NotEqual(signatureB);

            #endregion
        }

        [Fact]
        public void VerifyData()
        {
            #region Arange

            var keyPairAlive = AsymmetricCryptographyEcc.CreateKeyPair();

            var dataA = Guid.Empty.ToByteArray();
            var signatureA = AsymmetricCryptographyEcc.SignData(keyPairAlive.privateKeyInfo, dataA);

            #endregion

            #region Act

            var verifyData = AsymmetricCryptographyEcc.VerifyData(keyPairAlive.subjectPublicKeyInfo, dataA, signatureA);

            #endregion

            #region Assert

            verifyData.Should().BeTrue();

            #endregion
        }
    }
}

// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace EncryptionDemo.Sample.Test
{
    public class UseCaseTest
    {
        private readonly ITestOutputHelper outputHelper;

        public UseCaseTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Fact(DisplayName = "UseCase for Asymmetric Encryption")]
        public void UseCase_Asymmetric()
        {
            string alicePlainText;
            string bobPlainText;

            outputHelper.WriteLine($"Alice is creating a RSA key pair");
            var rsaAlicePrivate = AsymmetricCryptographyRsa.CreateRsaKeyPair();
            var rsaAlicePublic = AsymmetricCryptographyRsa.ExtractPublicKey(rsaAlicePrivate);

            outputHelper.WriteLine($"Bob is creating a RSA key pair");
            var rsaBobPrivate = AsymmetricCryptographyRsa.CreateRsaKeyPair();
            var rsaBobPublic = AsymmetricCryptographyRsa.ExtractPublicKey(rsaBobPrivate);

            byte[] encryptedKey;
            byte[] signature;
            SymmetricEncryption.EncryptedDataContainer encryptedDataContainer;

            // Alice
            {
                alicePlainText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
                outputHelper.WriteLine($"Alice wants to encrypt: {alicePlainText}");

                var key = Sample.KeyGeneration.CreateRandom(256);
                outputHelper.WriteLine($"Alice is creating a random secret: {key.ToText()}");

                encryptedKey = AsymmetricCryptographyRsa.Encrypt(rsaBobPublic, key);
                outputHelper.WriteLine($"Alice encrypts the key with bobs public key: {encryptedKey.ToText()}");

                encryptedDataContainer = SymmetricEncryption.Encrypt(key, alicePlainText.ToData(), new byte[0]);
                outputHelper.WriteLine($"Alice encrypts the data with the secret key");

                signature = AsymmetricCryptographyRsa.Sign(rsaAlicePrivate, GetData(encryptedDataContainer));
                outputHelper.WriteLine($"Alice sign the cipher text");

                outputHelper.WriteLine($"Alice sends cipher text, signature and her public key to bob");
            }

            // Bob
            {
                var isValid = AsymmetricCryptographyRsa.Verify(rsaAlicePublic, GetData(encryptedDataContainer), signature);
                outputHelper.WriteLine($"Bobs verify the signature of the cipher text, it is valid: {isValid}");

                var decryptedKey = AsymmetricCryptographyRsa.Decrypt(rsaBobPrivate, encryptedKey);
                outputHelper.WriteLine($"Bobs decrypt the decrypted key from Alice with his private key: {decryptedKey.ToText()}");

                var decryptedData = SymmetricEncryption.Decrypt(decryptedKey, encryptedDataContainer);
                bobPlainText = decryptedData.ToUtf8String();
                outputHelper.WriteLine($"Bobs decrypt the data from Alice with his private key: {bobPlainText}");
            }

            alicePlainText.Should().BeEquivalentTo(bobPlainText, "The plain text must be the same.");
        }

        private byte[] GetData(SymmetricEncryption.EncryptedDataContainer encryptedDataContainer)
        {
            return encryptedDataContainer.AssociatedData
                .Concat(encryptedDataContainer.CipherText)
                .Concat(encryptedDataContainer.Nonce)
                .Concat(encryptedDataContainer.Tag).ToArray();
        }


        [Fact(DisplayName = "UseCase for Symmetric Encryption")]
        public void UseCase_Symmetric()
        {
            string sharedPassword;
            (SymmetricEncryption.EncryptedDataContainer encryptedDataContainer, byte[] salt) fileforBob;

            string alicePlainText;
            string bobPlainText;

            // Alice
            {
                var plain = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
                alicePlainText = plain;
                outputHelper.WriteLine($"Alice wants to encrypt: {plain}");

                var alicePassword = "Correct Horse Battery Staple";
                outputHelper.WriteLine($"Alice will use password: {alicePassword}");
                sharedPassword = alicePassword;
                outputHelper.WriteLine($"Alice tells Bob the password");

                var (key, salt) = Sample.KeyGeneration.CreateFromPassword(alicePassword, 256);
                outputHelper.WriteLine($"The key derivation algorithm will create this key:  {key.ToText()}");
                outputHelper.WriteLine($"The key derivation algorithm will create this salt: {salt.ToText()}");
                outputHelper.WriteLine($"The key derivation settings are known");
                outputHelper.WriteLine($"HashAlgorithm:  {Sample.KeyGeneration.HashAlgorithmSha256.ToString()}");
                outputHelper.WriteLine($"SaltSizeInBits: {Sample.KeyGeneration.SaltSizeInBits}");
                outputHelper.WriteLine($"Iterations:     {Sample.KeyGeneration.Iterations:N0}");

                outputHelper.WriteLine($"Alice will encrypt this data with AES GCM.");
                var associatedData = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                outputHelper.WriteLine($"Alice will this unencrypted information: {associatedData}");
                var encryptedDataContainer = SymmetricEncryption.Encrypt(key, plain.ToData(), associatedData.ToData());

                outputHelper.WriteLine($"This is the ciphertext: {encryptedDataContainer.CipherText.ToText()}");
                outputHelper.WriteLine($"This is the tag, the authentication tag derived from the ciphertext: {encryptedDataContainer.Nonce.ToText()}");
                outputHelper.WriteLine($"This is the nonce: {encryptedDataContainer.Tag.ToText()}");

                fileforBob = (encryptedDataContainer: encryptedDataContainer, salt: salt);
                outputHelper.WriteLine("Alice sends ciphertext, tag, nonce and salt to bob");
            }

            outputHelper.WriteLine("--------------------------------------------------");

            // Bob
            {
                outputHelper.WriteLine($"Bob got the passwort from alice and the ciphertext, tag, nonce, associatedData and salt");
                outputHelper.WriteLine(JsonConvert.SerializeObject(fileforBob.encryptedDataContainer, Formatting.Indented));
                outputHelper.WriteLine($"Salt: {fileforBob.salt}");

                outputHelper.WriteLine($"Bob will use the password: {sharedPassword}");

                var (key, _) = Sample.KeyGeneration.CreateFromPassword(sharedPassword, 256, fileforBob.salt);
                outputHelper.WriteLine($"The key derivation algorithm will create this key:  {key.ToText()}");

                outputHelper.WriteLine($"Bob will decrypt this data with AES GCM.");
                outputHelper.WriteLine($"Bob make shure, that the associatedData is as expected: {fileforBob.encryptedDataContainer.AssociatedData.ToUtf8String()}");
                var plainData = SymmetricEncryption.Decrypt(key, fileforBob.encryptedDataContainer);
                bobPlainText = plainData.ToUtf8String();

                outputHelper.WriteLine($"Bob decrypted this data: {plainData.ToUtf8String()}");
                outputHelper.WriteLine("Because we use an authenticated encryption with associated data (AEAD) Bob can be sure, that this data wasn't change.");
            }

            alicePlainText.Should().BeEquivalentTo(bobPlainText, "The plain text must be the same.");
        }
    }

    public static class Extensions
    {
        public static string ToText(this byte[] data)
        {
            return Convert.ToBase64String(data) + $" ({data.Length * 8} bit)";
        }

        public static byte[] ToData(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string ToUtf8String(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
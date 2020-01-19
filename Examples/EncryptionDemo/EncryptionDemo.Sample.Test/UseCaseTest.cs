using System;
using System.Collections.Generic;
using System.Globalization;
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
        
        [Fact(DisplayName = "UseCase for Symmetric Encryption")]
        public void UseCase_Symmetric()
        {
            string sharedPasswort;
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
                sharedPasswort = alicePassword;
                outputHelper.WriteLine($"Alice tells Bob the password");

                var (key, salt) = KeyGeneration.CreateFromPassword(alicePassword, 256);
                outputHelper.WriteLine($"The key derivation algorithm will create this key:  {key.ToText()}");
                outputHelper.WriteLine($"The key derivation algorithm will create this salt: {salt.ToText()}");
                outputHelper.WriteLine($"The key derivation settings are known");
                outputHelper.WriteLine($"HashAlgorithm:  {KeyGeneration.HashAlgorithmSha256.ToString()}");
                outputHelper.WriteLine($"SaltSizeInBits: {KeyGeneration.SaltSizeInBits}");
                outputHelper.WriteLine($"Iterations:     {KeyGeneration.Iterations:N0}");

                outputHelper.WriteLine($"Alice will encrypt this data with AES GCM.");
                var associatedData = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                outputHelper.WriteLine($"Alice will this unencrypted information: {associatedData}");
                var encryptedDataContainer = SymmetricEncryption.Encrypt(key, plain.ToData(), associatedData.ToData());

                outputHelper.WriteLine($"This is the ciphertext: {encryptedDataContainer.CipherText.ToText()}");
                outputHelper.WriteLine($"This is the tag, the authentication tag derived from the ciphertext: {encryptedDataContainer.Nonce.ToText()}");
                outputHelper.WriteLine($"This is the nonce: {encryptedDataContainer.Tag.ToText()}");

                fileforBob = (encryptedDataContainer: encryptedDataContainer, salt:salt);
                outputHelper.WriteLine("Alice sends ciphertext, tag, nonce and salt to bob");
            }

            outputHelper.WriteLine("--------------------------------------------------");

            // Bob
            {
                outputHelper.WriteLine($"Bob got the passwort from alice and the ciphertext, tag, nonce, associatedData and salt");
                outputHelper.WriteLine(JsonConvert.SerializeObject(fileforBob.encryptedDataContainer, Formatting.Indented));
                outputHelper.WriteLine($"Salt: {fileforBob.salt}");

                outputHelper.WriteLine($"Bob will use the password: {sharedPasswort}");
                
                var (key, _) = KeyGeneration.CreateFromPassword(sharedPasswort, 256, fileforBob.salt);
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
            return Convert.ToBase64String(data)+$" ({data.Length*8} bit)";
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

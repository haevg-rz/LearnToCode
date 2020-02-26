// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace EncryptionDemo.Sample.Test
{
    public class KeyGeneration
    {
        [Theory()]
        [InlineData(512)]
        [InlineData(256)]
        [InlineData(128)]
        public void CreateRandom(int keySizeBit)
        {
            #region Arange

            #endregion

            #region Act

            var key = Sample.KeyGeneration.CreateRandom(keySizeBit);

            #endregion

            #region Assert

            key.Should().HaveCount(c => c == keySizeBit / 8);

            #endregion
        }

        [Theory()]
        [InlineData(512)]
        [InlineData(256)]
        [InlineData(128)]
        public void CreateFromPassword(int keySizeBit)
        {
            #region Arange

            var password = Guid.NewGuid().ToString();

            #endregion

            #region Act

            var (key1, salt) = Sample.KeyGeneration.CreateFromPassword(password, keySizeBit);
            var (key2, _) = Sample.KeyGeneration.CreateFromPassword(password, keySizeBit, salt);

            #endregion

            #region Assert

            key1.Should().HaveCount(c => c == keySizeBit / 8);
            key2.Should().HaveCount(c => c == keySizeBit / 8);

            salt.Should().HaveCount(c => c == 256 / 8);

            key1.Should().BeEquivalentTo(key2);

            #endregion
        }

        [Theory()]
        [InlineData("TamperSalt", 512)]
        [InlineData("TamperPassword", 512)]
        public void CreateFromPassword_Tamper(string testCase, int keySizeBit)
        {
            #region Arange

            var password = Guid.NewGuid().ToString();

            #endregion

            #region Act

            var (key1, salt) = Sample.KeyGeneration.CreateFromPassword(password, keySizeBit);
            switch (testCase)
            {
                case "TamperSalt":
                    salt[salt.Length / 2] ^= salt[salt.Length / 2];
                    break;
                case "TamperPassword":
                    password += "#";
                    break;
            }

            var (key2, _) = Sample.KeyGeneration.CreateFromPassword(password, keySizeBit, salt);

            #endregion

            #region Assert

            key1.Should().HaveCount(c => c == keySizeBit / 8);
            key2.Should().HaveCount(c => c == keySizeBit / 8);

            salt.Should().HaveCount(c => c == 256 / 8);

            key1.Should().NotBeEquivalentTo(key2);

            #endregion
        }
    }
}
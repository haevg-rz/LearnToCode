using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;

namespace EncryptionDemo.Sample.Test
{
    public class AesTest
    {
        public string PlainString { get; } = Guid.NewGuid().ToString();
        public byte[] PlainData  => Encoding.UTF8.GetBytes(PlainString);

        [Fact]
        public void Encrypt()
        {
            #region Arange

            var key = Key.CreateRandom(256);

            #endregion

            #region Act


            var r = Aes.Encrypt(key, PlainData);

            #endregion

            #region Assert

            r.CipherText.Should().NotBeEquivalentTo(PlainData);
            r.Nonce.Should().NotBeEquivalentTo(new byte[r.Nonce.Length]);
            r.Tag.Should().NotBeEquivalentTo(new byte[r.Tag.Length]);

            #endregion
        }
    }
}

﻿using System;
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

            var key = KeyGeneration.CreateRandom(256);
            var associatedData = Guid.NewGuid().ToByteArray();

            #endregion

            #region Act


            var r = SymmetricEncryption.Encrypt(key, PlainData, associatedData);

            #endregion

            #region Assert

            r.CipherText.Should().NotBeEquivalentTo(PlainData);
            r.Nonce.Should().NotBeEquivalentTo(new byte[r.Nonce.Length]);
            r.Tag.Should().NotBeEquivalentTo(new byte[r.Tag.Length]);

            #endregion
        }
    }
}

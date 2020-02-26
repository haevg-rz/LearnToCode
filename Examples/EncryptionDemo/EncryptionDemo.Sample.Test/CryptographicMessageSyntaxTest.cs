// This code is only for demo purpose!
// Don't be a fool by using this implementation in production.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EncryptionDemo.Sample.Test
{
    public class CryptographicMessageSyntaxTest
    {
        private readonly ITestOutputHelper outputHelper;

        public CryptographicMessageSyntaxTest(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        private X509Certificate2 ReadX509Certificate2(int i)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var name = executingAssembly.GetManifestResourceNames()
                .First(s => s.EndsWith($"Certificate_{i}_pw_qwert.pfx"));

            var ms = new MemoryStream();
            executingAssembly.GetManifestResourceStream(name).CopyTo(ms);

            var secureString = new SecureString();
            foreach (var item in "qwert") secureString.AppendChar(item);

            return new X509Certificate2(ms.ToArray(), secureString);
        }


        [Fact]
        public void Encrypt()
        {
            var data = Guid.NewGuid().ToByteArray();
            var cert = ReadX509Certificate2(1);

            var cms = CryptographicMessageSyntax.Encrypt(data, cert);

            data.Should().NotEqual(cms);
        }

        [Fact]
        public void Decrypt()
        {
            var data = Guid.NewGuid().ToByteArray();
            var cert = ReadX509Certificate2(1);

            var cms = CryptographicMessageSyntax.Encrypt(data, cert);
            var plain = CryptographicMessageSyntax.Decrypt(cms, cert);

            outputHelper.WriteLine(cms.Length + ": " + Convert.ToBase64String(cms));

            data.Should().NotEqual(cms);
            data.Should().Equal(plain);
        }
    }
}
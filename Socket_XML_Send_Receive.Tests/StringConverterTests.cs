using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Socket_XML_Send_Receive.Tests
{
    [TestFixture]
    public class StringConverterTests
    {
        [Test]
        public void ConvertsToASCIIWithoutPrefix()
        {
            var result = StringConverter.GetBytesToSend("ASCII","asds",false);
            byte[] array = { 97, 115, 100, 115 };
            Assert.That(result, Is.EqualTo(array));
        }

        [Test]
        public void ConvertsToASCIIWithPrefix()
        {
            var result = StringConverter.GetBytesToSend("ASCII", "asds", true);
            byte[] array = { 0, 0, 0, 4, 97, 115, 100, 115 };
            Assert.That(result, Is.EqualTo(array));
        }

        [Test]
        public void ConvertsToStringFromAsciiBytes()
        {
            byte[] bytes = { 97, 115, 100, 115 };
            string stringReceived;
            var result = StringConverter.TryGetStringFromBytes(bytes, 4, false, out stringReceived, "ASCII", null, null);
            Assert.That(stringReceived, Is.EqualTo("asds"));
            Assert.That(result, Is.True);
        }

        [Test]
        public void ConvertToStringFromUnicodeBytes()
        {
            byte[] bytes = { 0x61, 0, 0x62, 0, 0x63, 0};
            string stringReceived;
            var result = StringConverter.TryGetStringFromBytes(bytes, bytes.Length, false, out stringReceived, "Unicode", null, null);
            Assert.That(stringReceived, Is.EqualTo("abc"));
            Assert.That(result, Is.True);
        }

    }

    
  
}

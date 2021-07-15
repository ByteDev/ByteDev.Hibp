using System;
using NUnit.Framework;

namespace ByteDev.Hibp.UnitTests
{
    [TestFixture]
    public class HibpClientTests
    {
        [TestFixture]
        public class Constructor : HibpClientTests
        {
            [Test]
            public void WhenHttpClientIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentNullException>(() => _ = new HibpClient(null));
            }
        }
    }
}
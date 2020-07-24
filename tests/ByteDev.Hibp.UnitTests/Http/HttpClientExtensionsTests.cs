using System.Linq;
using System.Net.Http;
using ByteDev.Hibp.Http;
using NUnit.Framework;

namespace ByteDev.Hibp.UnitTests.Http
{
    [TestFixture]
    public class HttpClientExtensionsTests
    {
        private HttpClient _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HttpClient();
        }

        [TestFixture]
        public class AddRequestHeaderUserAgent : HttpClientExtensionsTests
        {
            private const string ExpectedUserAgent = "ByteDev-HibpClient";

            [Test]
            public void WhenUserAgentNotSpecified_ThenAddsUserAgent()
            {
                _sut.AddRequestHeaderUserAgent();
                
                Assert.That(_sut.DefaultRequestHeaders.UserAgent.Single().ToString(), Is.EqualTo(ExpectedUserAgent));
            }

            [Test]
            public void WhenUserAgentSpecified_ThenRemovesExistingUserAgent()
            {
                _sut.DefaultRequestHeaders.Add("User-Agent", "SomeClient");

                _sut.AddRequestHeaderUserAgent();

                Assert.That(_sut.DefaultRequestHeaders.UserAgent.Single().ToString(), Is.EqualTo(ExpectedUserAgent));
            }
        }

        [TestFixture]
        public class AddRequestHeaderContentNegotiation : HttpClientExtensionsTests
        {
            private const string ExpectedAccept = @"application/vnd.haveibeenpwned.v2+json";

            [Test]
            public void WhenContentNegotiationNotSpecified_ThenAddsContentNegotiation()
            {
                _sut.AddRequestHeaderContentNegotiation();
                
                Assert.That(_sut.DefaultRequestHeaders.Accept.Single().ToString(), Is.EqualTo(ExpectedAccept));
            }

            [Test]
            public void WhenContentNegotiationSpecified_ThenRemovesExistingContentNegotiation()
            {
                _sut.DefaultRequestHeaders.Add("Accept", "application/xml");

                _sut.AddRequestHeaderContentNegotiation();

                Assert.That(_sut.DefaultRequestHeaders.Accept.Single().ToString(), Is.EqualTo(ExpectedAccept));
            }
        }        
    }
}

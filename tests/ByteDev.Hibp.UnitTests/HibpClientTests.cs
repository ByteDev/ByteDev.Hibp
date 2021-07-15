using System;
using System.Net.Http;
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

        [TestFixture]
        public class GetAccountBreachesAsync : HibpClientTests
        {
            [Test]
            public void WhenRateLimitExceeded_AndRetryDisabled_ThenThrowException()
            {
                var options = new HibpClientOptions
                {
                    RetryOnRateLimitExceeded = false
                };

                var sut = new HibpClient(new HttpClient(new Return429RateLimitExceededResponseHandler()), options);

                Assert.ThrowsAsync<HibpClientException>(() => _ = sut.GetAccountBreachesAsync("john@somewhere.com"));
            }
        }
    }
}
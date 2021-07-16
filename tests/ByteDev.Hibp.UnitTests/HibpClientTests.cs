using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            private const string EmailAddress = "john@somewhere.com";

            [Test]
            public void WhenRateLimitExceeded_AndRetryDisabled_ThenThrowException()
            {
                var options = new HibpClientOptions
                {
                    RetryOnRateLimitExceeded = false
                };

                var sut = new HibpClient(new HttpClient(new Return429RateLimitExceededResponseHandler()), options);
                
                Assert.ThrowsAsync<HibpClientException>(() => _ = sut.GetAccountBreachesAsync(EmailAddress));
            }

            [Test]
            public async Task WhenRateLimitExceededOnlyOnFirstRequest_AndRetryEnabled_ThenReturnOkResponse()
            {
                var options = new HibpClientOptions
                {
                    RetryOnRateLimitExceeded = true
                };

                var sut = new HibpClient(new HttpClient(new Return429RateLimitExceededResponseHandler()), options);

                var result = await sut.GetAccountBreachesAsync(EmailAddress);

                Assert.That(result.Count(), Is.EqualTo(3));
            }
        }
    }
}
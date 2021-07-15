using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ByteDev.Hibp.Contract.Request;
using NUnit.Framework;

namespace ByteDev.Hibp.IntTests
{
    [TestFixture]
    public class HibpClientTests
    {
        private const int TotalSiteBreaches = 546;       // As of 15/07/2021

        private const string EmailAddressPwned = @"johnsmith@gmail.com";
        private const string EmailAddressNotPwned = @"djs834sskldi4999dspo@gmail.com";

        private const string DomainBreached = "apollo.io";
        private const string DomainNotBreached = "google.com";

        private const string BreachNameExists = "000webhost";
        private const string BreachNameDoesNotExists = "breachDoesNotExist";

        private IHibpClient _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HibpClient(new HttpClient(), ApiKeys.Valid);
        }

        [TearDown]
        public void TearDown()
        {
            // Throttling the API will result in response code error 429 returned.
            Thread.Sleep(1500);
        }

        [TestFixture]
        public class GetAccountBreachesAsync : HibpClientTests
        {
            [Test]
            [Ignore("Run ad-hoc. Can affect other tests when run in set.")]
            public async Task WhenRequestTooManyTimes_ThenThrowException()
            {
                try
                {
                    for (var i = 0; i < 5; i++)
                    {
                        Console.WriteLine("Request: " + i);
                        await _sut.GetAccountBreachesAsync(EmailAddressNotPwned);
                    }
                }
                catch (HibpClientException ex)
                {
                    StringAssert.StartsWith("Unhandled StatusCode: '429 TooManyRequests' returned.  Response body:\n'{ \"statusCode\": 429, \"message\": \"Rate limit is exceeded.", ex.Message);
                }
            }

            [Test]
            public void WhenApiKeyNotValid_ThenThrowException()
            {
                var sut = new HibpClient(new HttpClient(), ApiKeys.NotValid);

                var ex = Assert.ThrowsAsync<HibpClientException>(() => sut.GetAccountBreachesAsync(EmailAddressNotPwned));
                Assert.That(ex.Message, Is.EqualTo("Unhandled StatusCode: '401 Unauthorized' returned.  Response body:\n'{ \"statusCode\": 401, \"message\": \"Access denied due to invalid hibp-api-key.\" }'."));
            }

            [Test]
            public async Task WhenEmailAddressNotPwnd_ThenReturnsHasNotBeenPwned()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressNotPwned);

                Assert.That(result, Is.Empty);
            }
            
            [Test]
            public async Task WhenEmailAddressPwned_AndTruncateResponse_ThenReturnsOnlyBreachNames()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned, new HibpRequestOptions
                {
                    TruncateResponse = true
                });

                Assert.That(result.First().Name.Length, Is.GreaterThan(0));
                // All other properties are set to default (truncated).
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndNotTruncateResponse_ThenReturnsAllDetails()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned, new HibpRequestOptions
                {
                    TruncateResponse = false
                });
                
                var first = result.First();
                    
                Assert.That(first.Domain.Length, Is.GreaterThan(0));
                Assert.That(first.Description.Length, Is.GreaterThan(0));
                Assert.That(first.Title.Length, Is.GreaterThan(0));
                Assert.That(first.Name.Length, Is.GreaterThan(0));
                Assert.That(first.PwnCount, Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndIncludeUnverified_ThenReturnUnverifiedAsWell()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned, new HibpRequestOptions
                {
                    IncludeUnverified = true
                });

                Assert.That(result.Count(), Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndFilterByDomain_ThenReturnOnlyDomainBreach()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned, new HibpRequestOptions
                {
                    FilterByDomain = DomainBreached,
                    TruncateResponse = false
                });

                Assert.That(result.Single().Domain, Is.EqualTo(DomainBreached));
            }
        }

        [TestFixture]
        public class GetBreachedSitesAsync : HibpClientTests
        {
            [Test]
            public async Task WhenNoDomainFilter_ThenReturnAllSites()
            {
                var result = await _sut.GetBreachedSitesAsync();
                
                Assert.That(result.Count(), Is.GreaterThanOrEqualTo(TotalSiteBreaches));   
            }

            [Test]
            public async Task WhenDomainIsBreached_ThenReturnSiteBreach()
            {
                var result = await _sut.GetBreachedSitesAsync(DomainBreached);

                Assert.That(result.Single().Domain, Is.EqualTo(DomainBreached));
            }

            [Test]
            public async Task WhenDomainIsNotBreached_ThenReturnNoBreaches()
            {
                var result = await _sut.GetBreachedSitesAsync(DomainNotBreached);

                Assert.That(result, Is.Empty);
            }
        }

        [TestFixture]
        public class GetBreachSiteByNameAsync : HibpClientTests
        {
            [Test]
            public async Task WhenBreachExists_ThenReturnBreachSite()
            {
                var result = await _sut.GetBreachSiteByNameAsync(BreachNameExists);

                Assert.That(result.Name, Is.EqualTo(BreachNameExists));
            }

            [Test]
            public async Task WhenBreachDoesNotExist_ThenReturnNull()
            {
                var result = await _sut.GetBreachSiteByNameAsync(BreachNameDoesNotExists);

                Assert.That(result, Is.Null);
            }
        }

        [TestFixture]
        public class GetDataClassesAsync : HibpClientTests
        {
            [Test]
            public async Task WhenCalled_ThenReturnsAllDataClasses()
            {
                var result = await _sut.GetDataClassesAsync();

                Assert.That(result.Count(), Is.GreaterThan(0));
            }
        }

        [TestFixture]
        public class GetAccountPastesAsync : HibpClientTests
        {
            [Test]
            public async Task WhenEmailAddressAccountExists_ThenReturnPastes()
            {
                var result = await _sut.GetAccountPastesAsync(EmailAddressPwned);

                Assert.That(result.Count(), Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressAccountDoesNotExists_ThenReturnEmpty()
            {
                var result = await _sut.GetAccountPastesAsync(EmailAddressNotPwned);

                Assert.That(result, Is.Empty);
            }
        }
    }
}
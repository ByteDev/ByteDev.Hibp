using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ByteDev.Hibp.IntTests
{
    [TestFixture]
    public class HibpClientTests
    {
        private const string EmailAddressPwned = @"johnsmith@gmail.com";
        private const string EmailAddressNotPwned = @"djs834sskldi4999dspo@gmail.com";

        private const string DomainBreached = "apollo.io";
        private const string DomainNotBreached = "google.com";

        private HibpClient _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HibpClient(new HttpClient());
        }

        [TestFixture]
        public class GetBreachedSitesAsync : HibpClientTests
        {
            [Test]
            public async Task WhenNoDomainFilter_ThenReturnAllSites()
            {
                const int currentTotalSiteBreaches = 369;       // As of 10/07/2019 there were 369 site breaches

                var result = await _sut.GetBreachedSitesAsync();
                
                Assert.That(result.Breaches.Count(), Is.GreaterThanOrEqualTo(currentTotalSiteBreaches));   
                Assert.That(result.IsPwned, Is.True);
            }

            [Test]
            public async Task WhenDomainIsBreached_ThenReturnSiteBreach()
            {
                var result = await _sut.GetBreachedSitesAsync(DomainBreached);

                Assert.That(result.Breaches.Single().Domain, Is.EqualTo(DomainBreached));
                Assert.That(result.IsPwned, Is.True);
            }

            [Test]
            public async Task WhenDomainIsNotBreached_ThenReturnNoBreaches()
            {
                var result = await _sut.GetBreachedSitesAsync(DomainNotBreached);

                Assert.That(result.Breaches, Is.Empty);
                Assert.That(result.IsPwned, Is.False);
            }
        }

        [TestFixture]
        public class GetHasBeenPwnedAsync : HibpClientTests
        {
            [Test]
            public async Task WhenEmailAddressNotPwnd_ThenReturnsHasNotBeenPwned()
            {
                var result = await _sut.GetHasBeenPwnedAsync(EmailAddressNotPwned);

                Assert.That(result.IsPwned, Is.False);
                Assert.That(result.Breaches, Is.Empty);
            }

            [Test]
            public async Task WhenEmailAddressPwnd_ThenReturnsHasBeenPwned()
            {
                var result = await _sut.GetHasBeenPwnedAsync(EmailAddressPwned);

                Assert.That(result.IsPwned, Is.True);
                Assert.That(result.Breaches.Count(), Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndTruncateResponse_ThenReturnsOnlyBreachNames()
            {
                var result = await _sut.GetHasBeenPwnedAsync(EmailAddressPwned, new HibpRequestOptions { TruncateResponse = true});

                Assert.That(result.Breaches.Count(), Is.GreaterThan(0));
                Assert.That(result.Breaches.First().Name.Length, Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndIncludeUnverified_ThenReturnUnverifiedAsWell()
            {
                var result = await _sut.GetHasBeenPwnedAsync(EmailAddressPwned, new HibpRequestOptions { IncludeUnverified = true });

                Assert.That(result.Breaches.Count(), Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndFilterByDomain_ThenReturnOnlyDomainBreach()
            {
                var result = await _sut.GetHasBeenPwnedAsync(EmailAddressPwned, new HibpRequestOptions { FilterByDomain = DomainBreached });

                Assert.That(result.Breaches.Single().Domain, Is.EqualTo(DomainBreached));
            }
        }
    }
}
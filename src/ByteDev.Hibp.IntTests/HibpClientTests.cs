using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ByteDev.Hibp.Request;
using NUnit.Framework;

namespace ByteDev.Hibp.IntTests
{
    [TestFixture]
    public class HibpClientTests
    {
        private const int TotalSiteBreaches = 369;       // As of 10/07/2019 there were 369 site breaches

        private const string EmailAddressPwned = @"johnsmith@gmail.com";
        private const string EmailAddressNotPwned = @"djs834sskldi4999dspo@gmail.com";

        private const string DomainBreached = "apollo.io";
        private const string DomainNotBreached = "google.com";

        private const string BreachNameExists = "000webhost";
        private const string BreachNameDoesNotExists = "breachDoesNotExist";

        private HibpClient _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HibpClient(new HttpClient());
        }

        [TestFixture]
        public class GetAccountBreachesAsync : HibpClientTests
        {
            [Test]
            public async Task WhenEmailAddressNotPwnd_ThenReturnsHasNotBeenPwned()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressNotPwned);

                Assert.That(result, Is.Empty);
            }

            [Test]
            public async Task WhenEmailAddressPwnd_ThenReturnsHasBeenPwned()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned);

                Assert.That(result.Count(), Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndTruncateResponse_ThenReturnsOnlyBreachNames()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned, new HibpRequestOptions { TruncateResponse = true});

                Assert.That(result.Count(), Is.GreaterThan(0));
                Assert.That(result.First().Name.Length, Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndIncludeUnverified_ThenReturnUnverifiedAsWell()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned, new HibpRequestOptions { IncludeUnverified = true });

                Assert.That(result.Count(), Is.GreaterThan(0));
            }

            [Test]
            public async Task WhenEmailAddressPwned_AndFilterByDomain_ThenReturnOnlyDomainBreach()
            {
                var result = await _sut.GetAccountBreachesAsync(EmailAddressPwned, new HibpRequestOptions { FilterByDomain = DomainBreached });

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
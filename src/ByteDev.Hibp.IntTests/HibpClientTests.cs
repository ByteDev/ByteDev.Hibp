using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ByteDev.Hibp.IntTests
{
    [TestFixture]
    public class HibpClientTests
    {
        private const string EmailAddressPwned = @"johnsmith@gmail.com";
        private const string EmailAddressNotPwned = @"djs834sskldi4999dspo@gmail.com";

        private HibpClient _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HibpClient();
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
            public async Task WhenEmailAddressPwned_AndFilterByDomain_ThenReturnOnlyDomainBreaches()
            {
                const string domain = "adobe.com";

                var result = await _sut.GetHasBeenPwnedAsync(EmailAddressPwned, new HibpRequestOptions { FilterByDomain = domain });

                Assert.That(result.Breaches.Single().Domain, Is.EqualTo(domain));
            }
        }
    }
}
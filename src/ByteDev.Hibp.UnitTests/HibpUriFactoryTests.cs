using System;
using System.Net;
using System.Net.Mime;
using ByteDev.Hibp.Request;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ByteDev.Hibp.UnitTests
{
    [TestFixture]
    public class HibpUriFactoryTests
    {
        private const string UrlBase = "https://haveibeenpwned.com/api/";

        private const string EmailAddress = "johnsmith@gmail.com";

        private readonly string _emailAddressUrlEncoded = WebUtility.UrlEncode(EmailAddress);

        [TestFixture]
        public class CreateBreachedAccountUri : HibpUriFactoryTests
        {
            private const string ServicePath = "breachedaccount/";

            [Test]
            public void WhenEmailAddressIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => Act(null));
            }

            [Test]
            public void WhenEmailAddressIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => Act(string.Empty));
            }

            [Test]
            public void WhenOptionsIsNull_ThenReturnWithJustEmailAddress()
            {
                var result = Act(EmailAddress);

                Assert.That(result.ToString(), Is.EqualTo(ExpectedUrl()));
            }

            [Test]
            public void WhenAllOptionsEnabled_ThenReturnUri()
            {
                var result = Act(EmailAddress, new HibpRequestOptions {IncludeUnverified = true, TruncateResponse = true, FilterByDomain = "yahoo.com"});

                Assert.That(result.ToString(), Is.EqualTo(ExpectedUrl() + "?truncateResponse=true&includeUnverified=true&domain=yahoo.com"));
            }

            private string ExpectedUrl()
            {
                return $"{UrlBase}{ServicePath}" + _emailAddressUrlEncoded;
            }

            private static Uri Act(string emailAddress, HibpRequestOptions options = null)
            {
                return HibpUriFactory.CreateBreachedAccountUri(emailAddress, options);
            }
        }
    }
}
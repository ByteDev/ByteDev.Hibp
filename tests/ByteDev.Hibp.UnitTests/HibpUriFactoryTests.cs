using System;
using System.Net;
using ByteDev.Hibp.Contract.Request;
using NUnit.Framework;

namespace ByteDev.Hibp.UnitTests
{
    [TestFixture]
    public class HibpUriFactoryTests
    {
        private const string UrlBase = "https://haveibeenpwned.com/api/v3/";

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
            public void WhenAllOptionsSetToNonDefault_ThenReturnUri()
            {
                var result = Act(EmailAddress, new HibpRequestOptions
                {
                    IncludeUnverified = false, 
                    TruncateResponse = false, 
                    FilterByDomain = "yahoo.com"
                });

                Assert.That(result.ToString(), Is.EqualTo(ExpectedUrl() + "?truncateResponse=false&includeUnverified=false&domain=yahoo.com"));
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

        [TestFixture]
        public class CreateBreachedSiteUri : HibpUriFactoryTests
        {
            private const string ServicePath = "breaches";

            [Test]
            public void WhenDomainIsNull_ThenReturnUriWithoutDomainParam()
            {
                var result = HibpUriFactory.CreateBreachedSiteUri(null);

                Assert.That(result.ToString(), Is.EqualTo(ExpectedUrl()));
            }

            [Test]
            public void WhenDomainIsSpecified_ThenReturnUriWithDomain()
            {
                var result = HibpUriFactory.CreateBreachedSiteUri("yahoo.com");

                Assert.That(result.ToString(), Is.EqualTo(ExpectedUrl() + "?domain=yahoo.com"));
            }

            private string ExpectedUrl()
            {
                return $"{UrlBase}{ServicePath}";
            }
        }

        [TestFixture]
        public class CreateBreachSiteByNameUri : HibpUriFactoryTests
        {
            private const string ServicePath = "breach";

            [Test]
            public void WhenBreachNameIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => HibpUriFactory.CreateBreachSiteByNameUri(null));
            }

            [Test]
            public void WhenBreachNameIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => HibpUriFactory.CreateBreachSiteByNameUri(string.Empty));
            }

            [Test]
            public void WhenBreachNameSupplied_ThenReturnUriWithBreachName()
            {
                var result = HibpUriFactory.CreateBreachSiteByNameUri("000webhost");

                Assert.That(result.ToString(), Is.EqualTo(ExpectedUrl() + "/000webhost"));
            }

            private string ExpectedUrl()
            {
                return $"{UrlBase}{ServicePath}";
            }
        }

        [TestFixture]
        public class CreateDataClassesUri : HibpUriFactoryTests
        {
            private const string ServicePath = "dataclasses";

            [Test]
            public void WhenCalled_ThenReturnsUri()
            {
                var result = HibpUriFactory.CreateDataClassesUri();

                Assert.That(result.ToString(), Is.EqualTo($"{UrlBase}{ServicePath}"));
            }
        }

        [TestFixture]
        public class CreateAccountPastesUri : HibpUriFactoryTests
        {
            private const string ServicePath = "pasteaccount/";

            [Test]
            public void WhenEmailAddressIsNull_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => HibpUriFactory.CreateAccountPastesUri(null));
            }

            [Test]
            public void WhenEmailAddressIsEmpty_ThenThrowException()
            {
                Assert.Throws<ArgumentException>(() => HibpUriFactory.CreateAccountPastesUri(string.Empty));
            }

            [Test]
            public void WhenEmailAddressSupplied_ThenReturnUriWithEmailAddress()
            {
                var result = HibpUriFactory.CreateAccountPastesUri(EmailAddress);

                Assert.That(result.ToString(), Is.EqualTo($"{UrlBase}{ServicePath}{_emailAddressUrlEncoded}"));
            }
        }
    }
}
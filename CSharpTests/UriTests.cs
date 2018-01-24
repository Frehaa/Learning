using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpTests
{
    [TestClass]
    public class UriTests
    {
        [TestMethod]
        public void Uri_HttpsScheme_is_https()
        {
            Assert.AreEqual("https", Uri.UriSchemeHttps);
        }

        [TestMethod]
        public void Google_Uri_Is_Https()
        {
            Uri googleUri = new Uri("https://www.google.dk/");

            Assert.AreEqual(Uri.UriSchemeHttps, googleUri.Scheme);
        }

        [TestMethod]
        public void Crunchyroll_Uri_Is_Not_Https()
        {
            Uri crunchyrollUri = new Uri("http://www.crunchyroll.com/");

            Assert.AreNotEqual(Uri.UriSchemeHttps, crunchyrollUri.Scheme);
        }
    }
}

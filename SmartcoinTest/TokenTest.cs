using NUnit.Framework;
using Smartcoin;
using Smartcoin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartcoinTest
{
    [TestFixture]
    public class TokenTest
    {
        public static Token GenerateInstance()
        {
            var c = new Token();
            c.Card = new Card();
            c.Card.AddressCep = "00000-000";
            c.Card.AddressCity = "City";
            c.Card.AddressCountry = "Country";
            c.Card.AddressDistrict = "District";
            c.Card.AddressLine1 = "Line 1";
            c.Card.AddressLine2 = "Line 2";
            c.Card.AddressState = "State";
            c.Card.Country = "BR";
            c.Card.ExpMonth = 12;
            c.Card.ExpYear = 2017;
            c.Card.Name = "NAME SURNAME";
            c.Card.Cvc = 123;
            c.Card.Number = 4242424242424242;
            return c;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            SmartcoinConfiguration.SetKeys(TestConfiguration.API_KEY, TestConfiguration.API_SECRET);
        }

        [Test]
        public void CanCreateToken()
        {
            var c = GenerateInstance();
            var r = c.Create();
            Assert.IsNotEmpty(r.Id);
        }

        [Test]
        public void CanRetrieveToken()
        {
            var c = GenerateInstance();
            var cr = c.Create();

            var r = Token.Get(cr.Id);
            Assert.IsNotEmpty(r.Id);
            Assert.AreEqual(cr.Id, r.Id);
        }
    }
}

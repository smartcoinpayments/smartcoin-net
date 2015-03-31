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
    public class CustomerTest
    {
        public static Customer GenerateInstance()
        {
            var c = new Customer();
            c.Email = "fake_" + TestUtils.RandomString() + "@fakemail.com";
            return c;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            SmartcoinConfiguration.SetKeys(TestConfiguration.API_KEY, TestConfiguration.API_SECRET);
        }

        [Test]
        public void CanCreateCustomer()
        {
            var c = GenerateInstance();
            var c2 = c.Create();
            Assert.AreEqual(c2.Email, c.Email);
            Assert.IsNotEmpty(c2.Id);
        }

        [Test]
        public void CanUpdateCard()
        {
            var c = GenerateInstance();
            var rc = c.Create();
            Token t = TokenTest.GenerateInstance();
            var tc = t.Create();
            var cut = rc.UpdateCard(tc.Id);

            Assert.IsNotEmpty(cut.Cards.Data);
            Customer.Delete(cut.Id);
        }

        [Test]
        public void CanListAllCustomers()
        {
            var c = GenerateInstance();
            var rc = c.Create();
            var l = Customer.ListAll();
            Assert.IsNotEmpty(l.Data);
            Customer.Delete(rc.Id);
        }

        [Test]
        public void CanRetrieveCustomer()
        {
            var c = GenerateInstance();
            var rc = c.Create();
            var r = Customer.Get(rc.Id);
            Assert.AreEqual(rc.Id, r.Id);
            Assert.AreEqual(rc.Email, r.Email);

            Customer.Delete(r.Id);
        }
    }
}

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
    public class ChargeTest
    {
        public static Charge GenerateInstance(Card card = null, Customer customer = null, Token token = null, string type = "bank_slip")
        {
            var c = new Charge();
            Random rd = new Random();
            if (type != "bank_slip")
            {
                c.Type = "credit_card";
                c.Installment = rd.Next(1, 12);
                if (token != null)
                    c.CardToken = token.Id;
                else if (card != null)
                    c.Card = card;
                else if (customer != null)
                    c.Customer = customer.Id;
            }
            else
                c.Type = "bank_slip";

            c.Amount = rd.Next(1, 500);
            c.Currency = "brl";
            c.Description = TestUtils.RandomString();
            return c;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            SmartcoinConfiguration.SetKeys(TestConfiguration.API_KEY, TestConfiguration.API_SECRET);
        }

        [Test]
        public void CanCreateBankSlipCharge()
        {
            var c = GenerateInstance();
            var r = c.Create();
            Assert.IsNotEmpty(r.Id);
        }

        [Test]
        public void CanCreateCustomerCardCharge()
        {
            var cus = CustomerTest.GenerateInstance();
            cus = cus.Create();
            var card = new Card();
            card.Number = 5454545454545454;
            card.ExpMonth = 12;
            card.ExpYear = 2017;
            card.Cvc = 123;
            card.Name = "NAME TESTING CARD";
            cus.UpdateCard(card);

            var c = GenerateInstance(customer: cus, type: "credit_card");
            var r = c.Create();
            Assert.IsNotEmpty(r.Id);
        }

        [Test]
        public void CanCreateCardCharge()
        {
            var card = new Card();
            card.Number = 5454545454545454;
            card.ExpMonth = 12;
            card.ExpYear = 2017;
            card.Cvc = 123;
            card.Name = "NAME TESTING CARD";

            var c = GenerateInstance(type: "credit_card", card: card);
            var r = c.Create();
            Assert.IsNotEmpty(r.Id);
        }


        [Test]
        public void CanCreateTokenCharge()
        {
            var token = TokenTest.GenerateInstance();
            token = token.Create();

            var c = GenerateInstance(type: "credit_card", token: token);
            var r = c.Create();
            Assert.IsNotEmpty(r.Id);
        }


        [Test]
        public void CanCaptureCharge()
        {
            var token = TokenTest.GenerateInstance();
            token = token.Create();
            var c = GenerateInstance(token: token, type: "credit_card");
            c = c.Create();
            var r = c.CaptureNow();
            Assert.AreEqual(true, r.Captured);
        }

        [Test]
        public void CanRefundCharge()
        {
            var token = TokenTest.GenerateInstance();
            token = token.Create();
            var c = GenerateInstance(token: token, type: "credit_card");
            c = c.Create();
            var r = c.Refund();
            Assert.AreEqual(true, r.Refunded);
        }

        [Test]
        public void CanUpdateDescriptionCharge()
        {
            var c = GenerateInstance();
            var cr = c.Create();
            cr.Description = TestUtils.RandomString(cr.Description);
            var r = cr.UpdateDescription();
            Assert.AreNotEqual(c.Description, r.Description);
        }

        [Test]
        public void CanRetrieveCharge()
        {
            var c = GenerateInstance();
            var cr = c.Create();

            var r = Charge.Get(cr.Id);
            Assert.IsNotEmpty(r.Id);
            Assert.AreEqual(cr.Id, r.Id);
        }

        [Test]
        public void CanListAllCharges()
        {
            var charges = Charge.ListAll();
            Assert.Greater(charges.TotalCount, 0);
        }

        [Test]
        public void CanPaginateChargesList()
        {
            var charges = Charge.ListAll(5, 0);
            Assert.AreEqual(charges.Data.Count, 5);
            Assert.Greater(charges.TotalCount, 0);
        }

        [Test]
        public void CanQueryChargesList()
        {
            var charges = Charge.ListAll(5, 0, "<", new DateTime(2015, 5, 24));
            Assert.GreaterOrEqual(charges.Data.Count, 1);
        }
    }
}

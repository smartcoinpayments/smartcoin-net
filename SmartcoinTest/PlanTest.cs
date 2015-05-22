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
    public class PlanTest
    {
        public static Plan GenerateInstance()
        {
            var c = new Plan();
            c.Id = TestUtils.RandomString();
            c.Amount = 100;
            c.Currency = "brl";
            c.Interval = "week";
            c.IntervalCount = 2;
            c.Name = "Fake Plan " + c.Id;
            c.TrialPeriodDays = 15;
            return c;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            SmartcoinConfiguration.SetKeys(TestConfiguration.API_KEY, TestConfiguration.API_SECRET);
        }

        [Test]
        public void CanCreatePlan()
        {
            var c = GenerateInstance();
            var r = c.Create();
            Assert.IsNotEmpty(r.Id);
            Assert.AreEqual(c.Id, r.Id);
            Plan.Delete(r.Id);
        }

        [Test]
        public void CanUpdatePlan()
        {
            var c = GenerateInstance();

            var r = c.Create();
            r.Name = "Fake Plan " + TestUtils.RandomString();
            var u = r.Update();

            Assert.AreEqual(r.Id, u.Id);
            Assert.AreNotEqual(c.Name, u.Name);

            Plan.Delete(u.Id);
        }

        [Test]
        public void CanListAllPlans()
        {
            var c = Plan.ListAll();
            Assert.IsNotEmpty(c.Data);
        }

        [Test]
        public void CanDeletePlan()
        {
            var c = GenerateInstance();
            var r = c.Create();

            Assert.IsNotEmpty(r.Id);
            var d = Plan.Delete(r.Id);
            Assert.AreEqual(d, true);

        }

        [Test]
        public void CanRetrievePlan()
        {
            var c = GenerateInstance();
            var cr = c.Create();

            var r = Plan.Get(cr.Id);
            Assert.IsNotEmpty(r.Id);
            Assert.AreEqual(cr.Id, r.Id);
        }
    }
}

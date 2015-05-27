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
    public class SubscriptionTest
    {
        public static Subscription GenerateInstance(Plan p, Customer c)
        {
            var ci = new Subscription();
            ci.Customer = c.Id;
            ci.Plan = p;
            ci.TrialEnd = 7;
            ci.Quantity = 2;

            return ci;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            SmartcoinConfiguration.SetKeys(TestConfiguration.API_KEY, TestConfiguration.API_SECRET);
        }

        [Test]
        public void CanCreateSubscription()
        {
            Plan plan = PlanTest.GenerateInstance();
            plan = plan.Create();

            Customer cus = CustomerTest.GenerateInstance();
            cus = cus.Create();

            var ci = GenerateInstance(plan, cus);
            var cg = ci.Create();

            Assert.IsNotEmpty(cg.Id);
            Assert.AreEqual(plan.Id, cg.Plan.Id);
        }

        [Test]
        public void CanRetrieveSubscription()
        {

            Plan plan = PlanTest.GenerateInstance();
            plan = plan.Create();

            Customer cus = CustomerTest.GenerateInstance();
            cus = cus.Create();

            var ci = GenerateInstance(plan, cus);
            var cg = ci.Create();

            var r = Subscription.Get(cg.Customer, cg.Id);

            Assert.IsNotEmpty(r.Id);
            Assert.AreEqual(cg.Id, r.Id);
        }

        [Test]
        public void CanCancelSubscription()
        {

            Plan plan = PlanTest.GenerateInstance();
            plan = plan.Create();

            Customer cus = CustomerTest.GenerateInstance();
            cus = cus.Create();

            var ci = GenerateInstance(plan, cus);
            var cg = ci.Create();

            var r = Subscription.Cancel(cg);

            Assert.AreEqual(cg.Id, r.Id);
            Assert.AreEqual("canceled", r.Status);
        }

        [Test]
        public void CanListSubscriptions()
        {
            var subscriptions = Subscription.ListAll();
            Assert.GreaterOrEqual(subscriptions.TotalCount, 0);
        }
    }
}

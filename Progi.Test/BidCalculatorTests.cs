using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Progi.BusinessRules;

namespace Progi.Test
{
    public class MockBidCalculator : BidCalculator
    {
        public BidResultModel CalculateTotal(float bid, BidConfigModel config)
        {
            return CalculateTotalFromBid(bid, config);
        }
    }

    [TestClass]
    public class BidCalculatorTests
    {
        [TestMethod]
        public void TotalFromBidTest()
        {
            var config = BidConfigModel.Default;
            var total = new MockBidCalculator().CalculateTotal(1000, config).RoundedTotal;
            Assert.AreEqual(1180, total);

            total = new MockBidCalculator().CalculateTotal(50, config).RoundedTotal;
            Assert.AreEqual(50 + 10 + .02 * 50 + 5 + 100, total);

            total = new MockBidCalculator().CalculateTotal(501, config).RoundedTotal;
            Assert.AreEqual(501 + 50 + .02 * 501 + 10 + 100, total);
        }

        [TestMethod]
        public void BidFromTotalTest()
        {
            var config = BidConfigModel.Default;
            var bid = new MockBidCalculator().CalculateBidFromTotalCost(1180, config).ToRounded();
            Assert.AreEqual(1000, bid.Bid);

            bid = new MockBidCalculator().CalculateBidFromTotalCost(50 + 10 + .02 * 50 + 5 + 100, config).ToRounded();
            Assert.AreEqual(50D, bid.Bid);

            bid = new MockBidCalculator().CalculateBidFromTotalCost(501 + 50 + .02 * 501 + 10 + 100, config).ToRounded();
            Assert.AreEqual(501D, bid.Bid);

            bid = new MockBidCalculator().CalculateBidFromTotalCost(10000 + 50 + .02 * 10000 + 20 + 100, config);
            var rounded = bid.ToRounded();
            Assert.AreEqual(10000D, rounded.Bid);

            bid = new MockBidCalculator().CalculateBidFromTotalCost(101, config);
            Assert.AreEqual(null, bid, bid == null ? "" : bid.RoundedTotal.ToString());
        }
    }
}

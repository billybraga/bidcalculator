using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Progi.BusinessRules
{
    /// <summary>
    /// Encapsulate bid calculations
    /// </summary>
    public class BidCalculator
    {
        private static Lazy<BidCalculator> instanceLazy = new Lazy<BidCalculator>(() => new BidCalculator());

        public static BidCalculator Instance { get { return instanceLazy.Value; } }

        protected BidCalculator() { }

        /// <summary>
        /// Use a numeric method to narrow down the bid from the total cost. 
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public BidResultModel CalculateBidFromTotalCost(double totalCost, BidConfigModel config)
        {
            // Remove storage fee that we can predict
            var currentBid = totalCost - config.StorageFee;
            BidResultModel totalCostForCurrentBid;
            double minDelta = double.MaxValue;
            do
            {
                if (currentBid <= 1E-4)
                {
                    return null;
                }

                // calculate total cost from current bid approximation
                totalCostForCurrentBid = CalculateTotalFromBid(currentBid, config);
                var newBid = currentBid * totalCost / totalCostForCurrentBid.Total;
                var delta = Math.Abs(newBid - currentBid);

                if (delta > minDelta)
                {
                    return null;
                }

                minDelta = delta;

                // adjust current bid approximation on it's total cost result calculation
                currentBid = newBid;
            }
            // continue until we are close enough
            while (Math.Abs(totalCost - totalCostForCurrentBid.Total) > 1E-4);
            
            return totalCostForCurrentBid;
        }

        /// <summary>
        /// Calculate total cost from bid using a specified config
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        protected BidResultModel CalculateTotalFromBid(double bid, BidConfigModel config)
        {
            return new BidResultModel
            {
                Bid = bid,
                UserFee = 
                    Math.Min(
                        config.UserFeeMaximum,
                        Math.Max(
                            config.UserFeeMinimum,
                            bid * config.UserFeePercentage / 100)),
                SalesFee = bid * config.SalesFeePercentage / 100,
                AssociationFee = config
                    .AssociationBrackets
                    .Where(x => (x.Minimum == null || x.Minimum <= bid)
                        && (x.Maximum == null || x.Maximum > bid))
                    .Single()
                    .Fee,
                StorageFee = config.StorageFee,
            };
        }
    }
}
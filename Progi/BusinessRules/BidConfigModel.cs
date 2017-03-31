using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Progi.BusinessRules
{
    public class BidConfigModel
    {
        public double UserFeePercentage { get; set; }
        public double UserFeeMinimum { get; set; }
        public double UserFeeMaximum { get; set; }
        public double SalesFeePercentage { get; set; }
        public BigConfigAssocationBracketModel[] AssociationBrackets { get; set; }
        public double StorageFee { get; set; }

        public static readonly BidConfigModel Default = new BidConfigModel
        {
            UserFeePercentage = 10,
            UserFeeMinimum = 10,
            UserFeeMaximum = 50,
            SalesFeePercentage = 2,
            AssociationBrackets = new[]
            {
                new BigConfigAssocationBracketModel
                {
                    Minimum = null,
                    Maximum = 501,
                    Fee = 5
                },
                new BigConfigAssocationBracketModel
                {
                    Minimum = 501,
                    Maximum = 1001,
                    Fee = 10
                },
                new BigConfigAssocationBracketModel
                {
                    Minimum = 1001,
                    Maximum = 3000 + double.Epsilon,
                    Fee = 15
                },
                new BigConfigAssocationBracketModel
                {
                    Minimum = 3000 + double.Epsilon,
                    Maximum = null,
                    Fee = 20
                },
            },
            StorageFee = 100
        };
    }
}
using Newtonsoft.Json;
using System;

namespace Progi.BusinessRules
{
    public class BidResultModel
    {
        [JsonProperty("bid")]
        public double Bid { get; set; }
        [JsonProperty("userFee")]
        public double UserFee { get; set; }
        [JsonProperty("salesFee")]
        public double SalesFee { get; set; }
        [JsonProperty("assocationFee")]
        public double AssociationFee { get; set; }
        [JsonProperty("storageFee")]
        public double StorageFee { get; set; }
        [JsonIgnore]
        public double Total
        {
            get
            {
                return Bid + UserFee + SalesFee + AssociationFee + StorageFee;
            }
        }
        [JsonIgnore]
        public double RoundedTotal
        {
            get
            {
                return Math.Round(Total, 2);
            }
        }

        public BidResultModel ToRounded()
        {
            return new BidResultModel
            {
                Bid = Math.Round(Bid, 2),
                UserFee = Math.Round(UserFee, 2),
                SalesFee = Math.Round(SalesFee, 2),
                AssociationFee = Math.Round(AssociationFee, 2),
                StorageFee = Math.Round(StorageFee, 2),
            };
        }
    }
}
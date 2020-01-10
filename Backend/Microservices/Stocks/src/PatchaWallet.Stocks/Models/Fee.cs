using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatchaWallet.Stocks
{
    public class Fee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Coin Coin { get; set; }
        public decimal MinDeposit { get; set; }
        public decimal DepositFee { get; set; }
        public decimal MinWithDrawal { get; set; }
        public decimal WithDrawalComissionPercent { get; set; }
        public decimal WithDrawalComissionValueInBRL { get; set; }
        public decimal BtcWithDrawal { get; set; }
        public decimal BtcDeposit { get; set; }
    }
}

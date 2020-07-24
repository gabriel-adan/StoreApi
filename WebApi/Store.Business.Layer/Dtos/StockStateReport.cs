using System;

namespace Store.Business.Layer.Dtos
{
    public class StockStateReport : ItemReport
    {
        public Int64 Orders { get; set; }
        public decimal Sales { get; set; }
        public decimal Credits { get; set; }
        public double CreditPrice { get; set; }
    }
}

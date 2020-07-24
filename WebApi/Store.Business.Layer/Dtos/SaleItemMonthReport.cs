using System;

namespace Store.Business.Layer.Dtos
{
    public class SaleItemMonthReport : ItemReport
    {
        public Int64 Amount { get; set; }
        public double SubTotal { get; set; }
        public double Rentability { get; set; }
    }
}

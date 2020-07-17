using System;

namespace Store.Business.Layer.Dtos
{
    public class SaleItemMonthReport : ItemReport
    {
        public virtual Int64 Amount { get; set; }
        public virtual double SubTotal { get; set; }
        public virtual double Rentability { get; set; }
    }
}

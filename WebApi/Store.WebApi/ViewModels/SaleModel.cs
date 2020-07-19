using System;
using System.Collections.Generic;

namespace Store.WebApi.ViewModels
{
    public class SaleModel
    {
        public DateTime? Date { get; set; }
        public IList<int> OrderDetailIds { get; set; }
        public IList<float> UnitPrices { get; set; }

        public SaleModel()
        {
            OrderDetailIds = new List<int>();
            UnitPrices = new List<float>();
        }
    }
}

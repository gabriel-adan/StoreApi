using System;
using System.Collections.Generic;

namespace Store.WebApi.ViewModels
{
    public class SaleModel
    {
        public DateTime? Date { get; set; }
        public string UserName { get; set; }
        public IList<int> OrderDetailIds { get; set; }
        public IList<float> UnitPrices { get; set; }
        public IList<int> Amounts { get; set; }

        public SaleModel()
        {
            OrderDetailIds = new List<int>();
            UnitPrices = new List<float>();
            Amounts = new List<int>();
        }
    }
}

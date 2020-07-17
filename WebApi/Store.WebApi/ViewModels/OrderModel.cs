using System;
using System.Collections.Generic;

namespace Store.WebApi.ViewModels
{
    public class OrderModel
    {
        public DateTime? Date { get; set; }
        public string TicketCode { get; set; }
        public IList<int> ProductIds { get; set; }
        public IList<float> UnitCosts { get; set; }
        public IList<int> Amounts { get; set; }

        public OrderModel()
        {
            ProductIds = new List<int>();
            UnitCosts = new List<float>();
            Amounts = new List<int>();
        }
    }
}

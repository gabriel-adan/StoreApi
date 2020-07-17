using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;

namespace Store.Business.Layer
{
    public class Order : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual string TicketCode { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public virtual IList<OrderDetail> OrderDetails { get; set; }
    }
}

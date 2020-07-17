using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;

namespace Store.Business.Layer
{
    public class Sale : Entity
    {
        public virtual DateTime Date { get; set; }

        public Sale()
        {
            SaleDetails = new List<SaleDetail>();
        }

        public virtual User User { get; set; }
        public virtual IList<SaleDetail> SaleDetails { get; set; }
    }
}

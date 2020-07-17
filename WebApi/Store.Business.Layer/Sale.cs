using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;

namespace Store.Business.Layer
{
    public class Sale : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual string UserName { get; set; }

        public Sale()
        {
            SaleDetails = new List<SaleDetail>();
        }

        public virtual IList<SaleDetail> SaleDetails { get; set; }
    }
}

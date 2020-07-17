using SharpArch.Domain.DomainModel;
using System;
using System.Collections.Generic;

namespace Store.Business.Layer
{
    public class Credit : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual string UserName { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}

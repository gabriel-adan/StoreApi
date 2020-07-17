using System;
using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class Payment : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual float Amount { get; set; }
        public virtual string Description { get; set; }

        public virtual Credit Credit { get; set; }
    }
}

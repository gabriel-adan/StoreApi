using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class CreditDetail : Entity
    {
        public virtual float UnitPrice { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
        public virtual Credit Credit { get; set; }
    }
}

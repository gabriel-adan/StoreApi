using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class SaleDetail : Entity
    {
        public virtual float UnitPrice { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
        public virtual Sale Sale { get; set; }
    }
}

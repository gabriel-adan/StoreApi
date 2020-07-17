using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class OrderDetail : Entity
    {
        public virtual float UnitCost { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}

using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class Product : Entity
    {
        public virtual string Code { get; set; }
        public virtual float Price { get; set; }
        public virtual string Brand { get; set; }

        public virtual Specification Specification { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }
    }
}

using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class Specification : Entity
    {
        public virtual string Description { get; set; }
        public virtual string Detail { get; set; }

        public virtual Category Category { get; set; }
    }
}

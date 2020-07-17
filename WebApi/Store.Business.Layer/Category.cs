using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class Category : Entity
    {
        public virtual string Name { get; set; }
    }
}

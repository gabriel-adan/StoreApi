using SharpArch.Domain.DomainModel;

namespace Store.Business.Layer
{
    public class User : Entity
    {
        public virtual string UserName { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Password { get; set; }
    }
}

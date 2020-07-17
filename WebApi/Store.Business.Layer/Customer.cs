using SharpArch.Domain.DomainModel;
using System.Collections.Generic;

namespace Store.Business.Layer
{
    public class Customer : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        public virtual bool IsEnabled { get; set; }

        public virtual IList<Credit> Credits { get; set; }
    }
}

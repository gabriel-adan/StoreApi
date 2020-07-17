using System;
using System.Collections.Generic;

namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface ISaleRepository : IRepository<Sale>
    {
        IList<Sale> GetList(DateTime date);
    }
}

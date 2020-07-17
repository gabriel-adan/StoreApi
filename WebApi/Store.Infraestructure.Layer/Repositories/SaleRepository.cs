using NHibernate;
using NHibernate.Criterion;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace Store.Infraestructure.Layer.Repositories
{
    public class SaleRepository : Repository<Sale>, ISaleRepository
    {
        public SaleRepository(ISession session) : base(session)
        {

        }

        public IList<Sale> GetList(DateTime date)
        {
            try
            {
                return Session.CreateCriteria<Sale>()
                    .Add(Restrictions.Where<Sale>(s => s.Date == date))
                    .List<Sale>();
            }
            catch
            {
                throw;
            }
        }
    }
}

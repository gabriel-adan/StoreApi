using NHibernate;
using NHibernate.Criterion;
using Store.Business.Layer.RepositoryInterfaces;

namespace Store.Infraestructure.Layer.Repositories
{
    public class ItemRepository<T> : Repository<T>, IItemRepository<T> where T : class
    {
        public ItemRepository(ISession session) : base(session)
        {

        }

        public T FindByName(string name)
        {
            try
            {
                return Session.CreateCriteria<T>()
                    .Add(Restrictions.Eq("Name", name)).UniqueResult<T>();
            }
            catch
            {
                throw;
            }
        }
    }
}

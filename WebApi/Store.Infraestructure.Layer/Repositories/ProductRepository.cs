using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;

namespace Store.Infraestructure.Layer.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ISession session) : base (session)
        {

        }

        public Product Find(string code, int colorId, int sizeId, int specificationId)
        {
            try
            {
                return Session.CreateCriteria<Product>()
                    .Add(Restrictions.Where<Product>(
                        p => p.Code == code &&
                        p.Color.Id == colorId &&
                        p.Size.Id == sizeId &&
                        p.Specification.Id == specificationId)
                        ).UniqueResult<Product>();
            }
            catch
            {
                throw;
            }
        }

        public IList<Product> Find(string code)
        {
            try
            {
                return Session.CreateCriteria<Product>()
                    .Add(Restrictions.Where<Product>(p => p.Code == code)).List<Product>();
            }
            catch
            {
                throw;
            }
        }

        public IList<Specification> FindSpecifications(string description)
        {
            try
            {
                return Session.CreateCriteria<Specification>()
                    .Add(Restrictions.InsensitiveLike("Description", description, MatchMode.Anywhere)).List<Specification>();
            }
            catch
            {
                throw;
            }
        }
    }
}

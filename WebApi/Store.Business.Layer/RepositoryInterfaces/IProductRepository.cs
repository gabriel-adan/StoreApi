using System.Collections.Generic;

namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IList<Product> Find(string code);

        Product Find(string code, int colorId, int sizeId, int specificationId);

        IList<Specification> FindSpecifications(string description);
    }
}

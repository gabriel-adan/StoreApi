using System.Collections.Generic;

namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IList<OrderDetail> Find(int productId, int amount);
    }
}

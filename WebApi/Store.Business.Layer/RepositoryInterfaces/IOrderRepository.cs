using System.Collections.Generic;

namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        OrderDetail Find(string code, int colorId, int sizeId);

        IList<OrderDetail> Find(string code);

        IList<OrderDetail> Find(string code, int colorId, int sizeId, int amount);
    }
}

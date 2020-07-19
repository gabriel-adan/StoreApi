using NHibernate;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;
using System.Collections.Generic;

namespace Store.Infraestructure.Layer.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ISession session) : base(session)
        {

        }

        public IList<OrderDetail> Find(int productId, int amount)
        {
            try
            {
                var query = Session.CreateSQLQuery("SELECT od.Id, od.UnitCost, od.Product_Id, od.Orders_Id FROM OrderDetail od INNER JOIN Product p ON p.Id = od.Product_Id LEFT JOIN SaleDetail sd ON sd.OrderDetail_Id = od.Id LEFT JOIN CreditDetail cd ON cd.OrderDetail_Id = od.Id WHERE sd.Id IS NULL AND cd.Id IS NULL AND p.Id = :productId LIMIT :amount;")
                    .AddEntity(typeof(OrderDetail));
                query.SetInt32("productId", productId);
                query.SetInt32("amount", amount);
                return query.List<OrderDetail>();
            }
            catch
            {
                throw;
            }
        }
    }
}

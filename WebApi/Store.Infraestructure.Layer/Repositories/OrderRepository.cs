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

        public OrderDetail Find(string code, int colorId, int sizeId)
        {
            try
            {
                var query = Session.CreateSQLQuery("SELECT od.Id, od.UnitCost, od.Product_Id, od.Orders_Id, p.Id, p.Code, p.Price, p.Color_Id, p.Size_Id, p.Specification_Id FROM orderdetail od INNER JOIN product p ON p.Id = od.Product_Id LEFT JOIN saledetail sd ON sd.OrderDetail_Id = od.Id LEFT JOIN creditdetail cd ON cd.OrderDetail_Id = od.Id WHERE sd.Id IS NULL AND cd.Id IS NULL AND p.Code = :code AND p.Color_Id = :colorId AND p.Size_Id = :sizeId LIMIT 1;")
                    .AddEntity(typeof(OrderDetail));
                query.SetString("code", code);
                query.SetInt32("colorId", colorId);
                query.SetInt32("sizeId", sizeId);
                return query.UniqueResult<OrderDetail>();
            }
            catch
            {
                throw;
            }
        }

        public IList<OrderDetail> Find(string code)
        {
            try
            {
                var query = Session.CreateSQLQuery("SELECT od.Id, od.UnitCost, od.Product_Id, od.Orders_Id FROM orderdetail od INNER JOIN product p ON p.Id = od.Product_Id LEFT JOIN saledetail sd ON sd.OrderDetail_Id = od.Id LEFT JOIN creditdetail cd ON cd.OrderDetail_Id = od.Id WHERE sd.Id IS NULL AND cd.Id IS NULL AND p.Code = :code GROUP BY p.Code, p.Color_Id, p.Size_Id;")
                    .AddEntity(typeof(OrderDetail));
                query.SetString("code", code);
                return query.List<OrderDetail>();
            }
            catch
            {
                throw;
            }
        }

        public IList<OrderDetail> Find(string code, int colorId, int sizeId, int amount)
        {
            try
            {
                var query = Session.CreateSQLQuery("SELECT od.Id, od.UnitCost, od.Product_Id, od.Orders_Id, p.Id, p.Code, p.Price, p.Color_Id, p.Size_Id, p.Specification_Id FROM orderdetail od INNER JOIN product p ON p.Id = od.Product_Id LEFT JOIN saledetail sd ON sd.OrderDetail_Id = od.Id LEFT JOIN creditdetail cd ON cd.OrderDetail_Id = od.Id WHERE sd.Id IS NULL AND cd.Id IS NULL AND p.Code = :code AND p.Color_Id = :colorId AND p.Size_Id = :sizeId LIMIT :amount;")
                    .AddEntity(typeof(OrderDetail));
                query.SetString("code", code);
                query.SetInt32("colorId", colorId);
                query.SetInt32("sizeId", sizeId);
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

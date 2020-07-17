using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Store.Business.Layer;

namespace Store.Infraestructure.Layer.NHibernateMaps
{
    public class OrderMap : IAutoMappingOverride<Order>
    {
        public void Override(AutoMapping<Order> mapping)
        {
            mapping.Table("Orders");
            mapping.HasMany(x => x.OrderDetails).Table("OrderDetail").KeyColumn("Orders_Id").Cascade.All().Inverse();
        }
    }
}

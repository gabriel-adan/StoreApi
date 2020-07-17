using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Store.Business.Layer;

namespace Store.Infraestructure.Layer.NHibernateMaps
{
    public class OrderDetailMap : IAutoMappingOverride<OrderDetail>
    {
        public void Override(AutoMapping<OrderDetail> mapping)
        {
            mapping.References(x => x.Order, "Orders_Id");
            mapping.References(x => x.Product, "Product_Id");
        }
    }
}

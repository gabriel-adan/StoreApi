using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Store.Business.Layer;

namespace Store.Infraestructure.Layer.NHibernateMaps
{
    public class SaleDetailMap : IAutoMappingOverride<SaleDetail>
    {
        public void Override(AutoMapping<SaleDetail> mapping)
        {
            mapping.References(x => x.OrderDetail, "OrderDetail_Id");
        }
    }
}

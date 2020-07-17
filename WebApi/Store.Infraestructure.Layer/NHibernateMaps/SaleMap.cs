using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Store.Business.Layer;

namespace Store.Infraestructure.Layer.NHibernateMaps
{
    public class SaleMap : IAutoMappingOverride<Sale>
    {
        public void Override(AutoMapping<Sale> mapping)
        {
            mapping.HasMany<SaleDetail>(x => x.SaleDetails).Table("SaleDetail").KeyColumn("Sale_Id").Cascade.All().Inverse();
        }
    }
}

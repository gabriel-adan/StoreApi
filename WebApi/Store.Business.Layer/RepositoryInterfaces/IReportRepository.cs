using System;
using System.Collections.Generic;
using Store.Business.Layer.Dtos;

namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IReportRepository : IRepository<ItemReport>
    {
        IList<SaleItemMonthReport> GetSaleMonthReport(DateTime date);

        IList<StockStateReport> GetStockStateReport();

        IList<PaymedItemReport> GetPaymedItemReport(DateTime date);

        CreditAmountReport GetCreditAmountReport(int creditId);

        IList<SaleReport> GetSaleRentabilityReport(DateTime date);

        IList<ProductStockReport> GetProductStockReport();
    }
}

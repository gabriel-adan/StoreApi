using System;
using System.Collections.Generic;
using Store.Business.Layer.Dtos;

namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IReportRepository : IRepository<ItemReport>
    {
        IList<SaleItemMonthReport> GetSaleMonthReport(DateTime date);

        IList<StockStateReport> GetStockStateReport();
    }
}

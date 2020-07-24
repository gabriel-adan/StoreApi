using Store.Business.Layer.Dtos;
using System;
using System.Collections.Generic;

namespace Store.Logic.Layer.Contracts
{
    public interface IReportLogic
    {
        IList<SaleItemMonthReport> GetSalesByMonth(DateTime date);

        IList<StockStateReport> GetStockStateReport();

        double GetMonthRentabilityReport(DateTime month);

        IList<ProductStockReport> GetProductStockReport();
    }
}

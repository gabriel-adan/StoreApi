using Store.Business.Layer.Dtos;
using Store.Business.Layer.RepositoryInterfaces;
using Store.Logic.Layer.Contracts;
using System;
using System.Collections.Generic;

namespace Store.Logic.Layer.Common
{
    public class ReportLogic : IReportLogic
    {
        private readonly IReportRepository reportRepository;

        public ReportLogic(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public IList<SaleItemMonthReport> GetSalesByMonth(DateTime date)
        {
            try
            {
                if (date == DateTime.MinValue)
                    throw new Exception("Fecha inválida");
                return reportRepository.GetSaleMonthReport(date);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<StockStateReport> GetStockStateReport()
        {
            try
            {
                return reportRepository.GetStockStateReport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

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

        public double GetMonthRentabilityReport(DateTime month)
        {
            double total = 0;
            try
            {
                IList<PaymedItemReport> paymedItemReports = reportRepository.GetPaymedItemReport(month);
                foreach (PaymedItemReport paymedItem in paymedItemReports)
                {
                    CreditAmountReport creditAmountReport = reportRepository.GetCreditAmountReport(paymedItem.CreditId);
                    if (paymedItem.Paymed > creditAmountReport.Cost)
                        total += paymedItem.Paymed - creditAmountReport.Cost;
                }

                IList<SaleReport> saleReports = reportRepository.GetSaleRentabilityReport(month);
                foreach (SaleReport saleReport in saleReports)
                {
                    total += saleReport.Total;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return total;
        }

        public IList<ProductStockReport> GetProductStockReport()
        {
            try
            {
                return reportRepository.GetProductStockReport();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

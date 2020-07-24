using NHibernate;
using NHibernate.Transform;
using Store.Business.Layer.Dtos;
using Store.Business.Layer.RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace Store.Infraestructure.Layer.Repositories
{
    public class ReportRepository : Repository<ItemReport>, IReportRepository
    {
        public ReportRepository(ISession session) : base (session)
        {

        }

        public IList<SaleItemMonthReport> GetSaleMonthReport(DateTime date)
        {
            try
            {
                var query = Session.CreateSQLQuery("CALL SP_SALE_MONTH_REPORT(:date);").SetResultTransformer(Transformers.AliasToBean<SaleItemMonthReport>());
                query.SetDateTime("date", date);
                return query.List<SaleItemMonthReport>();
            }
            catch
            {
                throw;
            }
        }

        public IList<StockStateReport> GetStockStateReport()
        {
            try
            {
                var query = Session.CreateSQLQuery("SELECT Code, Description, Detail, Brand, Color, Size, Orders, Sales, Credits, UnitCost, UnitPrice, CreditPrice FROM STOCK_STATE_REPORT_VIEW;").SetResultTransformer(Transformers.AliasToBean<StockStateReport>());
                return query.List<StockStateReport>();
            }
            catch
            {
                throw;
            }
        }

        public IList<PaymedItemReport> GetPaymedItemReport(DateTime date)
        {
            try
            {
                var query = Session.CreateSQLQuery("CALL SP_PAYMED_AMOUNT_BY_MONTH_REPORT(:date);").SetResultTransformer(Transformers.AliasToBean<PaymedItemReport>());
                query.SetDateTime("date", date);
                return query.List<PaymedItemReport>();
            }
            catch
            {
                throw;
            }
        }

        public CreditAmountReport GetCreditAmountReport(int creditId)
        {
            try
            {
                var query = Session.CreateSQLQuery("CALL SP_CREDIT_AMOUNT_REPORT(:creditId);").SetResultTransformer(Transformers.AliasToBean<CreditAmountReport>());
                query.SetInt32("creditId", creditId);
                return query.UniqueResult<CreditAmountReport>();
            }
            catch
            {
                throw;
            }
        }

        public IList<SaleReport> GetSaleRentabilityReport(DateTime date)
        {
            try
            {
                var query = Session.CreateSQLQuery("SELECT Date, SUM(Rentability) AS Total from SALE_RENTABILITY_REPORT_VIEW WHERE MONTH(Date) = MONTH(:date) GROUP BY Date;").SetResultTransformer(Transformers.AliasToBean<SaleReport>());
                query.SetDateTime("date", date);
                return query.List<SaleReport>();
            }
            catch
            {
                throw;
            }
        }

        public IList<ProductStockReport> GetProductStockReport()
        {
            try
            {
                var query = Session.CreateSQLQuery("SELECT v.Code, v.Description, v.Detail, v.Brand, v.Size, v.Color, v.Category, (v.Orders - v.Sales - v.Credits) AS Stock FROM GLOBAL_STOCK_AMOUNT_REPORT_VIEW v;").SetResultTransformer(Transformers.AliasToBean<ProductStockReport>());
                return query.List<ProductStockReport>();
            }
            catch
            {
                throw;
            }
        }
    }
}

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
                var query = Session.CreateSQLQuery("SELECT Code, Description, Mark, Type, Color, Size, UnitCost, UnitPrice, Orders, Sales FROM stock_state_report_view;").SetResultTransformer(Transformers.AliasToBean<StockStateReport>());
                return query.List<StockStateReport>();
            }
            catch
            {
                throw;
            }
        }
    }
}

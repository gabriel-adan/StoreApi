using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store.Logic.Layer.Contracts;
using Store.WebApi.Reports;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportLogic reportLogic;

        public ReportsController(IReportLogic reportLogic) : base ()
        {
            this.reportLogic = reportLogic;
        }

        [HttpGet("SaleMonthReport/{date}")]
        public IActionResult GetSaleMonthReport(DateTime date)
        {
            try
            {
                var list = reportLogic.GetSalesByMonth(date);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("StockStateReport")]
        public IActionResult StockStateReport()
        {
            try
            {
                var list = reportLogic.GetStockStateReport();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = "Error al generar el reporte."});
            }
        }

        [HttpGet("MonthRentabilityReport/{date}")]
        public IActionResult MonthRentabilityReport(DateTime date)
        {
            try
            {
                double result = reportLogic.GetMonthRentabilityReport(date);
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = "Error al generar el reporte." });
            }
        }

        [HttpGet("ProductStockReport")]
        public IActionResult ProductStockReport()
        {
            try
            {
                var list = reportLogic.GetProductStockReport();
                byte[] result = StockReport.Generate(list);

                return File(result, "application/pdf", "Stock-" + DateTime.Now.ToString("dd-MM-yyyy HH:mm tt") + ".pdf");
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = "Error al generar el reporte." });
            }
        }
    }
}
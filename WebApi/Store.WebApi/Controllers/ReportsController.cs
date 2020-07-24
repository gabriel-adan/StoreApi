using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Store.Logic.Layer.Contracts;
using Store.WebApi.Reports;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IReportLogic reportLogic;

        public ReportsController(ILogger<ReportsController> logger, IReportLogic reportLogic) : base ()
        {
            this.logger = logger;
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
            catch (ArgumentException ae)
            {
                return NotFound(new { ErrorMessage = ae.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error.");
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
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error.");
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
            catch (ArgumentException ae)
            {
                return NotFound(new { ErrorMessage = ae.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error.");
            }
        }

        [HttpGet("ProductStockReport")]
        public IActionResult ProductStockReport()
        {
            try
            {
                var list = reportLogic.GetProductStockReport();
                if (list.Count > 0)
                {
                    byte[] result = StockReport.Generate(list);
                    return File(result, "application/pdf", "Stock-" + DateTime.Now.ToString("dd-MM-yyyy HH:mm tt") + ".pdf");
                }
                else
                {
                    return NotFound(new { ErrorMessage = "No hay datos para generar el reporte." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al generar el reporte.");
            }
        }
    }
}
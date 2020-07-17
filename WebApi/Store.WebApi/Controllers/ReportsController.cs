using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store.Logic.Layer.Contracts;

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
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
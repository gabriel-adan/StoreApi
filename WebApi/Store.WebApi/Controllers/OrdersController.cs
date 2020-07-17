using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Logic.Layer.Contracts;
using Store.WebApi.ViewModels;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderLogic orderLogic;

        public OrdersController(IOrderLogic orderLogic) : base ()
        {
            this.orderLogic = orderLogic;
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderLogic.Register(model.Date.Value, model.TicketCode, model.ProductIds, model.UnitCosts, model.Amounts);
                    return Ok(new { Result = "Factura registrada exitosamente." });
                }
                return NotFound(new { ErrorMessage = "Datos inválidos..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Logic.Layer.Contracts;
using Store.WebApi.ViewModels;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IOrderLogic orderLogic;

        public OrdersController(ILogger<OrdersController> logger, IOrderLogic orderLogic) : base ()
        {
            this.logger = logger;
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
            catch (ArgumentException ae)
            {
                return NotFound(new { ErrorMessage = ae.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al intentar registrar los datos de la factura.");
            }
        }

        [HttpGet("Stock/{productId}/{amount}")]
        public ActionResult Find(int productId, int amount)
        {
            try
            {
                var result = orderLogic.Find(productId, amount);
                var query = from orderDetail in result
                            select new
                            {
                                Id = orderDetail.Id,
                                UnitCost = orderDetail.UnitCost,
                                Product = new
                                {
                                    Id = orderDetail.Product.Id,
                                    Code = orderDetail.Product.Code,
                                    Price = orderDetail.Product.Price,
                                    Brand = orderDetail.Product.Brand,
                                    Color = new {
                                        Id = orderDetail.Product.Color.Id,
                                        Name = orderDetail.Product.Color.Name
                                    },
                                    Size = new
                                    {
                                        Id = orderDetail.Product.Size.Id,
                                        Name = orderDetail.Product.Size.Name
                                    },
                                    Specification = new
                                    {
                                        Id = orderDetail.Product.Specification.Id,
                                        Description = orderDetail.Product.Specification.Description,
                                        Detail = orderDetail.Product.Specification.Detail,
                                        Category = new
                                        {
                                            Id = orderDetail.Product.Specification.Category.Id,
                                            Name = orderDetail.Product.Specification.Category.Name
                                        }
                                    }
                                }
                            };
                return Ok(query.ToList());
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
    }
}
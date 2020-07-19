using System;
using System.Linq;
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
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
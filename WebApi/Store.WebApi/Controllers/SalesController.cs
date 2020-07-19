using System;
using System.Collections.Generic;
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
    public class SalesController : ControllerBase
    {
        private readonly ISaleLogic saleLogic;

        public SalesController(ISaleLogic saleLogic) : base ()
        {
            this.saleLogic = saleLogic;
        }

        [HttpGet("{date}")]
        public IActionResult Get(DateTime date)
        {
            try
            {
                var results = saleLogic.GetByDate(date);
                var query = from sale in results
                            select new
                            {
                                Id = sale.Id,
                                Date = sale.Date,
                                UserName = sale.User.FullName,
                                Total = sale.SaleDetails.Sum(item => item.UnitPrice)
                            };
                var list = query.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("Detail/{id}")]
        public IActionResult Detail(int id)
        {
            try
            {
                var sale = saleLogic.Get(id);
                var query = from detail in sale.SaleDetails group detail by (detail.OrderDetail.Product.Code, detail.OrderDetail.Product.Color.Id, detail.OrderDetail.Product.Size.Id);
                var list = new List<object>();
                foreach (var q in query)
                {
                    var item = q.FirstOrDefault();
                    list.Add(new
                    {
                        Code = item.OrderDetail.Product.Code,
                        Description = item.OrderDetail.Product.Specification.Description,
                        Size = item.OrderDetail.Product.Size.Name,
                        Color = item.OrderDetail.Product.Color.Name,
                        Detail = item.OrderDetail.Product.Specification.Detail,
                        Brand = item.OrderDetail.Product.Brand,
                        UnitPrice = item.UnitPrice,
                        Amount = q.Count()
                    });
                }

                return Ok(new {
                    Id = sale.Id,
                    Date = sale.Date,
                    UserName = sale.User.FullName,
                    Items = list
                });
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] SaleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string userName = User.Identity.Name;
                    saleLogic.Register(model.Date, userName, model.OrderDetailIds, model.UnitPrices);
                    return Ok(new { Result = "Venta registrada exitosamente." });
                }
                return NotFound(new { ErrorMessage = "Datos inválidos" });
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }
    }
}
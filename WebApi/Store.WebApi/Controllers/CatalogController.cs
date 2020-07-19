using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store.Logic.Layer.Contracts;
using Store.WebApi.ViewModels;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly IItemLogic itemLogic;
        private readonly IProductLogic productLogic;

        public CatalogController(IItemLogic itemLogic, IProductLogic productLogic) : base()
        {
            this.itemLogic = itemLogic;
            this.productLogic = productLogic;
        }

        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            try
            {
                return Ok(itemLogic.GetCategories());
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("Colors")]
        public IActionResult GetColors()
        {
            try
            {
                return Ok(itemLogic.GetColors());
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("Sizes")]
        public IActionResult GetSizes()
        {
            try
            {
                return Ok(itemLogic.GetSizes());
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("Specifications/{description}")]
        public IActionResult SearchSpecifications(string description)
        {
            try
            {
                var list = productLogic.FindSpecifications(description);
                var query = from item in list
                            select new
                            {
                                Id = item.Id,
                                Description = item.Description,
                                Detail = item.Detail,
                                Category = new
                                {
                                    Id = item.Category.Id,
                                    Name = item.Category.Name
                                }
                            };
                return Ok(query.ToList());
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("Find/{code}")]
        public IActionResult Find(string code)
        {
            try
            {
                var result = productLogic.Find(code);
                var query = from product in result
                            select new
                            {
                                Id = product.Id,
                                Code = product.Code,
                                Price = product.Price,
                                Brand = product.Brand,
                                Specification = new
                                {
                                    Id = product.Specification.Id,
                                    Description = product.Specification.Description,
                                    Detail = product.Specification.Detail,
                                    Category = new
                                    {
                                        Id = product.Specification.Category.Id,
                                        Name = product.Specification.Category.Name
                                    }
                                },
                                Color = new
                                {
                                    Id = product.Color.Id,
                                    Name = product.Color.Name
                                },
                                Size = new
                                {
                                    Id = product.Size.Id,
                                    Name = product.Size.Name
                                }
                            };
                return Ok(query.ToList());
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductModel model)
        {
            try
            {
                if (productLogic.Register(model.Code, model.Price, model.Description, model.Brand, model.Detail, model.Specification, model.Color, model.Size, model.Category))
                    return Ok(new { Result = "Producto registrado exitosamente." });
                else
                    return NotFound(new { ErrorMessage = "No se pudo registrar el producto." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return NotFound(new { ErrorMessage = "Ocurrió un error." });
            }
        }
    }
}
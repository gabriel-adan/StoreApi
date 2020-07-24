using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Store.Logic.Layer.Contracts;
using Store.WebApi.ViewModels;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IItemLogic itemLogic;
        private readonly IProductLogic productLogic;

        public CatalogController(ILogger<CatalogController> logger, IItemLogic itemLogic, IProductLogic productLogic) : base()
        {
            this.logger = logger;
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
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al listar las categorias.");
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
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al listar los colores.");
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
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al listar los talles.");
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
            catch (ArgumentException ae)
            {
                return NotFound(new { ErrorMessage = ae.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al listar las especificaciones de producto.");
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
            catch (ArgumentException ae)
            {
                return NotFound(new { ErrorMessage = ae.Message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al listar los productos.");
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
                logger.LogError(ex, ex.Message);
                throw new Exception("Ocurrió un error al intentar registrar el producto.");
            }
        }
    }
}
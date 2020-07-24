using System;
using System.Collections.Generic;
using Store.Logic.Layer.Contracts;
using Store.Business.Layer.RepositoryInterfaces;
using Store.Business.Layer;

namespace Store.Logic.Layer.Common
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository productRepository;
        private readonly IRepository<Specification> specificationRepository;
        private readonly IRepository<Color> colorRepository;
        private readonly IRepository<Size> sizeRepository;
        private readonly IRepository<Category> categoryRepository;

        public ProductLogic(IProductRepository productRepository, IRepository<Specification> specificationRepository, IRepository<Color> colorRepository, IRepository<Size> sizeRepository, IRepository<Category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.specificationRepository = specificationRepository;
            this.colorRepository = colorRepository;
            this.sizeRepository = sizeRepository;
            this.categoryRepository = categoryRepository;
        }

        public IList<Specification> FindSpecifications(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description))
                    throw new ArgumentException("Debe proveer un valor para buscar la descripción.");
                return productRepository.FindSpecifications(description);
            }
            catch
            {
                throw;
            }
        }

        public IList<Product> Find(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                    throw new ArgumentException("Debe proveer un código de producto.");
                return productRepository.Find(code);
            }
            catch
            {
                throw;
            }
        }

        public bool Register(string code, float price, string description, string brand, string detail, int specificationId, int colorId, int sizeId, int categoryId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                    throw new ArgumentException("Código de producto inválido.");
                if (price <= 0)
                    throw new ArgumentException("Precio de producto inválido.");
                if (string.IsNullOrEmpty(description) && specificationId == 0)
                    throw new ArgumentException("Especificación de producto inválida.");
                if (colorId == 0)
                    throw new ArgumentException("Color inválido.");
                if (sizeId == 0)
                    throw new ArgumentException("Talle inválido.");
                if (categoryId == 0)
                    throw new ArgumentException("Categoria inválida.");
                Color color = colorRepository.Get(colorId);
                if (color == null)
                    throw new ArgumentException("Color inválido.");
                Size size = sizeRepository.Get(sizeId);
                if (size == null)
                    throw new ArgumentException("Talle inválido.");
                Specification specification = specificationRepository.Get(specificationId);
                Product product = productRepository.Find(code, colorId, sizeId, specificationId);
                if (product != null)
                    throw new ArgumentException("Ya existe el producto con la especificación ingresada.");

                productRepository.BeginTransaction();
                if (specification == null)
                {
                    Category category = categoryRepository.Get(categoryId);
                    if (category == null)
                        throw new ArgumentException("Categoria inválida.");
                    specification = new Specification();
                    specification.Description = description;
                    specification.Detail = detail;
                    specification.Category = category;
                    specificationRepository.SaveOrUpdate(specification);
                }
                
                product = new Product();
                product.Code = code;
                product.Price = price;
                product.Brand = brand;
                product.Specification = specification;
                product.Color = color;
                product.Size = size;
                productRepository.SaveOrUpdate(product);
                productRepository.CommitTransaction();
                return true;
            }
            catch
            {
                productRepository.RollbackTransaction();
                throw;
            }
        }
    }
}

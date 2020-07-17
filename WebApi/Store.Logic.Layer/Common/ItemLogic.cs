using System;
using System.Collections.Generic;
using Store.Business.Layer;
using Store.Logic.Layer.Contracts;
using Store.Business.Layer.RepositoryInterfaces;

namespace Store.Logic.Layer.Common
{
    public class ItemLogic : IItemLogic
    {
        private readonly IRepository<Color> colorRepository;
        private readonly IRepository<Size> sizeRepository;
        private readonly IRepository<Category> categoryRepository;

        public ItemLogic(IRepository<Color> colorRepository, IRepository<Size> sizeRepository, IRepository<Category> categoryRepository)
        {
            this.colorRepository = colorRepository;
            this.sizeRepository = sizeRepository;
            this.categoryRepository = categoryRepository;
        }

        public void AddColor(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Especifique un nombre para el color.");
                Color color = new Color();
                color.Name = name;
                colorRepository.Save(color);
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Ocurrió un error al registrar el Color: {0}", name));
            }
        }

        public void AddSize(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Especifique un nombre para el talle.");
                Size size = new Size();
                size.Name = name;
                sizeRepository.Save(size);
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Ocurrió un error al registrar el Talle: {0}", name));
            }
        }

        public void EditColor(Color color)
        {
            try
            {
                colorRepository.SaveOrUpdate(color);
            }
            catch
            {
                throw;
            }
        }

        public void EditSize(Size size)
        {
            try
            {
                sizeRepository.SaveOrUpdate(size);
            }
            catch
            {
                throw;
            }
        }

        public void AddCategory(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Especifique un nombre para la categoria.");
                Category category = new Category();
                category.Name = name;
                categoryRepository.Save(category);
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Ocurrió un error al registrar la Categoria: {0}", name));
            }
        }

        public void EditCategory(Category category)
        {
            try
            {
                categoryRepository.SaveOrUpdate(category);
            }
            catch
            {
                throw;
            }
        }

        public IList<Color> GetColors()
        {
            try
            {
                return colorRepository.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<Size> GetSizes()
        {
            try
            {
                return sizeRepository.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public IList<Category> GetCategories()
        {
            try
            {
                return categoryRepository.GetAll();
            }
            catch
            {
                throw;
            }
        }
    }
}

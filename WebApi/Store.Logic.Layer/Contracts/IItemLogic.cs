using Store.Business.Layer;
using System.Collections.Generic;

namespace Store.Logic.Layer.Contracts
{
    public interface IItemLogic
    {
        void AddColor(string name);

        void EditColor(Color color);

        void AddSize(string name);

        void EditSize(Size size);

        void AddCategory(string name);

        void EditCategory(Category category);

        IList<Color> GetColors();

        IList<Size> GetSizes();

        IList<Category> GetCategories();
    }
}

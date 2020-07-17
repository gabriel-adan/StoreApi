using Store.Business.Layer;
using System.Collections.Generic;

namespace Store.Logic.Layer.Contracts
{
    public interface IProductLogic
    {
        IList<Specification> FindSpecifications(string description);

        IList<Product> Find(string code);

        bool Register(string code, float price, string description, string mark, string type, int specification, int color, int size, int category);
    }
}

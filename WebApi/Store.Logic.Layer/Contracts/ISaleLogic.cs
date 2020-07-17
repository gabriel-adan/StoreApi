using Store.Business.Layer;
using System;
using System.Collections.Generic;

namespace Store.Logic.Layer.Contracts
{
    public interface ISaleLogic
    {
        Sale Get(int id);

        IList<Sale> GetByDate(DateTime date);

        void Register(DateTime? date, string userName, IList<int> orderDetailIds, IList<float> unitPrices, IList<int> amounts);
    }
}

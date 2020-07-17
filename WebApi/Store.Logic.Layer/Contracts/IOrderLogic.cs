using System;
using System.Collections.Generic;

namespace Store.Logic.Layer.Contracts
{
    public interface IOrderLogic
    {
        void Register(DateTime? date, string ticketCode, IList<int> productIds, IList<float> unitCosts, IList<int> amounts);
    }
}

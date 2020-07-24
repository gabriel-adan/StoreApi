using System;
using System.Collections.Generic;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;
using Store.Logic.Layer.Contracts;

namespace Store.Logic.Layer.Common
{
    public class SaleLogic : ISaleLogic
    {
        private readonly ISaleRepository saleRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IRepository<OrderDetail> orderDetailRepository;
        private readonly IUserAccountRepository userAccountRepository;

        public SaleLogic(ISaleRepository saleRepository, IOrderRepository orderRepository, IRepository<OrderDetail> orderDetailRepository, IUserAccountRepository userAccountRepository)
        {
            this.saleRepository = saleRepository;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
            this.userAccountRepository = userAccountRepository;
        }

        public Sale Get(int id)
        {
            try
            {
                return saleRepository.Get(id);
            }
            catch
            {
                throw;
            }
        }

        public IList<Sale> GetByDate(DateTime date)
        {
            try
            {
                return saleRepository.GetList(date);
            }
            catch
            {
                throw;
            }
        }

        public void Register(DateTime? date, string userName, IList<int> orderDetailIds, IList<float> unitPrices)
        {
            try
            {
                User user = userAccountRepository.Find(userName);
                if (user == null)
                    throw new ArgumentException("Usuario inválido.");
                if (date == null || !date.HasValue || date == DateTime.MinValue)
                    throw new ArgumentException("Fecha inválida.");
                if (orderDetailIds == null || orderDetailIds.Count == 0)
                    throw new ArgumentException("Productos inválidos.");
                if (unitPrices == null || unitPrices.Count == 0)
                    throw new ArgumentException("Precios de productos inválidos.");

                int count = orderDetailIds.Count;
                if (count == unitPrices.Count)
                {
                    saleRepository.BeginTransaction();
                    Sale sale = new Sale();
                    sale.Date = date.Value;
                    sale.User = user;
                    saleRepository.Save(sale);
                    for (int i = 0; i < count; i++)
                    {
                        float unitPrice = unitPrices[i];
                        if (unitPrice < 0)
                            throw new ArgumentException("Precio inválido.");
                        int orderDetailId = orderDetailIds[i];
                        OrderDetail orderDetail = orderDetailRepository.Get(orderDetailId);
                        if (orderDetail == null)
                            throw new ArgumentException("Producto no encontrado.");

                        SaleDetail saleDetail = new SaleDetail();
                        saleDetail.Sale = sale;
                        saleDetail.OrderDetail = orderDetail;
                        saleDetail.UnitPrice = unitPrice == 0 ? orderDetail.Product.Price : unitPrice;
                        sale.SaleDetails.Add(saleDetail);
                    }
                    saleRepository.SaveOrUpdate(sale);
                    saleRepository.CommitTransaction();
                }
                else
                {
                    throw new ArgumentException("Datos de productos inválidos.");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                try
                {
                    saleRepository.RollbackTransaction();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}

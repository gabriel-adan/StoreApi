using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public SaleLogic(ISaleRepository saleRepository, IOrderRepository orderRepository, IRepository<OrderDetail> orderDetailRepository)
        {
            this.saleRepository = saleRepository;
            this.orderRepository = orderRepository;
            this.orderDetailRepository = orderDetailRepository;
        }

        public Sale Get(int id)
        {
            try
            {
                return saleRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error.", ex);
            }
        }

        public IList<Sale> GetByDate(DateTime date)
        {
            try
            {
                return saleRepository.GetList(date);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error.", ex);
            }
        }

        public void Register(DateTime? date, string userName, IList<int> orderDetailIds, IList<float> unitPrices, IList<int> amounts)
        {
            try
            {
                if (string.IsNullOrEmpty(userName))
                    throw new ArgumentException("Usuario inválido.");
                if (date == null || !date.HasValue || date == DateTime.MinValue)
                    throw new ArgumentException("Fecha inválida.");
                if (orderDetailIds == null || orderDetailIds.Count == 0)
                    throw new ArgumentException("Productos inválidos.");
                if (unitPrices == null || unitPrices.Count == 0)
                    throw new ArgumentException("Precios de productos inválidos.");
                if (amounts == null || amounts.Count == 0)
                    throw new ArgumentException("Cantidades de productos inválidos.");

                int count = orderDetailIds.Count;
                if (count == unitPrices.Count && count == amounts.Count)
                {
                    saleRepository.BeginTransaction();
                    Sale sale = new Sale();
                    sale.Date = date.Value;
                    sale.UserName = userName;
                    saleRepository.Save(sale);
                    for (int i = 0; i < count; i++)
                    {
                        int amount = amounts[i];
                        if (amount < 1)
                            throw new ArgumentException("Cantidad inválida.");
                        float unitPrice = unitPrices[i];
                        if (unitPrice < 0)
                            throw new ArgumentException("Precio inválido.");
                        int orderDetailId = orderDetailIds[i];
                        OrderDetail orderItem = orderDetailRepository.Get(orderDetailId);
                        if (orderItem == null)
                            throw new ArgumentException("Producto no encontrado.");
                        Product product = orderItem.Product;
                        IList<OrderDetail> orderDetails = orderRepository.Find(product.Code, product.Color.Id, product.Size.Id, amount);
                        int units = orderDetails.Count;
                        if (units == 0)
                            throw new ArgumentException(string.Format("No quedan unidades en stock del producto {0} {1} Talle: {2} Color: {3}.", product.Code, product.Specification.Description, product.Size.Name, product.Color.Name));
                        if (units != amount)
                            throw new ArgumentException(string.Format("Solo queda{0} {1} unidad{2} registrada{3} en el sistema del producto {4} {5} Talle: {6} Color: {7}.", units == 1 ? "" : "n", units, units == 1 ? "" : "es", units == 1 ? "" : "s", product.Code, product.Specification.Description, product.Size.Name, product.Color.Name));
                        foreach(OrderDetail orderDetail in orderDetails)
                        {
                            SaleDetail saleDetail = new SaleDetail();
                            saleDetail.Sale = sale;
                            saleDetail.OrderDetail = orderDetail;
                            saleDetail.UnitPrice = unitPrice == 0 ? orderDetail.Product.Price : unitPrice;
                            sale.SaleDetails.Add(saleDetail);
                        }
                    }
                    saleRepository.SaveOrUpdate(sale);
                    saleRepository.CommitTransaction();
                }
                else
                {
                    throw new ArgumentException("Datos de productos inválidos.");
                }
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error.", ex);
            }
            finally
            {
                saleRepository.RollbackTransaction();
            }
        }
    }
}

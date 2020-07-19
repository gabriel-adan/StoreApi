using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;
using Store.Logic.Layer.Contracts;

namespace Store.Logic.Layer.Common
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository orderRepository;
        private readonly IRepository<Product> productRepository;

        public OrderLogic(IOrderRepository orderRepository, IRepository<Product> productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }

        public void Register(DateTime? date, string ticketCode, IList<int> productIds, IList<float> unitCosts, IList<int> amounts)
        {
            try
            {
                if (string.IsNullOrEmpty(ticketCode))
                    throw new ArgumentException("El código de la factura es obligatorio.");
                if (date == null || !date.HasValue || date == DateTime.MinValue)
                    throw new ArgumentException("Fecha inválida.");
                if (productIds == null)
                    throw new ArgumentException("Productos inválidos.");
                if (unitCosts == null)
                    throw new ArgumentException("Costos de productos inválidos.");
                if (amounts == null)
                    throw new ArgumentException("Cantidades de productos inválidos.");

                int count = productIds.Count;

                if (count == 0)
                    throw new ArgumentException("No hay datos de productos para registrar.");

                if (count != unitCosts.Count)
                    throw new ArgumentException("Datos de costos incorrectos.");

                if (count != amounts.Count)
                    throw new ArgumentException("Datos de cantidades incorrectos.");

                orderRepository.BeginTransaction();
                Order order = new Order();
                order.Date = date.Value;
                order.TicketCode = ticketCode;
                orderRepository.Save(order);

                for (int i = 0; i < count; i++)
                {
                    int productId = productIds[i];
                    float unitCost = unitCosts[i];
                    int amount = amounts[i];

                    Product product = productRepository.Get(productId);
                    if (product == null)
                        throw new ArgumentException("Código de producto inválido.");

                    if (unitCost <= 0)
                        throw new ArgumentException(string.Format("El precio de Costo es inválido para el producto con Código {0}", product.Code));

                    if (amount <= 0)
                        throw new ArgumentException(string.Format("La cantidad es inválida para el producto con Código {0}", product.Code));
                    
                    for (int a = 0; a < amount; a++)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.UnitCost = unitCost;
                        orderDetail.Product = product;
                        orderDetail.Order = order;
                        order.OrderDetails.Add(orderDetail);
                    }
                }

                orderRepository.SaveOrUpdate(order);
                orderRepository.CommitTransaction();
            }
            catch (ArgumentException aex)
            {
                throw aex;
            }
            catch (SqlException sqlex)
            {
                throw new Exception("Error al intentar registrar los datos de la factura.", sqlex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error.", ex);
            }
            finally
            {
                try
                {
                    orderRepository.RollbackTransaction();
                }
                catch
                {
                    throw new Exception("Error en transacción...");
                }
            }
        }

        public IList<OrderDetail> Find(int productId, int amount)
        {
            try
            {
                if (productId <= 0)
                    throw new ArgumentException("Código de producto inválido.");
                if (amount <= 0)
                    throw new ArgumentException("Cantidad inválida.");
                return orderRepository.Find(productId, amount);
            }
            catch (ArgumentException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error.", ex);
            }
        }
    }
}

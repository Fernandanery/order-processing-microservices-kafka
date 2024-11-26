using Delivery.Domain.Entities;

namespace Delivery.Domain.Services
{
    public interface IOrderDeliveryService
    {
        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="items">Order items</param>
        /// <param name="customer">Order customer</param>
        Task CreateOrderDelivery(IList<string> items, Customer customer);

        /// <summary>
        /// Updates an order
        /// </summary>
        /// <param name="orderId">Order identification</param>
        /// <param name="orderSituation">Order situation</param>
        Task UpdateOrderDeliverySituation(string orderId, OrderDeliverySituation orderSituation);

        /// <summary>
        /// Gets all orders to delivery
        /// </summary>
        IList<Entities.OrderDelivery> GetAllOrdersToDelivery();
    }
}

using Delivery.Domain.Entities;

namespace Delivery.Domain.Infrastructure.Repositories
{
    /// <summary>
    /// Order delivery repository
    /// </summary>
    public class OrderDeliveryRepository : IOrderDeliveryRepository
    {
        private static readonly IList<Entities.OrderDelivery> _orderDataBase = [];

        /// <summary>
        /// Add an order into database
        /// </summary>
        /// <param name="order">Order's data</param>
        public void Add(Entities.OrderDelivery order)
        {
            _orderDataBase.Add(order);
        }

        /// <summary>
        /// Get all orders from database
        /// </summary>
        /// <returns>All orders</returns>
        public IList<Entities.OrderDelivery> GetAll()
        {
            return [.. _orderDataBase];
        }

        /// <summary>
        /// Get an order from database by its id
        /// </summary>
        /// <returns>Order related to the id asked</returns>
        public Entities.OrderDelivery GetOrderById(string OrderId)
        {
            var orderRecovered = _orderDataBase.FirstOrDefault(o => o.OrderDeliveryId.Equals(OrderId));

            return orderRecovered ?? throw new Exception("Order not found");
        }

        /// <summary>
        /// Get all orders from database for determined situation
        /// </summary>
        /// <returns>All orders those have the requested situation</returns>
        public IList<Entities.OrderDelivery> GetOrdersBySituation(OrderDeliverySituation orderSituation)
        {
            return _orderDataBase.Where(o => o.OrderDeliverySituation == orderSituation).ToList();
        }

        /// <summary>
        /// Update an order to database
        /// </summary>
        /// <param name="order">Order's data</param>
        public void Update(Entities.OrderDelivery order)
        {
            var orderRecovered = GetOrderById(order.OrderDeliveryId);

            orderRecovered.OrderDeliverySituation = order.OrderDeliverySituation;
        }
    }
}

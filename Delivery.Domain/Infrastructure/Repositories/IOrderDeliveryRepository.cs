using Delivery.Domain.Entities;

namespace Delivery.Domain.Infrastructure.Repositories
{
    /// <summary>
    /// Order Delivery repository
    /// </summary>
    public interface IOrderDeliveryRepository
    {
        /// <summary>
        /// Add an order into database
        /// </summary>
        /// <param name="order">Order's data</param>
        void Add(Entities.OrderDelivery order);

        /// <summary>
        /// Update an order to database
        /// </summary>
        /// <param name="order">Order's data</param>
        void Update(Entities.OrderDelivery order);

        /// <summary>
        /// Get all orders from database
        /// </summary>
        /// <returns>All orders</returns>
        IList<Entities.OrderDelivery> GetAll();

        /// <summary>
        /// Get an order from database by its id
        /// </summary>
        /// <returns>Order related to the id requested</returns>
        Entities.OrderDelivery GetOrderById(string OrderId);

        /// <summary>
        /// Get all orders from database for determined situation
        /// </summary>
        /// <returns>All orders those have the requested situation</returns>
        IList<Entities.OrderDelivery> GetOrdersBySituation(OrderDeliverySituation orderSituation);
    }
}

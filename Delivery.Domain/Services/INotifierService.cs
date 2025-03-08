using Delivery.Domain.Entities;

namespace Delivery.Domain.Services
{
    /// <summary>
    /// Notifier contract service
    /// </summary>
    public interface INotifierService
    {
        /// <summary>
        /// Notifies the customer about some order update
        /// </summary>
        /// <param name="order">Order data</param>
        void Notify(Entities.OrderDelivery order);
    }
}

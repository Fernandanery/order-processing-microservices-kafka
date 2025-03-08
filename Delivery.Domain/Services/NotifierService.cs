using Delivery.Domain.Entities;

namespace Delivery.Domain.Services
{
    /// <summary>
    /// Notifier service
    /// </summary>
    public class NotifierService : INotifierService
    {
        /// <summary>
        /// Notifies the customer about some order update
        /// </summary>
        /// <param name="order">Order data</param>
        public void Notify(Entities.OrderDelivery order)
        {
            SendEmail(order.Customer.Email);
        }

        /// <summary>
        /// Sends email about order update to customer
        /// </summary>
        private static void SendEmail(string email)
        {
            Console.WriteLine("Email sent to recipient: " + email);
        }
}
}

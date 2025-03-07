namespace Delivery.Domain.Entities
{
    /// <summary>
    /// Order Delivery
    /// </summary>
    public class OrderDelivery
    {
        /// <summary>
        /// Order Delivery id
        /// </summary>
        public string OrderDeliveryId { get; set; }

        /// <summary>
        /// Order delivery date
        /// </summary>
        public DateTime OrderDeliveryCreateDate { get; set; }

        /// <summary>
        /// Order delivery last update
        /// </summary>
        public DateTime OrderDeliveryLastUpdate { get; set; }

        /// <summary>
        /// Orders delivery situation
        /// </summary>
        public OrderDeliverySituation OrderDeliverySituation { get; set; }

        /// <summary>
        /// Orders delivery items
        /// </summary>
        public IList<string> Items { get; set; }

        /// <summary>
        /// Orders delivery customer
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Order builder
        /// </summary>
        /// <param name="items">Order items</param>
        /// <param name="customer">Order customer</param>
        public OrderDelivery(IList<string> items, Customer customer)
        {
            OrderDeliveryId = Guid.NewGuid().ToString();
            OrderDeliveryCreateDate = DateTime.Now;
            OrderDeliveryLastUpdate = OrderDeliveryCreateDate;
            OrderDeliverySituation = OrderDeliverySituation.CREATED;
            Items = items;
            Customer = customer;
        }

        public OrderDelivery()
        {

        }
    }

}

using Delivery.Domain.Entities;
using Delivery.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.API.Controllers
{
    /// <summary>
    /// Delivery Api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController(IOrderDeliveryService deliveryService) : ControllerBase
    {
        private readonly IOrderDeliveryService _deliveryService = deliveryService;

        /// <summary>
        /// Creates an order
        /// </summary>
        /// <param name="items">Order items</param>
        /// <param name="customer">Order customer</param>
        [HttpPost("create-order-delivery")]
        public async void CreateOrderDelivery([FromForm] IList<string> items, [FromForm] Customer customer)
        {
            await _deliveryService.CreateOrderDelivery(items, customer);
        }

        /// <summary>
        /// Updates an order situation
        /// </summary>
        /// <param name="orderDeliveryId">Order Delivery identification</param>
        /// <param name="orderDeliverySituation">Order situation</param>
        [HttpPut("update-order-delivery-situation")]
        public async void UpdateOrderSituation([FromForm] string orderDeliveryId, [FromForm] OrderDeliverySituation orderDeliverySituation)
        {
            await _deliveryService.UpdateOrderDeliverySituation(orderDeliveryId, orderDeliverySituation);
        }

        /// <summary>
        /// Gets all orders to delivery
        /// </summary>
        [HttpGet("get-all-orders-to-delivery")]
        public IList<Domain.Entities.OrderDelivery> GetAllOrdersToDelivery()
        {
            return _deliveryService.GetAllOrdersToDelivery();
        }
    }
}

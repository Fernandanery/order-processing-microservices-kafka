using Confluent.Kafka;
using Delivery.Domain.Entities;
using Delivery.Domain.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Delivery.Domain.Services
{
    /// <summary>
    /// Order service
    /// </summary>
    public class OrderDeliveryService : IOrderDeliveryService
    {
        private readonly IOrderDeliveryRepository _orderRepository;
        private readonly IConfiguration _configuration;
        private readonly string _orderTopicName;
        private readonly string _kafkaBootstrapServers;

        /// <summary>
        /// Order service builder
        /// </summary>
        /// <param name="orderRepository">Order repository</param>
        /// <param name="configuration">Global configuration (It is being used the "appsettings.json" file located in the solution's root</param>
        public OrderDeliveryService(IConfiguration configuration, IOrderDeliveryRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            _orderTopicName = _configuration["OrderSettings:OrderTopicName"];
            _kafkaBootstrapServers = _configuration["OrderSettings:KafkaBootstrapServer"];
        }

        /// <summary>
        /// Creates a order
        /// </summary>
        /// <param name="items">Order items</param>
        /// <param name="customer">Order customer</param>
        public async Task CreateOrderDelivery(IList<string> items, Customer customer)
        {
            Entities.OrderDelivery order = new Entities.OrderDelivery(items, customer);

            _orderRepository.Add(order);

            await PublishMessageToTopic(order);
        }

        /// <summary>
        /// Updates an order
        /// </summary>
        /// <param name="orderId">Order identification</param>
        /// <param name="orderSituation">Order situation</param>
        public async Task UpdateOrderDeliverySituation(string orderId, OrderDeliverySituation orderSituation)
        {
            Entities.OrderDelivery orderToBeUpdated = _orderRepository.GetOrderById(orderId);

            orderToBeUpdated.OrderDeliverySituation = orderSituation;
            orderToBeUpdated.OrderDeliveryLastUpdate = DateTime.Now;

            _orderRepository.Update(orderToBeUpdated);

            await PublishMessageToTopic(orderToBeUpdated);
        }

        /// <summary>
        /// Gets all orders to delivery
        /// </summary>
        public IList<Entities.OrderDelivery> GetAllOrdersToDelivery()
        {
            return _orderRepository.GetOrdersBySituation(OrderDeliverySituation.CREATED);
        }

        /// <summary>
        /// Publishs message with order data to Kafka topic
        /// </summary>
        /// <param name="order">Order data</param>
        /// <returns></returns>
        private async Task PublishMessageToTopic(Entities.OrderDelivery order)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaBootstrapServers
            };

            string orderConvertedToJson = JsonSerializer.Serialize(order);

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var deliveryReport = await producer.ProduceAsync(_orderTopicName, new Message<Null, string> { Value = orderConvertedToJson });
            }
        }
    }
}

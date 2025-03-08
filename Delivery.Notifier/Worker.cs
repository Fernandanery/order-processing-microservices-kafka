using Confluent.Kafka;
using Delivery.Domain.Services;
using System.Text.Json;

namespace Delivery.Notifier
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly string _orderTopicName;
        private readonly string _kafkaBootstrapServers;
        private readonly string _notifierConsumeGroupName;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _orderTopicName = _configuration["OrderSettings:OrderTopicName"];
            _kafkaBootstrapServers = _configuration["OrderSettings:KafkaBootstrapServer"];
            _notifierConsumeGroupName = _configuration["OrderSettings:NotifierConsumeGroupName"];
        }

        /// <summary>
        /// Lopping process
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            INotifierService notifierService;
            notifierService = new NotifierService();
            if (notifierService == null)
            {
                ArgumentNullException argumentNullException = new ArgumentNullException(nameof(notifierService));
                throw argumentNullException;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Order notifier running at: {time}", DateTimeOffset.Now);

                var config = new ConsumerConfig
                {
                    BootstrapServers = _kafkaBootstrapServers,
                    GroupId = _notifierConsumeGroupName,
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(_orderTopicName);

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cts.Token);

                            var order = JsonSerializer.Deserialize<Domain.Entities.OrderDelivery>(consumeResult.Message.Value);

                            notifierService.Notify(order);

                            Console.WriteLine($"Mensagem recebida: {consumeResult.Message.Value}");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Erro ao consumir a mensagem: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
        }
    }
}

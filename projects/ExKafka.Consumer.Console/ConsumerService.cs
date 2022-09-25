using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExKafka.Consumer.Console
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ConsumerConfig _consumerConfig;
        private readonly ILogger<ConsumerService> _logger;
        public ConsumerService(ILogger<ConsumerService> logger)
        {
            _logger = logger;
            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "Group 1",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando as mensagens");
            _consumer.Subscribe("topicOrders");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Run(() =>
                {
                    var result = _consumer.Consume(stoppingToken);
                    var order = JsonSerializer.Deserialize<Order>(result.Message.Value);
                    _logger.LogInformation($"GroupId: Group 1 - Mensagem: {result.Message.Value}");
                    _logger.LogInformation(order != null ? order.ToString() : "Objeto order nulo");
                });
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _consumer.Close();
            _logger.LogInformation("A conexão foi fechada.");
            return Task.CompletedTask;
        }
    }
}
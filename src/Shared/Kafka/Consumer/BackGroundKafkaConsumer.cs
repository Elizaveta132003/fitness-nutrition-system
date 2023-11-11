using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Shared.Kafka.Consumer
{
    public class BackGroundKafkaConsumer<TK, TV> : BackgroundService
    {
        private readonly KafkaConsumerConfig<TK, TV> _config;
        private IKafkaHandler<TK, TV> _handler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackGroundKafkaConsumer(IOptions<KafkaConsumerConfig<TK, TV>> config,
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _config = config.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                _handler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<TK, TV>>();

                var builder = new ConsumerBuilder<TK, TV>(_config).SetValueDeserializer(new KafkaDeserializer<TV>());

                using (IConsumer<TK, TV> consumer = builder.Build())
                {
                    consumer.Subscribe(_config.Topic);

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var consumeResult = consumer.Consume(TimeSpan.FromMilliseconds(5000));

                        if (consumeResult != null)
                        {
                            await _handler.HandleAsync(consumeResult.Message.Key, consumeResult.Message.Value);

                            consumer.Commit(consumeResult);

                            consumer.StoreOffset(consumeResult);
                        }
                    }
                }
            }
        }
    }
}

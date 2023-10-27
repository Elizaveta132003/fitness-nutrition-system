using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Kafka.Consumer;
using Shared.Kafka.Producer;

namespace Shared.Kafka
{
    public static class RegisterServiceExtensions
    {
        public static IServiceCollection AddKafkaMessageBus(this IServiceCollection serviceCollection)
            => serviceCollection.AddScoped(typeof(IKafkaMessageBus<,>), typeof(KafkaMessageBus<,>));

        public static IServiceCollection AddKafkaConsumer<Tk, Tv, THandler>(this IServiceCollection services,
            Action<KafkaConsumerConfig<Tk, Tv>> configAction) where THandler : class, IKafkaHandler<Tk, Tv>
        {
            services.AddScoped<IKafkaHandler<Tk, Tv>, THandler>();

            services.AddHostedService<BackGroundKafkaConsumer<Tk, Tv>>();

            services.Configure(configAction);

            return services;
        }

        public static IServiceCollection AddKafkaProducer<Tk, Tv>(this IServiceCollection services,
            Action<KafkaProducerConfig<Tk, Tv>> configAction)
        {
            services.AddConfluentKafkaProducer<Tk, Tv>();

            services.AddScoped<KafkaProducer<Tk, Tv>>();

            services.Configure(configAction);

            return services;
        }

        private static IServiceCollection AddConfluentKafkaProducer<Tk, Tv>(this IServiceCollection services)
        {
            services.AddScoped(
                serviceProvider =>
                {
                    var config = serviceProvider.GetRequiredService<IOptions<KafkaProducerConfig<Tk, Tv>>>();

                    var builder = new ProducerBuilder<Tk, Tv>(config.Value).SetValueSerializer(new KafkaSerializer<Tv>());

                    return builder.Build();
                });

            return services;
        }
    }
}

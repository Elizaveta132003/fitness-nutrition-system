using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kafka.Messages;
using Shared.Kafka;

namespace Identity.Application.Configurations
{
    public static class KafkaConfiguration
    {
        public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("ProducerConfig");

            services.AddKafkaProducer<string, UserMessage>(p =>
            {
                p.Topic = kafkaConfig["Topic"]!;
                p.BootstrapServers = kafkaConfig["BootstrapServers"];
            });
        }
    }
}

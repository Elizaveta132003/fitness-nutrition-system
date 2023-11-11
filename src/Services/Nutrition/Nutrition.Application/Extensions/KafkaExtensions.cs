using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutrition.Application.Events;
using Shared.Kafka;
using Shared.Kafka.Messages;

namespace Nutrition.Application.Extensions
{
    public static class KafkaExtensions
    {
        public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("ConsumerConfig");

            services.AddKafkaConsumer<string, UserMessage, NutritionKafkaHandler>(p =>
            {
                p.Topic = "users_topic";
                p.GroupId = kafkaConfig["GroupId"];
                p.BootstrapServers = kafkaConfig["BootstrapServers"];
            });
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Kafka;
using Shared.Kafka.Messages;
using Workouts.BusinessLogic.Events;

namespace Workouts.BusinessLogic.Extensions
{
    public static class KafkaExtension
    {
        public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddKafkaConsumer<string, UserMessage, WorkoutsKafkaHandler>(p =>
            {
                var kafkaConfig = configuration.GetSection("ConsumerConfig");

                p.Topic = "users_topic";
                p.GroupId = kafkaConfig["GroupId"];
                p.BootstrapServers = kafkaConfig["BootstrapServers"];
            });
        }
    }
}

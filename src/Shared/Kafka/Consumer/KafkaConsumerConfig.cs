using Confluent.Kafka;

namespace Shared.Kafka.Consumer
{
    public class KafkaConsumerConfig<Tk, Tv> : ConsumerConfig
    {
        public string Topic { get; set; }
        public KafkaConsumerConfig()
        {
            EnableAutoOffsetStore = false;
            EnableAutoCommit = true;
            AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
        }
    }
}

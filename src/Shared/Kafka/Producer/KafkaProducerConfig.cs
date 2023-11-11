using Confluent.Kafka;

namespace Shared.Kafka.Producer
{
    public class KafkaProducerConfig<Tk, Tv> : ProducerConfig
    {
        public string Topic { get; set; }

        public KafkaProducerConfig()
        {
            SecurityProtocol = Confluent.Kafka.SecurityProtocol.Plaintext;
        }
    }
}

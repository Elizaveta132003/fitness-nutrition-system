using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace Shared.Kafka
{
    public class KafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (typeof(T) == typeof(Null))
            {
                return null!;
            }

            var json = JsonConvert.SerializeObject(data);

            return Encoding.UTF8.GetBytes(json);
        }
    }
}

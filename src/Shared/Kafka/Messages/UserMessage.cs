using Shared.Kafka.Enums;

namespace Shared.Kafka.Messages
{
    public class UserMessage
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public MessageType MessageType { get; set; }
    }
}

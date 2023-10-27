using Nutrition.Domain.Entities;
using Nutrition.Domain.Interfaces.IRepositories;
using Shared.Kafka.Consumer;
using Shared.Kafka.Enums;
using Shared.Kafka.Messages;

namespace Nutrition.Application.Events
{
    public class NutritionKafkaHandler : IKafkaHandler<string, UserMessage>
    {
        private readonly IUserRepository _userRepository;

        public NutritionKafkaHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(string key, UserMessage value)
        {
            var user = new User()
            {
                Id = value.Id,
                Username = value.Username
            };

            switch (value.MessageType)
            {
                case MessageType.Create:

                    _userRepository.Create(user);

                    await _userRepository.SaveChangesAsync();

                    break;
                case MessageType.Delete:

                    _userRepository.Delete(user);

                    await _userRepository.SaveChangesAsync();

                    break;
                default:
                    break;
            }
        }
    }
}

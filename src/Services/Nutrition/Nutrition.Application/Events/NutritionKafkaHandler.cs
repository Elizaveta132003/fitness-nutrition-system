using Microsoft.Extensions.Logging;
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
        private readonly ILogger<NutritionKafkaHandler> _logger;

        public NutritionKafkaHandler(IUserRepository userRepository, ILogger<NutritionKafkaHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
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

                    _logger.LogInformation("User is created");

                    break;
                case MessageType.Delete:

                    _userRepository.Delete(user);

                    await _userRepository.SaveChangesAsync();

                    _logger.LogInformation("User is removed");

                    break;
                default:
                    break;
            }
        }
    }
}

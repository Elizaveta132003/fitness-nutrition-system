using Shared.Kafka.Consumer;
using Shared.Kafka.Enums;
using Shared.Kafka.Messages;
using Workouts.DataAccess.Entities;
using Workouts.DataAccess.Repositories.Interfaces;

namespace Workouts.BusinessLogic.Events
{
    public class WorkoutsKafkaHandler : IKafkaHandler<string, UserMessage>
    {
        private readonly IUserRepository _userRepository;

        public WorkoutsKafkaHandler(IUserRepository userRepository)
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

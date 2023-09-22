using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Task CreateAsync(User user);
        public void Update(User user);
        public void Delete(User user);
    }
}

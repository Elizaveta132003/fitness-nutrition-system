using Nutrition.Domain.Entities;

namespace Nutrition.Domain.Interfaces.IRepositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
        public Task<bool> UserExistsAsync(string name, CancellationToken cancellationToken=default);
    }
}

using Calories.DataAccess.Entities;
using Calories.DataAccess.Repositories.Interfaces;
using MongoDB.Driver;

namespace Calories.DataAccess.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _mongoCollection;

        public BaseRepository(IMongoDatabase database)
        {
            _mongoCollection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _mongoCollection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Eq(entity => entity.Id, id);

            await _mongoCollection.DeleteOneAsync(filter, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _mongoCollection.Find(Builders<T>.Filter.Empty).ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _mongoCollection.FindAsync(Builders<T>.Filter.Eq(entity => entity.Id, id),
                cancellationToken: cancellationToken);

            return await entity.FirstOrDefaultAsync(cancellationToken);
        }


        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<T>.Filter.Eq(updateEntity => updateEntity.Id, entity.Id);

            await _mongoCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
        }
    }
}

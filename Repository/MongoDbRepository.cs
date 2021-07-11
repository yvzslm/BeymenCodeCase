using Core;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class MongoDbRepository<T> : IMongoDbRepository<T>
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbRepository(MongoDbConnectionSettings mongodbConnectionSettings)
        {
            var client = new MongoClient(mongodbConnectionSettings.ConnectionString);
            var database = client.GetDatabase(mongodbConnectionSettings.DatabaseName);
            _collection = database.GetCollection<T>(mongodbConnectionSettings.CollectionName);
        }

        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(FilterDefinition<T> filter, T entity)
        {
            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task<T> Get(FilterDefinition<T> filter)
        {
            var result = await _collection.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter)
        {
            var result = await _collection.FindAsync(filter);
            return await result.ToListAsync();
        }
    }
}

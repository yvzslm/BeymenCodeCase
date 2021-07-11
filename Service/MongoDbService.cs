using MongoDB.Driver;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class MongoDbService<T> : IMongoDbService<T>
    {
        private readonly IMongoDbRepository<T> _mongoDbRepository;

        public MongoDbService(IMongoDbRepository<T> mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task Add(T entity)
        {
            await _mongoDbRepository.Add(entity);
        }

        public async Task Update(FilterDefinition<T> filter, T entity)
        {
            await _mongoDbRepository.Update(filter, entity);
        }

        public async Task<T> Get(FilterDefinition<T> filter)
        {
            return await _mongoDbRepository.Get(filter);
        }

        public async Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter)
        {
            return await _mongoDbRepository.GetAll(filter);
        }
    }
}

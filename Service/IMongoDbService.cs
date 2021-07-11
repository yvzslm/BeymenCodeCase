using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IMongoDbService<T>
    {
        Task Add(T entity);
        Task Update(FilterDefinition<T> filter, T entity);
        Task<T> Get(FilterDefinition<T> filter);
        Task<IEnumerable<T>> GetAll(FilterDefinition<T> filter);
    }
}

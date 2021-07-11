using Core;
using MongoDB.Driver;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationReader
{
    public class ConfigurationReader : IConfigurationReader
    {
        private static Dictionary<string, object> _configValues;
        private readonly IMongoDbRepository<ConfigurationModel> _mongoDbRepository;
        private readonly string _applicationName;
        private readonly int _refreshTimerIntervalInMs;
        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            _configValues = new Dictionary<string, object>();
            var settings = new MongoDbConnectionSettings()
            {
                ConnectionString = connectionString,
                DatabaseName = "ConfigDb",
                CollectionName = "Configs"
            };

            _mongoDbRepository = new MongoDbRepository<ConfigurationModel>(settings);
            _applicationName = applicationName;
            _refreshTimerIntervalInMs = refreshTimerIntervalInMs;
        }

        public async Task<T> GetValue<T>(string key)
        {
            var eqFilterName = Builders<ConfigurationModel>.Filter.Eq(x => x.Name, key);
            var eqFilterApplicationName = Builders<ConfigurationModel>.Filter.Eq(x => x.ApplicationName, _applicationName);
            var eqFilterIsActive = Builders<ConfigurationModel>.Filter.Eq(x => x.IsActive, true);
            var filter = Builders<ConfigurationModel>.Filter.And(eqFilterName, eqFilterApplicationName, eqFilterIsActive);

            try
            {
                var config = await _mongoDbRepository.Get(filter);

                if (config != null)
                {
                    if (_configValues.ContainsKey(key))
                        _configValues[key] = config.Value;
                    else
                        _configValues.Add(key, config.Value);

                    return (T)Convert.ChangeType(config.Value, typeof(T));
                }
                else
                {
                    return GetLastConfigValue();
                }
            }
            catch (Exception)
            {
                return GetLastConfigValue();
            }

            T GetLastConfigValue()
            {
                if (_configValues.ContainsKey(key))
                    return (T)_configValues[key];

                throw new Exception("Config değeri bulunamadı.");
            }
        }
    }
}

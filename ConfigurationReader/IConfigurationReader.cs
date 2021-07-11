using System.Threading.Tasks;

namespace ConfigurationReader
{
    public interface IConfigurationReader
    {
        Task<T> GetValue<T>(string key);
    }
}

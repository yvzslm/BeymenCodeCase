using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationReader
{
    public static class ServiceCollectionExtension
    {
        public static void AddConfigurationReader(this IServiceCollection services, string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            services.AddSingleton<IConfigurationReader>(new ConfigurationReader(applicationName,
                                                                                connectionString,
                                                                                refreshTimerIntervalInMs));
        }
    }
}

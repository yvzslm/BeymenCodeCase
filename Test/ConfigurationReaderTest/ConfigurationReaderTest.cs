using System.Threading.Tasks;
using Xunit;

namespace ConfigurationReaderTest
{
    public class ConfigurationReaderTest
    {
        [InlineData("SERVICE-A", "boyner.com.tr")]
        [Theory]
        public async Task Should_Get_Expected_SiteName(string applicationName, string expectedSiteName)
        {
            var configReader = new ConfigurationReader.ConfigurationReader(applicationName, 
                                                                           "mongodb://localhost:27017",
                                                                           5000);

            var siteName = await configReader.GetValue<string>("SiteName");

            Assert.Equal(expectedSiteName, siteName);
        }
    }
}

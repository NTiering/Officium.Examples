using Microsoft.Extensions.Configuration;

namespace fnTokens.Settings
{
    public sealed class AppSettings : IAppSettings
    {

        public AppSettings(IConfiguration configuration)
        {
            DefaultExpiryInMins = int.Parse(configuration["DefaultExpiryInMins"]);
        }

        public int DefaultExpiryInMins { get; private set; }
    }
}
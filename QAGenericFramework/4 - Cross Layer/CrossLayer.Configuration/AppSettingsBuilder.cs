using Microsoft.Extensions.Configuration;

namespace CrossLayer.Configuration
{
    public static class AppSettingsBuilder
    {
        public static AppSettings GetConfiguration(IConfigurationRoot configurationRoot)
        {
            var configuration = new AppSettings
            {
                ExecutionType = configurationRoot.GetSection(nameof(ExecutionType)).Get<ExecutionType>(),
                AppConfiguration = configurationRoot.GetSection(nameof(AppConfiguration)).Get<AppConfiguration>(),
                ConnectionStrings = configurationRoot.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>(),
                SauceLabs = configurationRoot.GetSection(nameof(SauceLabs)).Get<SauceLabs>()
            };

            return configuration;
        }
    }
}

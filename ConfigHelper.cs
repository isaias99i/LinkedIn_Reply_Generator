using Microsoft.Extensions.Configuration;

namespace LinkedInScraperOpenAI;

public static class ConfigHelper
{
    public static IConfiguration LoadConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace NorthwindApi.Dapper.Tests
{
    public static class LoggerMocker
    {
        public static ILogger<T> GetLogger<T>()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger<T>();

            return serviceProvider.GetService<ILoggerFactory>().CreateLogger<T>();
        }
    }
}

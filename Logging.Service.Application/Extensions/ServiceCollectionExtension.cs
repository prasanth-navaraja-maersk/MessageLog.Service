using Microsoft.Extensions.DependencyInjection;

namespace Logging.Service.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddLoggingService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMessageLogHandler, MessageLogHandler>();
        serviceCollection.AddScoped<IErrorLogHandler, ErrorLogHandler>();
    }
}
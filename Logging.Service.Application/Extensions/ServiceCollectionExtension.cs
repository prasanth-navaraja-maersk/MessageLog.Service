using Microsoft.Extensions.DependencyInjection;

namespace Logging.Service.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddLoggingService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMessageLogDocumentHandler, MessageLogDocumentHandler>();
        serviceCollection.AddScoped<IErrorLogDocumentHandler, ErrorLogDocumentHandler>();
        serviceCollection.AddScoped<IMessageLogHandler, MessageLogHandler>();
        //serviceCollection.AddScoped<IErrorLogDocHandler, ErrorLogDocHandler>();
    }
}
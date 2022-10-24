using Finance.Common.Database.Relational.Extensions;
using MessageLog.Infrastructure;
using MessageLog.Service.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;

namespace MessageLog.Service.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddLoggingService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ILoggingService, LoggingService>();
        serviceCollection.AddScoped<IMessageLogRepository, MessageLogRepository>();
        serviceCollection.AddScopedPersistence<LoggingContext>();
        serviceCollection.AddQueryExecution<LoggingContext, PostgresCompiler>();
    }
}
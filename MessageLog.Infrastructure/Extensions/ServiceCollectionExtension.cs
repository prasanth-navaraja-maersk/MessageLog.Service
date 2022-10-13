using Finance.Common.Database.Relational.Extensions;
using MessageLog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;

namespace MessageLog.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddLoggingService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMessageLogRepository, MessageLogRepository>();
        serviceCollection.AddScopedPersistence<LoggingContext>();
        serviceCollection.AddQueryExecution<LoggingContext, PostgresCompiler>();
    }
}
using Finance.Common.Database.Relational.Extensions;
using MessageLog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;

namespace MessageLog.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddLoggingRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScopedPersistence<LoggingContext>();
        serviceCollection.AddQueryExecution<LoggingContext, PostgresCompiler>();
        serviceCollection.AddScoped<IMessageLogDocumentRepository, MessageLogDocumentRepository>();
        serviceCollection.AddScoped<IErrorLogDocumentRepository, ErrorLogDocumentRepository>();
        serviceCollection.AddScoped<IMessageLogRepository, MessageLogRepository>();
        //serviceCollection.AddScoped<IErrorLogDocRepository, ErrorLogDocRepository>();
    }
}
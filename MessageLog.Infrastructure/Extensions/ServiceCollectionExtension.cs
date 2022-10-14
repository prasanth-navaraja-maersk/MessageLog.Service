using Finance.Common.Database.Relational.Extensions;
using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Interfaces.Repositories;
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
        serviceCollection.AddScoped<IMessageLogRepository, MessageLogRepository>();
        //serviceCollection.AddScoped<IAsyncRepository<Entities.MessageLog, LoggingContext, long>,
        //    MessageLogRepository>();
        //serviceCollection.AddScoped<IUnitOfWork<LoggingContext>>();
    }
}
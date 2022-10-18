namespace MessageLog.Infrastructure.Repositories;

public interface IMessageLogDocRepository
{
    Task<long> UpsertMessageLogDocsAsync(Entities.MessageLogDoc messageLog, CancellationToken cancellationToken);
}
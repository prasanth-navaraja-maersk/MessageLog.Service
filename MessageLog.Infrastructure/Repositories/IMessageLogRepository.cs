namespace MessageLog.Infrastructure.Repositories;

public interface IMessageLogRepository
{
    Task<long> UpsertMessageLogs(Entities.MessageLog messageLog, CancellationToken cancellationToken);
}
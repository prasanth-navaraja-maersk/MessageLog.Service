namespace MessageLog.Infrastructure.Repositories;

public interface IMessageLogRepository
{
    Task<long> UpsertMessageLogsAsync(Entities.MessageLog messageLog, CancellationToken cancellationToken);
    Task<IEnumerable<Entities.MessageLog>> GetMessageLogsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Entities.MessageLog>> GetMessageLogsByMessageTypeAsync(string messageType,
        CancellationToken cancellationToken);

    void ClearMessageLogs();
}
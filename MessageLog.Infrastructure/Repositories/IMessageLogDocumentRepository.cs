namespace MessageLog.Infrastructure.Repositories;

public interface IMessageLogDocumentRepository
{
    Task<long> UpsertMessageLogs(Entities.MessageLogDocument messageLogDocument, CancellationToken cancellationToken);

    Task<IEnumerable<Entities.MessageLogDocument>> GetMessageLogsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<Entities.MessageLogDocument>> GetMessageLogsByMessageTypeAsync(string messageType,
        CancellationToken cancellationToken);

    void ClearMessageLogDocuments();
}
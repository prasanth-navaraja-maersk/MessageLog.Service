using MessageLog.Infrastructure.Entities;

namespace MessageLog.Infrastructure.Repositories;

public interface IMessageLogDocRepository
{
    Task<long> UpsertMessageLogDocsAsync(MessageLogDoc messageLog, CancellationToken cancellationToken);
    Task<IEnumerable<MessageLogDoc>> GetMessageLogDocsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<MessageLogDoc>> GetMessageLogDocsByMessageTypeAsync(string messageType,
        CancellationToken cancellationToken);
}
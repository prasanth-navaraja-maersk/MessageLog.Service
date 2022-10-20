using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Entities;

namespace Logging.Service.Application;

public interface IMessageLogDocHandler
{
    Task<long> UpsertMessageLogDocAsync(MessageLogDocRequest messageLogRequest, CancellationToken cancellationToken);
    Task<IEnumerable<MessageLogDoc>> GetMessageLogDocAsync(CancellationToken cancellationToken);

    Task<IEnumerable<MessageLogDoc>> GetMessageLogDocsByMessageTypeAsync(string messageType,
        CancellationToken cancellationToken);

    void ClearMessageLogDocs();
}
using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IMessageLogHandler
{
    Task<long> UpsertMessageLogsAsync(MessageLogRequest messageLogRequest, CancellationToken cancellationToken);
    Task<IEnumerable<MessageLog.Infrastructure.Entities.MessageLog>> GetMessageLogsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<MessageLog.Infrastructure.Entities.MessageLog>> GetMessageLogsByMessageTypeAsync(string messageType,
        CancellationToken cancellationToken);

    void ClearMessageLogs();
}
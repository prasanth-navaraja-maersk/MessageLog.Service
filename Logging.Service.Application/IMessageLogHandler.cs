using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IMessageLogHandler
{
    Task<long> UpsertMessageLog(MessageLogRequest messageLogRequest, CancellationToken cancellationToken);

    Task<IEnumerable<MessageLogRequest>> GetMessageLogsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<MessageLogRequest>> GetMessageLogsByMessageTypeAsync(string messageType,
        CancellationToken cancellationToken);
}
using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Repositories;

namespace Logging.Service.Application;

public class MessageLogHandler : IMessageLogHandler
{
    private readonly IMessageLogRepository _messageLogRepository;

    public MessageLogHandler(IMessageLogRepository messageLogRepository)
    {
        _messageLogRepository = messageLogRepository;
    }

    public async Task<long> UpsertMessageLog(MessageLogRequest messageLogRequest, CancellationToken cancellationToken)
    {
        var messageLog = new MessageLog.Infrastructure.Entities.MessageLog
        {
            MessageId = messageLogRequest.MessageId,
            MessageType = messageLogRequest.MessageType,
            MessageLogs = messageLogRequest.MessageLogs
        };

        return await _messageLogRepository.UpsertMessageLogs(messageLog, cancellationToken);
    }
}
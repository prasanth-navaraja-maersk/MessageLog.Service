using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Repositories;

namespace Logging.Service.Application;

public class MessageLogHandler : IMessageLogHandler
{
    private readonly IMessageLogRepository _repository;

    public MessageLogHandler(IMessageLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> UpsertMessageLogsAsync(MessageLogRequest messageLogRequest, CancellationToken cancellationToken)
    {
        var messageLog = new MessageLog.Infrastructure.Entities.MessageLog
        {
            CorrelationId = messageLogRequest.MessageLog.CorrelationId,
            ExternalIdentifier = messageLogRequest.MessageLog.ExternalIdentifier,
            MessageType = messageLogRequest.MessageLog.MessageType,
            Status = messageLogRequest.MessageLog.Status,
            Stage = messageLogRequest.MessageLog.Stage,
            Source = messageLogRequest.MessageLog.Source,
            Destination = messageLogRequest.MessageLog.Destination,
            Retries = messageLogRequest.MessageLog.Retries,
            SystemCreateDate = messageLogRequest.MessageLog.SystemCreateDate,
            SystemModifiedDate = messageLogRequest.MessageLog.SystemModifiedDate,
            IsError = messageLogRequest.MessageLog.IsError
        };
        
        return await _repository.UpsertMessageLogsAsync(messageLog, cancellationToken);
    }

    public async Task<IEnumerable<MessageLog.Infrastructure.Entities.MessageLog>> GetMessageLogsAsync(CancellationToken cancellationToken) 
        => await _repository.GetMessageLogsAsync(cancellationToken);
    
    public async Task<IEnumerable<MessageLog.Infrastructure.Entities.MessageLog>> GetMessageLogsByMessageTypeAsync(string messageType, CancellationToken cancellationToken) 
        => await _repository.GetMessageLogsByMessageTypeAsync(messageType, cancellationToken);

    public void ClearMessageLogs() 
        => _repository.ClearMessageLogs();
}
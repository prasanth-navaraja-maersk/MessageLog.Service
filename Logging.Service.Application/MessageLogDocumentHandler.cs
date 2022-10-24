using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Repositories;

namespace Logging.Service.Application;

public class MessageLogDocumentHandler : IMessageLogDocumentHandler
{
    private readonly IMessageLogDocumentRepository _messageLogRepository;

    public MessageLogDocumentHandler(IMessageLogDocumentRepository messageLogRepository)
    {
        _messageLogRepository = messageLogRepository;
    }

    public async Task<long> UpsertMessageLogDocuments(MessageLogDocumentRequest messageLogRequest, CancellationToken cancellationToken)
    {
        var messageLog = new MessageLog.Infrastructure.Entities.MessageLogDocument
        {
            CorrelationId = messageLogRequest.MessageId,
            MessageType = messageLogRequest.MessageType,
            MessageLogDocuments = messageLogRequest.MessageLogs
        };

        return await _messageLogRepository.UpsertMessageLogs(messageLog, cancellationToken);
    }

    public async Task<IEnumerable<MessageLogDocumentRequest>> GetMessageLogDocumentsAsync(CancellationToken cancellationToken)
    {
        var messageLogs = await _messageLogRepository.GetMessageLogsAsync(cancellationToken);
        return messageLogs.Select(x => new MessageLogDocumentRequest()
        {
            MessageId = x.CorrelationId,
            MessageType = x.MessageType,
            MessageLogs = x.MessageLogDocuments,
        }).ToList();
    }

    public async Task<IEnumerable<MessageLogDocumentRequest>> GetMessageLogDocumentsByMessageTypeAsync(string messageType, CancellationToken cancellationToken)
    {
        var messageLogs = await _messageLogRepository.GetMessageLogsByMessageTypeAsync(messageType, cancellationToken);
        return messageLogs.Select(x => new MessageLogDocumentRequest()
        {
            MessageId = x.CorrelationId,
            MessageType = x.MessageType,
            MessageLogs = x.MessageLogDocuments,
        }).ToList();
    }

    public void ClearMessageLogDocuments()
        => _messageLogRepository.ClearMessageLogDocuments();
}
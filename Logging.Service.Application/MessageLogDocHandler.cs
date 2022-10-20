using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Entities;
using MessageLog.Infrastructure.Repositories;

namespace Logging.Service.Application;

public class MessageLogDocHandler : IMessageLogDocHandler
{
    private readonly IMessageLogDocRepository _repository;

    public MessageLogDocHandler(IMessageLogDocRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> UpsertMessageLogDocAsync(MessageLogDocRequest messageLogRequest, CancellationToken cancellationToken)
    {
        var messageLog = new MessageLogDoc
        {
            MessageId = messageLogRequest.MessageLogDoc.MessageId,
            MessageType = messageLogRequest.MessageLogDoc.MessageType,
            Status = messageLogRequest.MessageLogDoc.Status,
            Stage = messageLogRequest.MessageLogDoc.Stage,
            Source = messageLogRequest.MessageLogDoc.Source,
            Destination = messageLogRequest.MessageLogDoc.Destination,
            Retries = messageLogRequest.MessageLogDoc.Retries,
            SystemCreateDate = messageLogRequest.MessageLogDoc.SystemCreateDate,
            SystemModifiedDate = messageLogRequest.MessageLogDoc.SystemModifiedDate,
            IsError = messageLogRequest.MessageLogDoc.IsError
        };
        
        return await _repository.UpsertMessageLogDocsAsync(messageLog, cancellationToken);
    }

    public async Task<IEnumerable<MessageLogDoc>> GetMessageLogDocAsync(CancellationToken cancellationToken) 
        => await _repository.GetMessageLogDocsAsync(cancellationToken);
    
    public async Task<IEnumerable<MessageLogDoc>> GetMessageLogDocsByMessageTypeAsync(string messageType, CancellationToken cancellationToken) 
        => await _repository.GetMessageLogDocsByMessageTypeAsync(messageType, cancellationToken);

    public void ClearMessageLogDocs() 
        => _repository.ClearMessageLogDocs();
}
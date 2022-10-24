using Logging.Service.Application;
using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Service.API.Controllers;

[ApiController]
[Route("MessageLogs")]
public class MessageLogController : ControllerBase
{
    private readonly ILogger<MessageLogController> _logger;
    private readonly IMessageLogHandler _messageLogHandler;

    public MessageLogController(ILogger<MessageLogController> logger, IMessageLogHandler messageLogHandler)
    {
        _logger = logger;
        _messageLogHandler = messageLogHandler;
    }

    [HttpPost(Name = "UpsertDocs")]
    public async Task<long> UpsertDocsAsync(MessageLogRequest messageLogDocRequest, CancellationToken cancellationToken)
    {
        return await _messageLogHandler.UpsertMessageLogsAsync(messageLogDocRequest, cancellationToken);
    }
    
    [HttpGet]
    public async Task<IEnumerable<MessageLog.Infrastructure.Entities.MessageLog>> GetMessageLogsAsync(CancellationToken cancellationToken)
        => await _messageLogHandler.GetMessageLogsAsync(cancellationToken);
    
    [HttpGet]
    [Route("MessageType")]
    public async Task<IEnumerable<MessageLog.Infrastructure.Entities.MessageLog>> GetMessageLogsByMessageTypeAsync(string messageType, CancellationToken cancellationToken) 
        => await _messageLogHandler.GetMessageLogsByMessageTypeAsync(messageType, cancellationToken);
    
    [HttpDelete]
    public void DeleteMessageLogsAsync() 
        => _messageLogHandler.ClearMessageLogs();
}
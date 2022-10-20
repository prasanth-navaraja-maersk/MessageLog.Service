using Logging.Service.Application;
using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Service.API.Controllers;

[ApiController]
[Route("MessageLogDocs")]
public class MessageLogDocController : ControllerBase
{
    private readonly ILogger<MessageLogDocController> _logger;
    private readonly IMessageLogDocHandler _messageLogHandler;

    public MessageLogDocController(ILogger<MessageLogDocController> logger, IMessageLogDocHandler messageLogHandler)
    {
        _logger = logger;
        _messageLogHandler = messageLogHandler;
    }

    [HttpPost(Name = "UpsertDocs")]
    public async Task<long> UpsertDocsAsync(MessageLogDocRequest messageLogDocRequest, CancellationToken cancellationToken)
    {
        return await _messageLogHandler.UpsertMessageLogDocAsync(messageLogDocRequest, cancellationToken);
    }
    
    [HttpGet]
    public async Task<IEnumerable<MessageLogDoc>> GetMessageLogDocsAsync(CancellationToken cancellationToken)
        => await _messageLogHandler.GetMessageLogDocAsync(cancellationToken);
    
    [HttpGet]
    [Route("MessageType")]
    public async Task<IEnumerable<MessageLogDoc>> GetMessageLogDocsByMessageTypeAsync(string messageType, CancellationToken cancellationToken) 
        => await _messageLogHandler.GetMessageLogDocsByMessageTypeAsync(messageType, cancellationToken);
    
    [HttpDelete]
    //[Route("Delete")]
    public void DeleteMessageLogDocsAsync() 
        => _messageLogHandler.ClearMessageLogDocs();
}
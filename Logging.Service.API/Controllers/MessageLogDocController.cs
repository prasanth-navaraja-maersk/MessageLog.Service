using Logging.Service.Application;
using Logging.Service.Application.Requests;
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
    public async Task<long> UpsertDocs(MessageLogDocRequest messageLogDocRequest, CancellationToken cancellationToken)
    {
        return await _messageLogHandler.UpsertMessageLogDoc(messageLogDocRequest, cancellationToken);
    }
}
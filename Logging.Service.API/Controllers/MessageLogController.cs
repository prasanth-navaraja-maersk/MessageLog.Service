using Logging.Service.Application;
using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Service.API.Controllers
{
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

        [HttpPost(Name = "Upsert")]
        public async Task<long> Upsert(MessageLogRequest messageLogRequest)
        {
            return await _messageLogHandler.UpsertMessageLog(messageLogRequest);
        }
    }
}
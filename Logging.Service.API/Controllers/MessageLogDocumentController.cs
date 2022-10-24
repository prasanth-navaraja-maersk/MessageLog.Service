using Logging.Service.Application;
using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Service.API.Controllers
{
    [ApiController]
    [Route("MessageLogDocuments")]
    public class MessageLogDocumentController : ControllerBase
    {
        private readonly ILogger<MessageLogDocumentController> _logger;
        private readonly IMessageLogHandler _messageLogHandler;

        public MessageLogDocumentController(ILogger<MessageLogDocumentController> logger, IMessageLogHandler messageLogHandler)
        {
            _logger = logger;
            _messageLogHandler = messageLogHandler;
        }

        [HttpPost(Name = "Upsert")]
        public async Task<long> Upsert(MessageLogRequest messageLogRequest, CancellationToken cancellationToken)
        {
            return await _messageLogHandler.UpsertMessageLog(messageLogRequest, cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<MessageLogRequest>> GetMessageLogsAsync(CancellationToken cancellationToken)
        {
            return await _messageLogHandler.GetMessageLogsAsync(cancellationToken);
        }

        [HttpGet]
        [Route("/MessageLogDocuments/MessageType")]
        public async Task<IEnumerable<MessageLogRequest>> GetMessageLogsByMessageTypeAsync(string messageType, CancellationToken cancellationToken)
        {
            return await _messageLogHandler.GetMessageLogsAsync(cancellationToken);
        }

        [HttpDelete]
        public void DeleteMessageLogsAsync()
            => _messageLogHandler.ClearMessageLogs();
    }
}
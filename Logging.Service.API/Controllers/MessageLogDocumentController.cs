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
        private readonly IMessageLogDocumentHandler _messageLogDocumentHandler;

        public MessageLogDocumentController(ILogger<MessageLogDocumentController> logger, IMessageLogDocumentHandler messageLogDocumentHandler)
        {
            _logger = logger;
            _messageLogDocumentHandler = messageLogDocumentHandler;
        }

        [HttpPost(Name = "Upsert")]
        public async Task<long> Upsert(MessageLogDocumentRequest messageLogRequest, CancellationToken cancellationToken)
        {
            return await _messageLogDocumentHandler.UpsertMessageLogDocuments(messageLogRequest, cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<MessageLogDocumentRequest>> GetMessageLogsAsync(CancellationToken cancellationToken)
        {
            return await _messageLogDocumentHandler.GetMessageLogDocumentsAsync(cancellationToken);
        }

        [HttpGet]
        [Route("/MessageLogDocuments/MessageType")]
        public async Task<IEnumerable<MessageLogDocumentRequest>> GetMessageLogsByMessageTypeAsync(string messageType, CancellationToken cancellationToken)
        {
            return await _messageLogDocumentHandler.GetMessageLogDocumentsAsync(cancellationToken);
        }

        [HttpDelete]
        public void DeleteMessageLogsAsync()
            => _messageLogDocumentHandler.ClearMessageLogDocuments();
    }
}
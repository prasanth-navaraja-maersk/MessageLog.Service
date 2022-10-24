using Logging.Service.Application;
using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Service.API.Controllers
{
    [ApiController]
    [Route("ErrorLogDocuments")]
    public class ErrorLogDocumentController : ControllerBase
    {
        private readonly ILogger<ErrorLogDocumentController> _logger;
        private readonly IErrorLogDocumentHandler _errorLogDocumentHandler;

        public ErrorLogDocumentController(ILogger<ErrorLogDocumentController> logger, IErrorLogDocumentHandler errorLogDocumentHandler)
        {
            _logger = logger;
            _errorLogDocumentHandler = errorLogDocumentHandler;
        }

        [HttpPost(Name = "UpsertErrorLogDocumentsAsync")]
        public async Task<long> Upsert(ErrorLogDocumentRequest errorLogRequest, CancellationToken cancellationToken)
        {
            return await _errorLogDocumentHandler.UpsertErrorLogDocumentsAsync(errorLogRequest, cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<ErrorLogDocumentRequest>> GetErrorLogsAsync(CancellationToken cancellationToken)
        {
            return await _errorLogDocumentHandler.GetErrorLogDocumentsAsync(cancellationToken);
        }

        [HttpGet]
        [Route("ErrorCategory")]
        public async Task<IEnumerable<ErrorLogDocumentRequest>> GetErrorLogsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken)
        {
            return await _errorLogDocumentHandler.GetErrorLogDocumentsByErrorCategoryAsync(errorCategory, cancellationToken);
        }

        [HttpDelete]
        public void DeleteErrorLogsAsync()
            => _errorLogDocumentHandler.ClearErrorLogDocuments();
    }
}
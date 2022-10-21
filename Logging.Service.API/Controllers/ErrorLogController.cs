using Logging.Service.Application;
using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Service.API.Controllers
{
    [ApiController]
    [Route("ErrorLogs")]
    public class ErrorLogController : ControllerBase
    {
        private readonly ILogger<ErrorLogController> _logger;
        private readonly IErrorLogHandler _errorLogHandler;

        public ErrorLogController(ILogger<ErrorLogController> logger, IErrorLogHandler errorLogHandler)
        {
            _logger = logger;
            _errorLogHandler = errorLogHandler;
        }

        [HttpPost(Name = "UpsertErrorLogs")]
        public async Task<long> Upsert(ErrorLogRequest errorLogRequest, CancellationToken cancellationToken)
        {
            return await _errorLogHandler.UpsertErrorLog(errorLogRequest, cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<ErrorLogRequest>> GetErrorLogsAsync(CancellationToken cancellationToken)
        {
            return await _errorLogHandler.GetErrorLogsAsync(cancellationToken);
        }

        [HttpDelete]
        public void DeleteErrorLogsAsync()
            => _errorLogHandler.ClearErrorLogs();
    }
}
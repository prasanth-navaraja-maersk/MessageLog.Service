using Logging.Service.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Service.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageLogController : ControllerBase
    {
        private readonly ILogger<MessageLogController> _logger;

        public MessageLogController(ILogger<MessageLogController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "MessageLogs")]
        public async Task<long> Insert(MessageLogRequest messageLogRequest)
        {
            return 1;
        }
    }
}
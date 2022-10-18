using MessageLog.Infrastructure.Entities;

namespace Logging.Service.Application.Requests;

public class ErrorLogDocRequest
{
    public ErrorLogDoc ErrorLogDoc { get; set; }
}
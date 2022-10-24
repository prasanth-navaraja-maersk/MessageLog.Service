using MessageLog.Infrastructure.Entities;

namespace Logging.Service.Application.Requests;

public class ErrorLogRequest
{
    public ErrorLog ErrorLog { get; set; }
}
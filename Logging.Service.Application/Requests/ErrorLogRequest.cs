using System.Text.Json;

namespace Logging.Service.Application.Requests;

public class ErrorLogRequest
{
    public string LogMessageId { get; set; }
    public string LogMessageType { get; set; }
    public JsonDocument ErrorLogs { get; set; }
}
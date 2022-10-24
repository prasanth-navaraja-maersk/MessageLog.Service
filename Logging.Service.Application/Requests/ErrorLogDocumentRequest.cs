using System.Text.Json;

namespace Logging.Service.Application.Requests;

public class ErrorLogDocumentRequest
{
    public string LogMessageId { get; set; }
    public string LogMessageType { get; set; }
    public JsonDocument ErrorLogDocuments { get; set; }
}
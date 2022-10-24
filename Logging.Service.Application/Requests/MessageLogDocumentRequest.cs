using System.Text.Json;

namespace Logging.Service.Application.Requests;

public class MessageLogDocumentRequest
{
    public string MessageId { get; set; }
    public string MessageType { get; set; }
    public JsonDocument MessageLogs { get; set; }
}
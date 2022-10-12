using System.Text.Json;

namespace MessageLog.Infrastructure;

public class MessageLog
{
    public long Id { get; set; }
    public string MessageId { get; set; }
    public string MessageType { get; set; }
    
    //public MessageLogDoc MessageLogs { get; set; } //ToDo
    
    public JsonDocument MessageLogs { get; set; }

    public DateTime? SystemCreateDate { get; set; }

    public void Dispose() => MessageLogs?.Dispose();
}
using System.Text.Json;
using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Service.Entities;

public class MessageLog : IEntity<int>, ISystemCreateDate, ISystemModifiedDate
{
    public int Id { get; set; }
    public string MessageId { get; set; }
    public string MessageType { get; set; }
    
    //public MessageLogDoc MessageLogs { get; set; } //ToDo
    
    public JsonDocument MessageLogs { get; set; }

    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; set; }

    public void Dispose() => MessageLogs?.Dispose();
}
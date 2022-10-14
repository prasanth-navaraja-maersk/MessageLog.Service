using System.Text.Json;
using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Infrastructure.Entities;

public class MessageLog : IEntity<long>, ISystemCreateDate, ISystemModifiedDate
{
    public long Id { get; set; }
    public string MessageId { get; set; }
    public string MessageType { get; set; }

    //public MessageLogDoc MessageLogs { get; set; } //ToDo

    public JsonDocument MessageLogs { get; set; }

    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; set; }

    public void Dispose() => MessageLogs?.Dispose();
}
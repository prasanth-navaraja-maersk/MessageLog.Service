using System.Text.Json;
using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Infrastructure.Entities;

public class MessageLogDocument : IEntity<long>, ISystemCreateDate, ISystemModifiedDate
{
    public long Id { get; set; }
    public string CorrelationId { get; set; }
    public string MessageType { get; set; }

    public JsonDocument MessageLogDocuments { get; set; }

    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; set; }

    public void Dispose() => MessageLogDocuments?.Dispose();
}
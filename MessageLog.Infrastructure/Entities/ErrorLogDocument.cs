using System.Text.Json;
using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Infrastructure.Entities;

public class ErrorLogDocument : IEntity<long>, ISystemCreateDate, ISystemModifiedDate
{
    public long Id { get; set; }
    public string  LogMessageId { get; set; }
    public string  LogMessageType { get; set; }
    public JsonDocument ErrorLogDocuments { get; set; }
    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; set; }

    public void Dispose() => ErrorLogDocuments?.Dispose();

}
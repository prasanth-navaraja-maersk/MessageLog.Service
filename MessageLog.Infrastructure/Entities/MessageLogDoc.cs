using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Infrastructure.Entities;

public class MessageLogDoc : IEntity<long>, ISystemCreateDate, ISystemModifiedDate
{
    public string MessageId { get; set; }
    public string MessageType { get; set; }
    public string Status { get; set; }
    public string Stage { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
    public int Retries { get; set; } = 0;

    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; set; }
    public bool IsError { get; set; }
    public long Id { get; set; }
}
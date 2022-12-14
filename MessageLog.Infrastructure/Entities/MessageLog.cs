using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Infrastructure.Entities;

public class MessageLog : IEntity<long>, ISystemCreateDate, ISystemModifiedDate
{
    public long Id { get; set; }
    public long? CorrelationId { get; set; }
    public string? ExternalIdentifier { get; set; }
    public string MessageType { get; set; }
    public string? Metadata { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }
    public string Status { get; set; }
    public string Stage { get; set; }
    public bool IsError { get; set; }
    public int Retries { get; set; } = 0;
    public string? BlobUrl { get; set; }
    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; set; }
}
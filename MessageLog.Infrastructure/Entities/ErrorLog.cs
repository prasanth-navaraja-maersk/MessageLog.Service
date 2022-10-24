using Finance.Common.Database.Relational.Interfaces.Entities;

namespace MessageLog.Infrastructure.Entities;

public class ErrorLog : IEntity<long>, ISystemCreateDate
{
    public long Id { get; set; }
    public long? CorrelationId { get; set; }
    public string? ExternalIdentifier { get; set; }
    public string ErrorCategory { get; set; }
    public string ErrorMessage { get; set; }
    public string? BlobUrl { get; set; }
    public DateTime? SystemCreateDate { get; set; }
}
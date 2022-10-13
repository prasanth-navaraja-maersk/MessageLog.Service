namespace MessageLog.Service.Entities;

public class ErrorLog
{
    public long Id { get; set; }
    public int? ExternalIdentifier { get; set; }
    public ErrorLogDoc ErrorLogs { get; set; } //ToDo
    public DateTime? SystemCreateDate { get; set; }
}
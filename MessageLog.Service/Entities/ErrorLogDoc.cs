namespace MessageLog.Service.Entities;

public class ErrorLogDoc
{
    public long LogMessageId { get; set; }
    public string ExternalInvoiceId { get; set; }
    public string ErrorMessage { get; set; }
    public string Category { get; set; }
    public DateTime? SystemCreateDate { get; set; }
}
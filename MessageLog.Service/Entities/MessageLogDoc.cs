namespace MessageLog.Service.Entities;

public class MessageLogDoc
{
    public string StandardAlphaCarrierCode { get; set; }
    public string CustomerCode { get; set; }
    public string VendorName { get; set; }
    public string InvoiceNumber { get; set; }

    public string Status { get; set; }
    public int Retries { get; set; } = 0;

    public DateTime? SystemCreateDate { get; set; }
    public DateTime? SystemModifiedDate { get; set; }
    public bool IsError { get; set; }

    public string Stage { get; set; }
    public string Source { get; set; }
    public string Destination { get; set; }

}
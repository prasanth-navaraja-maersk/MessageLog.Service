using MessageLog.Infrastructure.Entities;

namespace Logging.Service.Application.Requests;

public class MessageLogRequest
{
    public MessageLog.Infrastructure.Entities.MessageLog MessageLog { get; set; }
}
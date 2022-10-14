using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IMessageLogHandler
{
    Task<long> UpsertMessageLog(MessageLogRequest messageLogRequest);
}
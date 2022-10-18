using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IMessageLogDocHandler
{
    Task<long> UpsertMessageLogDoc(MessageLogDocRequest messageLogRequest, CancellationToken cancellationToken);
}
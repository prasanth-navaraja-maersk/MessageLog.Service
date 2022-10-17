using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IErrorLogHandler
{
    Task<long> UpsertErrorLog(ErrorLogRequest errorLogRequest, CancellationToken cancellationToken);
}
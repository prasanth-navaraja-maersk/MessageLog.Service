using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IErrorLogHandler
{
    Task<long> UpsertErrorLog(ErrorLogRequest errorLogRequest, CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLogRequest>> GetErrorLogsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLogRequest>> GetErrorLogsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken);
    void ClearErrorLogs();
}
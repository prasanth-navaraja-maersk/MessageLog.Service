using MessageLog.Infrastructure.Entities;

namespace MessageLog.Infrastructure.Repositories;

public interface IErrorLogRepository
{
    Task<long> UpsertErrorLogs(ErrorLog errorLog, CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLog>> GetErrorLogsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLog>> GetErrorLogsByErrorCategoryAsync(string messageType, CancellationToken cancellationToken);
    void ClearErrorLogs();
}
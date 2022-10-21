using MessageLog.Infrastructure.Entities;

namespace MessageLog.Infrastructure.Repositories;

public interface IErrorLogRepository
{
    Task<long> UpsertErrorLogs(ErrorLog errorLog, CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLog>> GetErrorLogsAsync(CancellationToken cancellationToken);
    void ClearErrorLogs();
    Task<IEnumerable<Entities.ErrorLog>> GetErrorLogsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken);
}
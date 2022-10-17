using MessageLog.Infrastructure.Entities;

namespace MessageLog.Infrastructure.Repositories;

public interface IErrorLogRepository
{
    Task<long> UpsertErrorLogs(ErrorLog errorLog, CancellationToken cancellationToken);
}
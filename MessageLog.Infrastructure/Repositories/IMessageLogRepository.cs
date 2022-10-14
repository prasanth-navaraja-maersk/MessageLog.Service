namespace MessageLog.Infrastructure.Repositories;

public interface IMessageLogRepository
{
    Task<long> UpsertMessageLogs(MessageLog messageLog);
}
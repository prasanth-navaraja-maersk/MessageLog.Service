namespace MessageLog.Infrastructure.Repositories;

public interface IMessageLogRepository
{
    long InsertMessageLogs(MessageLog messageLog);
}
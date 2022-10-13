namespace MessageLog.Service.Repositories;

public interface IMessageLogRepository
{
    long InsertMessageLogs(Entities.MessageLog messageLog);
}
namespace MessageLog.Service;

public interface ILoggingService
{
    long InsertMessageLogs(Entities.MessageLog messageLog);
}
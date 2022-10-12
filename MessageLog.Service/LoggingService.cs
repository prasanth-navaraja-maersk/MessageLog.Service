using MessageLog.Infrastructure;

public class LoggingService
{
    public long InsertMessageLogs(MessageLog.Infrastructure.MessageLog messageLog)
    {
        using var context = new LoggingContext();
        context.MessageLogs.Add(messageLog);
        context.SaveChanges();

        return context.MessageLogs
            .First(X => X.MessageId == messageLog.MessageId && X.MessageType == messageLog.MessageType).Id;
    }
}
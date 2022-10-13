using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using MessageLog.Service.Entities;

namespace MessageLog.Service;

public class LoggingService : AsyncRepository<Entities.MessageLog, LoggingContext, int>
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public LoggingService(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }
    //public long InsertMessageLogs(Entities.MessageLog messageLog)
    //{
    //    using var context = new LoggingContext();
    //    context.MessageLogs.Add(messageLog);
    //    context.SaveChanges();

    //    return context.MessageLogs
    //        .First(X => X.MessageId == messageLog.MessageId && X.MessageType == messageLog.MessageType).Id;
    //}

    public long InsertMessageLogs(Entities.MessageLog messageLog)
    {
        return _uow.DbContext.MessageLogs
            .First(X => X.MessageId == messageLog.MessageId && X.MessageType == messageLog.MessageType).Id;
    }
}
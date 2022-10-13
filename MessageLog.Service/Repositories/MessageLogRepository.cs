using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using MessageLog.Service.Entities;

namespace MessageLog.Service.Repositories;

public class MessageLogRepository : AsyncRepository<Entities.MessageLog, LoggingContext, int>, IMessageLogRepository
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public MessageLogRepository(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }

    public long InsertMessageLogs(Entities.MessageLog messageLog)
    {
        return _uow.DbContext.MessageLogs
            .First(X => X.MessageId == messageLog.MessageId && X.MessageType == messageLog.MessageType).Id;
    }
}
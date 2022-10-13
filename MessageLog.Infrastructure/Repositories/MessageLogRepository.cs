using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;

namespace MessageLog.Infrastructure.Repositories;

public class MessageLogRepository : AsyncRepository<MessageLog, LoggingContext, int>, IMessageLogRepository
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public MessageLogRepository(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }

    public long InsertMessageLogs(MessageLog messageLog)
    {
        return _uow.DbContext.MessageLogs
            .First(X => X.MessageId == messageLog.MessageId && X.MessageType == messageLog.MessageType).Id;
    }
}
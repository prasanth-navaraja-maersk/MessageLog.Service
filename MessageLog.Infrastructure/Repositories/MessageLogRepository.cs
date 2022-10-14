using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MessageLog.Infrastructure.Repositories;

public class MessageLogRepository : AsyncRepository<Entities.MessageLog, LoggingContext, long>, IMessageLogRepository
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public MessageLogRepository(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }

    public async Task<long> UpsertMessageLogs(Entities.MessageLog messageLog)
    {
        var log = await _uow.DbContext.MessageLogs
            .FirstOrDefaultAsync(X => X.MessageId == messageLog.MessageId && X.MessageType == messageLog.MessageType);
        return log!.Id;
    }
}
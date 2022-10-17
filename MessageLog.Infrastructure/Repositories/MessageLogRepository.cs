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

    public async Task<long> UpsertMessageLogs(Entities.MessageLog messageLog, CancellationToken cancellationToken)
    {
        long id = 0;
        var log = await _uow.DbContext.MessageLog
            .FirstOrDefaultAsync(x => x.MessageId == messageLog.MessageId 
                                      && x.MessageType == messageLog.MessageType, 
                cancellationToken);
        if (log == null)
        {
            var entity = await _uow.DbContext.MessageLog.AddAsync(messageLog, cancellationToken);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = entity.Entity.Id;
        }
        else
        {
            _uow.DbContext.MessageLog.Update(log);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = log!.Id;
        }

        return id;
    }
}
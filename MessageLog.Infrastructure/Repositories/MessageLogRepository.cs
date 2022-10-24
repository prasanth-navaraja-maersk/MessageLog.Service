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

    public async Task<long> UpsertMessageLogsAsync(Entities.MessageLog messageLog, CancellationToken cancellationToken)
    {
        long id;
        var log = await _uow.DbContext.MessageLogs
            .FirstOrDefaultAsync(x => x.CorrelationId == messageLog.CorrelationId
                                      && x.MessageType == messageLog.MessageType,
                cancellationToken);
        if (log == null)
        {
            var entity = await _uow.DbContext.MessageLogs.AddAsync(messageLog, cancellationToken);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = entity.Entity.Id;
        }
        else
        {
            _uow.DbContext.MessageLogs.Update(log);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = log!.Id;
        }

        return id;
    }

    public async Task<IEnumerable<Entities.MessageLog>> GetMessageLogsAsync(CancellationToken cancellationToken)
        => await _uow.DbContext.MessageLogs.ToListAsync(cancellationToken: cancellationToken);
    
    public async Task<IEnumerable<Entities.MessageLog>> GetMessageLogsByMessageTypeAsync(string messageType, CancellationToken cancellationToken)
        => await _uow.DbContext.MessageLogs
            .Where(x => x.MessageType == messageType)
            .ToListAsync(cancellationToken: cancellationToken);

    public void ClearMessageLogs()
    {
        _uow.DbContext.MessageLogs.RemoveRange(_uow.DbContext.MessageLogs);
        _uow.DbContext.SaveChanges();
    }
}
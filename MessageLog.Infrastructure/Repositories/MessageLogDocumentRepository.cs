using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MessageLog.Infrastructure.Repositories;

public class MessageLogDocumentRepository : AsyncRepository<Entities.MessageLogDocument, LoggingContext, long>, IMessageLogDocumentRepository
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public MessageLogDocumentRepository(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }

    public async Task<long> UpsertMessageLogs(Entities.MessageLogDocument messageLogDocument, CancellationToken cancellationToken)
    {
        long id = 0;
        var log = await _uow.DbContext.MessageLogDocuments
            .FirstOrDefaultAsync(x => x.CorrelationId == messageLogDocument.CorrelationId 
                                      && x.MessageType == messageLogDocument.MessageType, 
                cancellationToken);
        if (log == null)
        {
            var entity = await _uow.DbContext.MessageLogDocuments.AddAsync(messageLogDocument, cancellationToken);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = entity.Entity.Id;
        }
        else
        {
            _uow.DbContext.MessageLogDocuments.Update(log);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = log!.Id;
        }

        return id;
    }

    public async Task<IEnumerable<Entities.MessageLogDocument>> GetMessageLogsAsync(CancellationToken cancellationToken)
    {
        return await _uow.DbContext.MessageLogDocuments.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Entities.MessageLogDocument>> GetMessageLogsByMessageTypeAsync(string messageType, CancellationToken cancellationToken)
    {
        return await _uow.DbContext.MessageLogDocuments.Where(m=>m.MessageType == messageType).ToListAsync(cancellationToken);
    }

    public void ClearMessageLogDocuments()
    {
        _uow.DbContext.MessageLogDocuments.RemoveRange(_uow.DbContext.MessageLogDocuments);
        _uow.DbContext.SaveChanges();
    }
}
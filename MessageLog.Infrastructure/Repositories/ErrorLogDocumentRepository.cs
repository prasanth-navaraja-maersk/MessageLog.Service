using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using MessageLog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageLog.Infrastructure.Repositories;

public class ErrorLogDocumentRepository : AsyncRepository<ErrorLogDocument, LoggingContext, long>, IErrorLogDocumentRepository
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public ErrorLogDocumentRepository(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }

    public async Task<long> UpsertErrorLogDocumentsAsync(ErrorLogDocument errorLogDocument, CancellationToken cancellationToken)
    {
        long id = 0;
        var log = await _uow.DbContext.ErrorLogDocuments
            .FirstOrDefaultAsync(X => X.LogMessageId == errorLogDocument.LogMessageId
                                      && X.LogMessageType == errorLogDocument.LogMessageType, cancellationToken);
        if (log == null)
        {
            var entity = await _uow.DbContext.ErrorLogDocuments.AddAsync(errorLogDocument, cancellationToken);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = entity.Entity.Id;
        }
        else
        {
            _uow.DbContext.ErrorLogDocuments.Update(log);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = log!.Id;
        }

        return id;
    }

    public async Task<IEnumerable<Entities.ErrorLogDocument>> GetErrorLogDocumentsAsync(CancellationToken cancellationToken)
    {
        return await _uow.DbContext.ErrorLogDocuments.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ErrorLogDocument>> GetErrorLogDocumentsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken)
    {
        return _uow.DbContext.ErrorLogDocuments
            .Where(e => e.ErrorLogDocuments.RootElement.GetProperty("ErrorCategory").GetString() == errorCategory)
            .ToList();
        //return await _uow.DbContext.ErrorLogDocuments.ToListAsync(cancellationToken);
        //return await _uow.DbContext.ErrorLogDocuments.Where(m => m.MessageType == messageType).ToListAsync(cancellationToken);
    }

    public void ClearErrorLogDocuments()
    {
        _uow.DbContext.ErrorLogDocuments.RemoveRange(_uow.DbContext.ErrorLogDocuments);
        _uow.DbContext.SaveChanges();
    }
}
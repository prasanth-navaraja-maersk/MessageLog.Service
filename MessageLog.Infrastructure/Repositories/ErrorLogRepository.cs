using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using MessageLog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageLog.Infrastructure.Repositories;

public class ErrorLogRepository : AsyncRepository<ErrorLog, LoggingContext, long>, IErrorLogRepository
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public ErrorLogRepository(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }

    public async Task<long> UpsertErrorLogs(ErrorLog errorLog, CancellationToken cancellationToken)
    {
        long id = 0;
        var log = await _uow.DbContext.ErrorLog
            .FirstOrDefaultAsync(X => X.LogMessageId == errorLog.LogMessageId
                                      && X.LogMessageType == errorLog.LogMessageType, cancellationToken);
        if (log == null)
        {
            var entity = await _uow.DbContext.ErrorLog.AddAsync(errorLog, cancellationToken);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = entity.Entity.Id;
        }
        else
        {
            _uow.DbContext.ErrorLog.Update(log);
            await _uow.DbContext.SaveChangesAsync(cancellationToken);
            id = log!.Id;
        }

        return id;
    }

    public async Task<IEnumerable<Entities.ErrorLog>> GetErrorLogsAsync(CancellationToken cancellationToken)
    {
        return await _uow.DbContext.ErrorLog.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Entities.ErrorLog>> GetErrorLogsByErrorCategoryAsync(string messageType, CancellationToken cancellationToken)
    {
        return await _uow.DbContext.ErrorLog.ToListAsync(cancellationToken);
        //return await _uow.DbContext.ErrorLog.Where(m => m.MessageType == messageType).ToListAsync(cancellationToken);
    }

    public void ClearErrorLogs()
    {
        _uow.DbContext.ErrorLog.RemoveRange(_uow.DbContext.ErrorLog);
        _uow.DbContext.SaveChanges();
    }
}
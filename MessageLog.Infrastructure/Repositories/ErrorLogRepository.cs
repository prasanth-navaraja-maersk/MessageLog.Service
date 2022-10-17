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
}
using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using MessageLog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessageLog.Infrastructure.Repositories;

public class MessageLogDocRepository : AsyncRepository<Entities.MessageLog, LoggingContext, long>, IMessageLogDocRepository
{
    private readonly IUnitOfWork<LoggingContext> _uow;

    public MessageLogDocRepository(IUnitOfWork<LoggingContext> uow) : base(uow)
    {
        _uow = uow;
    }

    public async Task<long> UpsertMessageLogDocsAsync(MessageLogDoc messageLog, CancellationToken cancellationToken)
    {
        long id = 0;
        try
        {
            var log = await _uow.DbContext.MessageLogDoc
                .FirstOrDefaultAsync(x => x.MessageId == messageLog.MessageId
                                          && x.MessageType == messageLog.MessageType,
                    cancellationToken);
            if (log == null)
            {
                var entity = await _uow.DbContext.MessageLogDoc.AddAsync(messageLog, cancellationToken);
                await _uow.DbContext.SaveChangesAsync(cancellationToken);
                id = entity.Entity.Id;
            }
            else
            {
                _uow.DbContext.MessageLogDoc.Update(log);
                await _uow.DbContext.SaveChangesAsync(cancellationToken);
                id = log!.Id;
            }
        }
        catch (Exception e)
        {
        }

        return id;
    }
}
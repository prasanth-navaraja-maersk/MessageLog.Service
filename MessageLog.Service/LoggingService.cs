using Finance.Common.Database.Relational.Interfaces;
using Finance.Common.Database.Relational.Repositories;
using MessageLog.Service.Entities;
using MessageLog.Service.Repositories;

namespace MessageLog.Service;

public class LoggingService : ILoggingService
{
    private readonly IMessageLogRepository _messageLogRepository;

    public LoggingService(IMessageLogRepository messageLogRepository)
    {
        _messageLogRepository = messageLogRepository;
    }

    public long InsertMessageLogs(Entities.MessageLog messageLog)
    {
        return _messageLogRepository.InsertMessageLogs(messageLog);
    }
}
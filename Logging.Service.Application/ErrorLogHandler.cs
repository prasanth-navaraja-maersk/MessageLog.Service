using System.Runtime.InteropServices.ComTypes;
using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Repositories;

namespace Logging.Service.Application;

public class ErrorLogHandler : IErrorLogHandler
{
    private readonly IErrorLogRepository _errorLogRepository;

    public ErrorLogHandler(IErrorLogRepository errorLogRepository)
    {
        _errorLogRepository = errorLogRepository;
    }

    public async Task<long> UpsertErrorLog(ErrorLogRequest errorLogRequest, CancellationToken cancellationToken)
    {
        var messageLog = new MessageLog.Infrastructure.Entities.ErrorLog
        {
            LogMessageId = errorLogRequest.LogMessageId,
            LogMessageType = errorLogRequest.LogMessageType,
            ErrorLogs = errorLogRequest.ErrorLogs
        };

        return await _errorLogRepository.UpsertErrorLogs(messageLog, cancellationToken);
    }
}
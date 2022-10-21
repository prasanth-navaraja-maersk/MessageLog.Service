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

    public async Task<IEnumerable<ErrorLogRequest>> GetErrorLogsAsync(CancellationToken cancellationToken)
    {
        var errorLogs = await _errorLogRepository.GetErrorLogsAsync(cancellationToken);
        return errorLogs.Select(x => new ErrorLogRequest()
        {
            LogMessageId = x.LogMessageId,
            LogMessageType = x.LogMessageType,
            ErrorLogs = x.ErrorLogs,
        }).ToList();
    }

    public async Task<IEnumerable<ErrorLogRequest>> GetErrorLogsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken)
    {
        var errorLogs = await _errorLogRepository.GetErrorLogsByErrorCategoryAsync(errorCategory, cancellationToken);
        return errorLogs.Select(x => new ErrorLogRequest()
        {
            LogMessageId = x.LogMessageId,
            LogMessageType = x.LogMessageType,
            ErrorLogs = x.ErrorLogs,
        }).ToList();
    }

    public void ClearErrorLogs()
        => _errorLogRepository.ClearErrorLogs();
}
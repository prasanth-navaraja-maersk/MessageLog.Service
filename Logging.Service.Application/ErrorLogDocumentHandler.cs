using Logging.Service.Application.Requests;
using MessageLog.Infrastructure.Repositories;

namespace Logging.Service.Application;

public class ErrorLogDocumentHandler : IErrorLogDocumentHandler
{
    private readonly IErrorLogDocumentRepository _errorLogDocumentRepository;

    public ErrorLogDocumentHandler(IErrorLogDocumentRepository errorLogDocumentRepository)
    {
        _errorLogDocumentRepository = errorLogDocumentRepository;
    }

    public async Task<long> UpsertErrorLogDocumentsAsync(ErrorLogDocumentRequest errorLogRequest, CancellationToken cancellationToken)
    {
        var messageLog = new MessageLog.Infrastructure.Entities.ErrorLogDocument
        {
            LogMessageId = errorLogRequest.LogMessageId,
            LogMessageType = errorLogRequest.LogMessageType,
            ErrorLogDocuments = errorLogRequest.ErrorLogDocuments
        };

        return await _errorLogDocumentRepository.UpsertErrorLogDocumentsAsync(messageLog, cancellationToken);
    }

    public async Task<IEnumerable<ErrorLogDocumentRequest>> GetErrorLogDocumentsAsync(CancellationToken cancellationToken)
    {
        var errorLogs = await _errorLogDocumentRepository.GetErrorLogDocumentsAsync(cancellationToken);
        return errorLogs.Select(x => new ErrorLogDocumentRequest()
        {
            LogMessageId = x.LogMessageId,
            LogMessageType = x.LogMessageType,
            ErrorLogDocuments = x.ErrorLogDocuments,
        }).ToList();
    }

    public async Task<IEnumerable<ErrorLogDocumentRequest>> GetErrorLogDocumentsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken)
    {
        var errorLogs = await _errorLogDocumentRepository.GetErrorLogDocumentsByErrorCategoryAsync(errorCategory, cancellationToken);
        return errorLogs.Select(x => new ErrorLogDocumentRequest()
        {
            LogMessageId = x.LogMessageId,
            LogMessageType = x.LogMessageType,
            ErrorLogDocuments = x.ErrorLogDocuments,
        }).ToList();
    }

    public void ClearErrorLogDocuments()
        => _errorLogDocumentRepository.ClearErrorLogDocuments();
}
using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IErrorLogDocumentHandler
{
    Task<long> UpsertErrorLogDocumentsAsync(ErrorLogDocumentRequest errorLogRequest, CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLogDocumentRequest>> GetErrorLogDocumentsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLogDocumentRequest>> GetErrorLogDocumentsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken);
    void ClearErrorLogDocuments();
}
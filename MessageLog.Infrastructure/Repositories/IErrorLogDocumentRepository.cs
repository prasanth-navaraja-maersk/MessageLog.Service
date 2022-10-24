using MessageLog.Infrastructure.Entities;

namespace MessageLog.Infrastructure.Repositories;

public interface IErrorLogDocumentRepository
{
    Task<long> UpsertErrorLogDocumentsAsync(ErrorLogDocument errorLogDocument, CancellationToken cancellationToken);
    Task<IEnumerable<ErrorLogDocument>> GetErrorLogDocumentsAsync(CancellationToken cancellationToken);
    void ClearErrorLogDocuments();
    Task<IEnumerable<ErrorLogDocument>> GetErrorLogDocumentsByErrorCategoryAsync(string errorCategory, CancellationToken cancellationToken);
}
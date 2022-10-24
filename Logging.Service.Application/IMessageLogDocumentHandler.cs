using Logging.Service.Application.Requests;

namespace Logging.Service.Application;

public interface IMessageLogDocumentHandler
{
    Task<long> UpsertMessageLogDocuments(MessageLogDocumentRequest messageLogRequest, CancellationToken cancellationToken);

    Task<IEnumerable<MessageLogDocumentRequest>> GetMessageLogDocumentsAsync(CancellationToken cancellationToken);

    Task<IEnumerable<MessageLogDocumentRequest>> GetMessageLogDocumentsByMessageTypeAsync(string messageType,
        CancellationToken cancellationToken);

    void ClearMessageLogDocuments();
}
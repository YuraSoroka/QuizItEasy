using QuizItEasy.Domain.Common;

namespace QuizItEasy.Application.Common.Services;

public interface IBlobStorageService
{
    Task<Result<string>> UploadAsync(
        string containerName,
        string blobName,
        Stream content,
        string contentType,
        CancellationToken cancellationToken = default);

    Task<Result<Stream>> DownloadAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default);

    Task<Result> DeleteAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default);
}

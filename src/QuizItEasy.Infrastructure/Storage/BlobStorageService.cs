using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using QuizItEasy.Application.Common.Services;
using QuizItEasy.Domain.Common;

namespace QuizItEasy.Infrastructure.Storage;

public sealed class BlobStorageService(BlobServiceClient blobServiceClient) : IBlobStorageService
{
    public async Task<Result<string>> UploadAsync(
        string containerName,
        string blobName,
        Stream content,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);

            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType }, cancellationToken: cancellationToken);

            return Result.Success(blobClient.Uri.ToString());
        }
        catch (Exception ex)
        {
            return Result.Failure<string>(Error.Problem("BlobUploadError", ex.Message));
        }
    }

    public async Task<Result<Stream>> DownloadAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync(cancellationToken))
            {
                return Result.Failure<Stream>(Error.NotFound("BlobNotFound", $"Blob '{blobName}' not found in container '{containerName}'."));
            }

            var response = await blobClient.OpenReadAsync(cancellationToken: cancellationToken);
            return Result.Success<Stream>(response);
        }
        catch (Exception ex)
        {
            return Result.Failure<Stream>(Error.Problem("BlobDownloadError", ex.Message));
        }
    }

    public async Task<Result> DeleteAsync(
        string containerName,
        string blobName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var response = await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);

            if (!response.Value)
            {
                return Result.Failure(Error.NotFound("BlobNotFound", $"Blob '{blobName}' not found in container '{containerName}'."));
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Problem("BlobDeleteError", ex.Message));
        }
    }
}

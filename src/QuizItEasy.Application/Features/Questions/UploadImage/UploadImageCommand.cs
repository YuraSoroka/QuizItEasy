using Mapster;
using Microsoft.AspNetCore.Http;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Application.Common.Services;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizCollectionItem = QuizItEasy.Domain.Entities.QuizCollections.QuizCollection;

namespace QuizItEasy.Application.Features.Questions.UploadImage;

// TODO: add validation
public sealed record UploadImageCommand(string CollectionId, IFormFile Image)
    : ICommand<UploadImageResponse>;

// Update the handler to accept BlobServiceClient:
public sealed class UploadImageCommandHandler(
    IMongoRepository<QuizCollectionItem> collectionRepository,
    IBlobStorageService blobStorageService)
    : ICommandHandler<UploadImageCommand, UploadImageResponse>
{
    public async Task<Result<UploadImageResponse>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var collection = await collectionRepository.FindByIdAsync(request.CollectionId);

        FileMetadata fileMetadata = FileMetadata.Create(
            Path.GetFileName(request.Image.FileName),
            request.Image.Length,
            Path.GetExtension(request.Image.FileName),
            request.Image.ContentType,
            collection.Code);

        using var stream = request.Image.OpenReadStream();
        // *upload file to azure blob storage
        var url = await blobStorageService.UploadAsync(
            "quiz-it-easy",
            fileMetadata.BlobLink,
            stream,
            request.Image.ContentType,
            cancellationToken);

        var eq = url.Value == fileMetadata.BlobLink;

        return fileMetadata.Adapt<UploadImageResponse>();
    }
}

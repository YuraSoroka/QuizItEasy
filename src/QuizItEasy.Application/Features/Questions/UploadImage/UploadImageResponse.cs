namespace QuizItEasy.Application.Features.Questions.UploadImage;

public sealed record UploadImageResponse
{
    public Guid Id { get; init; }
    public string UntrustedName { get; init; }
    public long ByteSize { get; init; }
    public string FileExtension { get; init; }
    public string ContentType { get; init; }
    public DateTime UploadedAt { get; init; }
    public string RelativePath { get; init; }
}

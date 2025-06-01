namespace QuizItEasy.Domain.Entities.Common;

public class FileMetadata : ValueObject
{
    public Guid Id { get; private set; }
    public string UntrustedName { get; private set; }
    public long ByteSize { get; private set; }
    public string FileExtension { get; private set; }
    public string ContentType { get; init; }
    public DateTime UploadedAt { get; init; }
    public string RelativePath { get; init; }

    public string BlobLink => $"{RelativePath}/{Id}{FileExtension}";

    private FileMetadata(string untrustedName, long byteSize, string fileExtension, string contentType, string relativePath)
    {
        Id = Guid.CreateVersion7();
        UntrustedName = untrustedName;
        ByteSize = byteSize;
        FileExtension = fileExtension;
        ContentType = contentType;
        RelativePath = relativePath;
        UploadedAt = DateTime.UtcNow;
    }

    public static FileMetadata Create(
        string untrustedName,
        long byteSize,
        string fileExtension,
        string contentType,
        string relativePath)
    {
        return new FileMetadata(untrustedName, byteSize, fileExtension, contentType, relativePath);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return UntrustedName;
        yield return ByteSize;
        yield return FileExtension;
        yield return ContentType;
        yield return UploadedAt;
    }
}

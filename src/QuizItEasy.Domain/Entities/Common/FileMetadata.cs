namespace QuizItEasy.Domain.Entities.Common;

public class FileMetadata
{
    public string UntrustedName { get; private set; }
    public Guid StorageName { get; private set; }
    public long ByteSize { get; private set; }
    public string FileExtension { get; private set; }

    private FileMetadata(string untrustedName, long byteSize, string fileExtension)
    {
        UntrustedName = untrustedName;
        ByteSize = byteSize;
        FileExtension = fileExtension;
        StorageName = Guid.CreateVersion7();
    }
    
    public static FileMetadata Create(
        string untrustedName, 
        long byteSize, 
        string fileExtension)
    {
        return new FileMetadata(untrustedName, byteSize, fileExtension);
    }
}

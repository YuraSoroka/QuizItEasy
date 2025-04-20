using MongoDB.Bson;

namespace QuizItEasy.Domain.Entities.Common;

public abstract class Question(string text, FileMetadata? image) : AuditableEntity
{
    public ObjectId Id { get; init; } = ObjectId.GenerateNewId();
    public string Text { get; protected set; } = text;
    public FileMetadata? Image { get; protected set; } = image;
}

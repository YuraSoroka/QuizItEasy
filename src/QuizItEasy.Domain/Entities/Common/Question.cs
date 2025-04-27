using MongoDB.Bson;
using QuizItEasy.Domain.Common;

namespace QuizItEasy.Domain.Entities.Common;

[MongoCollectionName("questions")]
public abstract class Question(string text, ObjectId quizCollectionId, FileMetadata? image)
    : AggregateRoot
{
    public ObjectId QuizCollectionId { get; protected set; } = quizCollectionId;
    public string Text { get; protected set; } = text;
    public FileMetadata? Image { get; protected set; } = image;
}

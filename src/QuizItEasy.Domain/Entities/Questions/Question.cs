using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuizItEasy.Domain.Entities.Questions;


public class Question
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("text")]
    public string Text { get; set; }

    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)]
    public QuestionType Type { get; set; }

    [BsonElement("options")]
    public List<QuestionOption> Options { get; set; } = [];

    [BsonElement("correctAnswers")]
    public List<string> CorrectAnswers { get; set; } = []; // Stores IDs of correct options

    [BsonElement("imageUrl")]
    public string? ImageUrl { get; set; } // For image-based questions
}

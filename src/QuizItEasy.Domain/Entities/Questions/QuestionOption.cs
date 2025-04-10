using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuizItEasy.Domain.Entities.Questions;

public class QuestionOption
{
    [BsonElement("id")]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

    [BsonElement("text")]
    public string Text { get; set; }

    [BsonElement("imageUrl")]
    public string? ImageUrl { get; set; } // Optional for image-based options
}

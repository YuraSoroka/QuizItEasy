using MongoDB.Driver;
using QuizItEasy.Application.Common.Abstractions;

namespace QuizItEasy.Infrastructure.Persistence;

public class MongoDbContext(IMongoClient mongoClient) : IMongoDbContext
{
    public IMongoDatabase Database { get; } = mongoClient.GetDatabase("quiziteasy");

    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        return Database.GetCollection<T>(name ?? typeof(T).Name.ToLower());
    }
}

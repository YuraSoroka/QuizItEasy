using MongoDB.Driver;
using QuizItEasy.Application.Common.Abstractions;

namespace QuizItEasy.Infrastructure.Persistence;

public class MongoDbContext : IMongoDbContext
{
    public IMongoDatabase Database { get; }
    public IMongoClient MongoClient { get; }

    public MongoDbContext()
    {
        MongoClient = new MongoClient("connection_string");
        Database = MongoClient.GetDatabase("quiziteasy");
    }

    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        return Database.GetCollection<T>(name ?? typeof(T).Name.ToLower());
    }
}

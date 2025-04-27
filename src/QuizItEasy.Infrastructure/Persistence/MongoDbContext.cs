using MongoDB.Driver;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Domain.Common;
using QuizItEasy.Infrastructure.Common;

namespace QuizItEasy.Infrastructure.Persistence;

public sealed class MongoDbContext(IMongoClient mongoClient) : IMongoDbContext
{
    public IMongoDatabase Database { get; } = mongoClient.GetDatabase(DocumentDbSettings.Database);

    public IMongoCollection<T> GetCollection<T>(string? name = null)
    {
        return Database.GetCollection<T>(name ?? GetCollectionName(typeof(T)));
    }

    private string GetCollectionName(Type documentType)
    {
        return ((MongoCollectionNameAttribute)documentType.GetCustomAttributes(
                typeof(MongoCollectionNameAttribute),
                true)
            .FirstOrDefault())?.CollectionName ?? documentType.Name.ToLower();
    }
}

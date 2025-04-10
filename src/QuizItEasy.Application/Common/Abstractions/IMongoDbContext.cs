using MongoDB.Driver;

namespace QuizItEasy.Application.Common.Abstractions;

public interface IMongoDbContext //: IDisposable
{
    IMongoDatabase Database { get; }
    IMongoCollection<T> GetCollection<T>(string? name = null);
    //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    //Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    //Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    //Task RollbackTransaction(CancellationToken cancellationToken = default);
    //void AddCommand(Func<Task> func);
}

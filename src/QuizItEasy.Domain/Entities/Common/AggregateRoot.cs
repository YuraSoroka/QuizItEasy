using MongoDB.Bson;
using QuizItEasy.Domain.Common;

namespace QuizItEasy.Domain.Entities.Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(ObjectId id)
        : base(id)
    { }

    protected AggregateRoot() { }

    protected readonly List<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }
}

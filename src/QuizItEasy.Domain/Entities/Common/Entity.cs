using MongoDB.Bson;

namespace QuizItEasy.Domain.Entities.Common;

public abstract class Entity
{
    public ObjectId Id { get; init; } = ObjectId.GenerateNewId();

    public bool IsDeleted { get; protected set; } = false;
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public string CreatedBy { get; protected set; } = "admin";

    public override bool Equals(object? other)
    {
        if (other is null || other.GetType() != GetType())
        {
            return false;
        }

        return ((Entity)other).Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected Entity(ObjectId id) => Id = id;

    protected Entity() { }

}

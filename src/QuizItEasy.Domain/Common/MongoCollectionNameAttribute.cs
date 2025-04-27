namespace QuizItEasy.Domain.Common;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class MongoCollectionNameAttribute(string collectionName) : Attribute
{
    public string CollectionName { get; } = collectionName;
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.Domain.Common.Extensions;

public static class DomainExtensions
{
    public static ObjectId AsObjectId(this string id)
    {
        ArgumentNullException.ThrowIfNull(id);
        return ObjectId.Parse(id);
    }

    public static string GetTypeDiscriminator<T>(T classType)
    {
        return BsonClassMap.LookupClassMap(classType?.GetType()).Discriminator;
    }
}

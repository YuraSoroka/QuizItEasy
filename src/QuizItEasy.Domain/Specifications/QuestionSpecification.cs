using MongoDB.Bson;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Domain.Specifications;

public class QuestionSpecification : Specification<Question>
{
    public QuestionSpecification(ObjectId collectionId, int pageNumber, int pageSize)
    {
        AddCriteria(q => q.QuizCollectionId.Equals(collectionId))
            .AddOrderBy(q => q.OrderBy(x => x.Id))
            .ApplyPaging(pageNumber, pageSize);
    }
}

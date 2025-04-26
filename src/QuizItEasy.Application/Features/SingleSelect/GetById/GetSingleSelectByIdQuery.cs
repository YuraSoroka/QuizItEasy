using Mapster;
using MongoDB.Bson;
using MongoDB.Driver;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;

namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public record GetSingleSelectByIdQuery(string Id) : IQuery<SingleSelectResponse>;

public class GetSingleSelectByIdQueryHandler(IMongoDbContext mongoDbContext)
    : IQueryHandler<GetSingleSelectByIdQuery, SingleSelectResponse>
{
    public async Task<Result<SingleSelectResponse>> Handle(GetSingleSelectByIdQuery request, CancellationToken cancellationToken)
    {
        var collection = mongoDbContext.GetCollection<Domain.Entities.Questions.SingleSelect>("AZ-204");

        FilterDefinition<Domain.Entities.Questions.SingleSelect> filter = Builders<Domain.Entities.Questions.SingleSelect>.Filter
            .Eq(q => q.Id, ObjectId.Parse(request.Id));

        var singleSelect = await collection
            .Find(filter)
            .SingleAsync(cancellationToken);

        return singleSelect.Adapt<SingleSelectResponse>();
    }
}

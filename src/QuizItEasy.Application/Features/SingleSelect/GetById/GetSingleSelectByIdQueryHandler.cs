using Mapster;
using MapsterMapper;
using MongoDB.Driver;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using SingleSelectQuestion = QuizItEasy.Domain.Entities.Questions.SingleSelect;

namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public class GetSingleSelectByIdQueryHandler(IMongoDbContext mongoDbContext, IMapper mapper) 
    : IQueryHandler<GetSingleSelectByIdQuery, SingleSelectResponse>
{
    public async Task<Result<SingleSelectResponse>> Handle(GetSingleSelectByIdQuery request, CancellationToken cancellationToken)
    {
        var collection = mongoDbContext.GetCollection<SingleSelectQuestion>("AZ-204");

        FilterDefinition<SingleSelectQuestion> filter = Builders<SingleSelectQuestion>.Filter
            .Eq(q => q.Id, request.Id);
        
        var singleSelect = await collection
            .Find(filter)
            .SingleAsync(cancellationToken);

        return singleSelect.Adapt<SingleSelectResponse>();
    }
}

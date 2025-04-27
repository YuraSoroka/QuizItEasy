using Mapster;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public record GetSingleSelectByIdQuery(string Id)
    : IQuery<SingleSelectResponse>;

public class GetSingleSelectByIdQueryHandler(IMongoRepository<Question> questionRepository)
    : IQueryHandler<GetSingleSelectByIdQuery, SingleSelectResponse>
{
    public async Task<Result<SingleSelectResponse>> Handle(GetSingleSelectByIdQuery request,
        CancellationToken cancellationToken)
    {
        var singleSelect = await questionRepository
            .FindByIdAsync(request.Id);

        return singleSelect.Adapt<SingleSelectResponse>();
    }
}

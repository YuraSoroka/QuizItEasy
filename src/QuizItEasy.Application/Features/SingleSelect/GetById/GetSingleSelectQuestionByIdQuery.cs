using Mapster;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public record GetSingleSelectQuestionByIdQuery(string Id)
    : IQuery<SingleSelectQuestionResponse>;

public class GetSingleSelectByIdQueryHandler(IMongoRepository<Question> questionRepository)
    : IQueryHandler<GetSingleSelectQuestionByIdQuery, SingleSelectQuestionResponse>
{
    public async Task<Result<SingleSelectQuestionResponse>> Handle(
        GetSingleSelectQuestionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var singleSelect = await questionRepository
            .FindByIdAsync(request.Id);

        return singleSelect.Adapt<SingleSelectQuestionResponse>();
    }
}

using Mapster;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Application.Features.SingleSelect.GetById;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Application.Features.MultiSelect.GetById;

public record GetMultiSelectQuestionByIdQuery(string Id)
    : IQuery<MultiSelectQuestionResponse>;

public class GetMultiSelectQuestionByIdQueryHandler(IMongoRepository<Question> questionRepository)
    : IQueryHandler<GetMultiSelectQuestionByIdQuery, MultiSelectQuestionResponse>
{
    public async Task<Result<MultiSelectQuestionResponse>> Handle(
        GetMultiSelectQuestionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var multiSelect = await questionRepository
            .FindByIdAsync(request.Id);

        return multiSelect.Adapt<MultiSelectQuestionResponse>();
    }
}

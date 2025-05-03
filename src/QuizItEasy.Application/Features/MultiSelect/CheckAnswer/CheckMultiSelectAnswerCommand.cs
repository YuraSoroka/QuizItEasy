using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.MultiSelect;

namespace QuizItEasy.Application.Features.MultiSelect.CheckAnswer;

public record CheckMultiSelectAnswerCommand(string QuestionId, IEnumerable<Guid> AnswerIds)
    : ICommand<bool>;

public class CheckMultiSelectAnswerCommandHandler(IMongoRepository<Question> questionRepository)
    : ICommandHandler<CheckMultiSelectAnswerCommand, bool>
{
    public async Task<Result<bool>> Handle(CheckMultiSelectAnswerCommand request, CancellationToken cancellationToken)
    {
        if (await questionRepository.FindByIdAsync(request.QuestionId) is not MultiSelectQuestion multiSelect)
        {
            return Result.Failure<bool>(Error.NotFound("MultiSelectQuestionError", "Not Found"));
        }

        return multiSelect.IsCorrectAnswer(request.AnswerIds);
    }
}

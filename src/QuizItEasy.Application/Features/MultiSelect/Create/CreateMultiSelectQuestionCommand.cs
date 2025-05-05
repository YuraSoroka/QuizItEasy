using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.MultiSelect;
using QuizCollectionItem = QuizItEasy.Domain.Entities.QuizCollections.QuizCollection;

namespace QuizItEasy.Application.Features.MultiSelect.Create;

public sealed record CreateMultiSelectQuestionCommand(
    string QuestionText,
    string QuizCollectionId,
    IEnumerable<CreateMultiSelectAnswerRequest> Answers
    ) : ICommand<string>;

public sealed record CreateMultiSelectAnswerRequest(
    string Value,
    bool IsCorrect);


public class CreateMultiSelectQuestionCommandHandler(
    IMongoRepository<Question> questionRepository,
    IMongoRepository<QuizCollectionItem> quizCollectionRepository)
    : ICommandHandler<CreateMultiSelectQuestionCommand, string>
{
    public async Task<Result<string>> Handle(CreateMultiSelectQuestionCommand request, CancellationToken cancellationToken)
    {
        var answers = request.Answers
            .Select(ar => Answer.CreateOption(ar.Value, ar.IsCorrect));

        var multiSelectQuestion = await MultiSelectQuestion.Create(
            quizCollectionRepository,
            answers,
            request.QuestionText,
            request.QuizCollectionId);

        if (multiSelectQuestion.IsFailure)
        {
            return Result.Failure<string>(multiSelectQuestion.Error);
        }

        await questionRepository.InsertOneAsync(multiSelectQuestion.Value);

        return Result.Success(multiSelectQuestion.Value.Id.ToString());
    }
}

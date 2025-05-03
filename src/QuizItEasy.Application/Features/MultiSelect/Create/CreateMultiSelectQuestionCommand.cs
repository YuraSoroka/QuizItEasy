using MongoDB.Bson;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Application.Features.SingleSelect.Create;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.MultiSelect;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.Application.Features.MultiSelect.Create;

public sealed record CreateMultiSelectQuestionCommand(
    string QuestionText,
    string QuizCollectionId,
    IEnumerable<CreateMultiSelectAnswerRequest> Answers
    ) : ICommand<string>;

public sealed record CreateMultiSelectAnswerRequest(
    string Value,
    bool IsCorrect);


public class CreateMultiSelectQuestionCommandHandler(IMongoRepository<Question> questionRepository)
    : ICommandHandler<CreateMultiSelectQuestionCommand, string>
{
    public async Task<Result<string>> Handle(CreateMultiSelectQuestionCommand request, CancellationToken cancellationToken)
    {
        var answers = request.Answers
            .Select(ar => Answer.CreateOption(ar.Value, ar.IsCorrect));

        var multiSelectQuestion = MultiSelectQuestion.Create(
            answers,
            request.QuestionText,
            ObjectId.Parse(request.QuizCollectionId));

        await questionRepository.InsertOneAsync(multiSelectQuestion.Value);

        return Result.Success(multiSelectQuestion.Value.Id.ToString());
    }
}

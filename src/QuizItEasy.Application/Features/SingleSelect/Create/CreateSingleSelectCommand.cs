using MongoDB.Bson;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.Application.Features.SingleSelect.Create;

public sealed record CreateSingleSelectCommand(
    string QuestionText,
    string QuizCollectionId,
    IEnumerable<CreateSingleSelectAnswerRequest> Answers
    ) : ICommand<string>;

public sealed record CreateSingleSelectAnswerRequest(
    string Value,
    bool IsCorrect);


public class CreateSingleSelectCommandHandler(IMongoRepository<Question> questionRepository)
    : ICommandHandler<CreateSingleSelectCommand, string>
{
    public async Task<Result<string>> Handle(CreateSingleSelectCommand request, CancellationToken cancellationToken)
    {
        var answers = request.Answers
            .Select(ar => Answer.CreateOption(ar.Value, ar.IsCorrect));

        var singleSelectQuestion = SingleSelectQuestion.Create(
            answers,
            request.QuestionText,
            ObjectId.Parse(request.QuizCollectionId));

        if (singleSelectQuestion.IsFailure)
        {
            return Result.Failure<string>(singleSelectQuestion.Error);
        }

        await questionRepository.InsertOneAsync(singleSelectQuestion.Value);

        return Result.Success(singleSelectQuestion.Value.Id.ToString());
    }
}

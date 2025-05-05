using MongoDB.Bson;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;
using QuizCollectionItem = QuizItEasy.Domain.Entities.QuizCollections.QuizCollection;

namespace QuizItEasy.Application.Features.SingleSelect.Create;

public sealed record CreateSingleSelectQuestionCommand(
    string QuestionText,
    string QuizCollectionId,
    IEnumerable<CreateSingleSelectAnswerRequest> Answers
    ) : ICommand<string>;

public sealed record CreateSingleSelectAnswerRequest(
    string Value,
    bool IsCorrect);


public class CreateSingleSelectCommandHandler(
    IMongoRepository<Question> questionRepository,
    IMongoRepository<QuizCollectionItem> quizCollectionRepository)
    : ICommandHandler<CreateSingleSelectQuestionCommand, string>
{
    public async Task<Result<string>> Handle(CreateSingleSelectQuestionCommand request, CancellationToken cancellationToken)
    {
        var answers = request.Answers
            .Select(ar => Answer.CreateOption(ar.Value, ar.IsCorrect));

        var singleSelectQuestion = await SingleSelectQuestion.Create(
            quizCollectionRepository,
            answers,
            request.QuestionText,
            request.QuizCollectionId);

        if (singleSelectQuestion.IsFailure)
        {
            return Result.Failure<string>(singleSelectQuestion.Error);
        }

        await questionRepository.InsertOneAsync(singleSelectQuestion.Value);

        return Result.Success(singleSelectQuestion.Value.Id.ToString());
    }
}

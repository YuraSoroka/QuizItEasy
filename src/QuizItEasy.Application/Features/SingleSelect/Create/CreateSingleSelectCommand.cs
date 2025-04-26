using MongoDB.Bson;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;
using SingleSelectQuestion = QuizItEasy.Domain.Entities.Questions.SingleSelect;

namespace QuizItEasy.Application.Features.SingleSelect.Create;

public sealed record CreateSingleSelectCommand(
    string QuestionText,
    string QuizCollectionId,
    IEnumerable<CreateSingleSelectAnswerRequest> Answers
    ) : ICommand<string>;

public sealed record CreateSingleSelectAnswerRequest(
    string Value,
    bool IsCorrect);


public class CreateSingleSelectCommandHandler(IMongoDbContext mongoDbContext)
    : ICommandHandler<CreateSingleSelectCommand, string>
{
    public async Task<Result<string>> Handle(CreateSingleSelectCommand request, CancellationToken cancellationToken)
    {
        var collection = mongoDbContext.GetCollection<Question>("AZ-204");

        var answers = request.Answers
            .Select(ar => Answer.CreateOption(ar.Value, ar.IsCorrect));

        var singleSelectQuestion = SingleSelectQuestion.Create(
            answers,
            request.QuestionText,
            ObjectId.Parse(request.QuizCollectionId));

        await collection.InsertOneAsync(
            singleSelectQuestion,
            cancellationToken: cancellationToken);

        return Result.Success(singleSelectQuestion.Id.ToString());
    }
}

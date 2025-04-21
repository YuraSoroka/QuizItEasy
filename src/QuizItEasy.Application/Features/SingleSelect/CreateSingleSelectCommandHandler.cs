using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;
using SingleSelectQuestion = QuizItEasy.Domain.Entities.Questions.SingleSelect;

namespace QuizItEasy.Application.Features.SingleSelect;

public class CreateSingleSelectCommandHandler(IMongoDbContext mongoDbContext)
    : ICommandHandler<CreateSingleSelectCommand>
{
    public async Task<Result> Handle(CreateSingleSelectCommand request, CancellationToken cancellationToken)
    {
        var collection = mongoDbContext.GetCollection<Question>("AZ-204");

        var answers = request.Answers
            .Select(ar => Answer.CreateOption(ar.Value, ar.IsCorrect));

        await collection.InsertOneAsync(
            SingleSelectQuestion.Create(answers, request.QuestionText),
            cancellationToken: cancellationToken);

        return Result.Success();
    }
}

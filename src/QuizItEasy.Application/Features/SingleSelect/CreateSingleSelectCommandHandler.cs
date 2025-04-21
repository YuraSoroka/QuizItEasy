using MediatR;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;
using SingleSelectQuestion = QuizItEasy.Domain.Entities.Questions.SingleSelect;

namespace QuizItEasy.Application.Features.SingleSelect;

public class CreateSingleSelectCommandHandler(IMongoDbContext mongoDbContext) 
    : IRequestHandler<CreateSingleSelectCommand>
{
    public Task Handle(CreateSingleSelectCommand request, CancellationToken cancellationToken)
    {
        var collection = mongoDbContext.GetCollection<Question>("AZ-204");

        IEnumerable<Answer> answers = request.Answers
            .Select(ar => Answer.CreateOption(ar.Value, ar.IsCorrect));
        
        return collection.InsertOneAsync(
            SingleSelectQuestion.Create(answers, request.QuestionText), 
            cancellationToken: cancellationToken);
        
    }
}

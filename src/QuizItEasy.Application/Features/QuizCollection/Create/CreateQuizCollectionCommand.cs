using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizCollectionItem = QuizItEasy.Domain.Entities.QuizCollections.QuizCollection;

namespace QuizItEasy.Application.Features.QuizCollection.Create;

public sealed record CreateQuizCollectionCommand(
    string Name,
    string Code)
    : ICommand<string>;

public class CreateQuizCollectionCommandHandler(IMongoRepository<QuizCollectionItem> quizCollectionRepository) 
    : ICommandHandler<CreateQuizCollectionCommand, string>
{
    public async Task<Result<string>> Handle(CreateQuizCollectionCommand request, CancellationToken cancellationToken)
    {
        var quizCollection = QuizCollectionItem.Create(request.Code, request.Name);
        await quizCollectionRepository.InsertOneAsync(quizCollection);
        return quizCollection.Id.ToString();
    }
}

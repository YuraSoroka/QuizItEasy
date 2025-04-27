using Mapster;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizCollectionItem = QuizItEasy.Domain.Entities.QuizCollections.QuizCollection;

namespace QuizItEasy.Application.Features.QuizCollection.GetById;

public record GetQuizCollectionByIdQuery(string Id)
    : IQuery<QuizCollectionResponse>;

public class GetQuizCollectionByIdQueryHandler(IMongoRepository<QuizCollectionItem> quizCollectionRepository)
    : IQueryHandler<GetQuizCollectionByIdQuery, QuizCollectionResponse>
{
    public async Task<Result<QuizCollectionResponse>> Handle(GetQuizCollectionByIdQuery request, CancellationToken cancellationToken)
    {
        var quizCollection = await quizCollectionRepository.FindByIdAsync(request.Id);

        return quizCollection.Adapt<QuizCollectionResponse>();
    }
}

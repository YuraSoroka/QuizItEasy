using Mapster;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Application.Common.Models;
using QuizItEasy.Application.Features.QuizCollection.GetById;
using QuizItEasy.Domain.Common;
using QuizCollectionItem = QuizItEasy.Domain.Entities.QuizCollections.QuizCollection;

namespace QuizItEasy.Application.Features.QuizCollection.GetAll;

public record GetQuizCollectionQuery(int PageNumber, int PageSize)
    : IQuery<PagedList<QuizCollectionResponse>>;

public class GetQuizCollectionQueryHandler(IMongoRepository<QuizCollectionItem> quizCollectionRepository)
    : IQueryHandler<GetQuizCollectionQuery, PagedList<QuizCollectionResponse>>
{
    public async Task<Result<PagedList<QuizCollectionResponse>>> Handle(GetQuizCollectionQuery request, CancellationToken cancellationToken)
    {
        var source = quizCollectionRepository
            .AsQueryable()
            .ProjectToType<QuizCollectionResponse>();

        var pagedResult = await PagedList<QuizCollectionResponse>.CreateAsync(
            source,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return pagedResult;
    }
}

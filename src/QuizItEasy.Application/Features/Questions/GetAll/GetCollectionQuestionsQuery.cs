using System.Collections.Immutable;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Application.Common.Models;
using QuizItEasy.Application.Common.Services;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Common.Extensions;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Application.Features.Questions.GetAll;

public sealed record GetCollectionQuestionsQuery(
    string CollectionId, int PageNumber) : IQuery<PagedList<QuestionResponse>>
{
    public int PageSize { get; } = 1;
}

public class GetCollectionQuestionsQueryHandler(
    IMongoRepository<Question> questionMongoRepository,
    QuestionToJsonResolverRegistry questionToJsonResolverRegistry)
        : IQueryHandler<GetCollectionQuestionsQuery, PagedList<QuestionResponse>>
{
    public async Task<Result<PagedList<QuestionResponse>>> Handle(
        GetCollectionQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        var collectionId = request.CollectionId.AsObjectId();

        var questionsTask = questionMongoRepository.GetAllPaginatedAsync(
            q => q.QuizCollectionId.Equals(collectionId),
            request.PageNumber,
            request.PageSize);

        var countTask = Task.Run(() => questionMongoRepository.AsQueryable()
                .Count(q => q.QuizCollectionId.Equals(collectionId)));

        await Task.WhenAll(questionsTask, countTask);

        var questionResponse = questionsTask.Result
            .Select(questionToJsonResolverRegistry.ResolveAsJson)
            .ToImmutableList();

        return new PagedList<QuestionResponse>(
            questionResponse, countTask.Result, request.PageNumber, request.PageSize);
    }
}

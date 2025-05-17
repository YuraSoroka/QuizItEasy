using MediatR;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Models;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.Questions.GetAll;

namespace QuizItEasy.API.Endpoints.Questions;

public sealed class GetCollectionQuestionsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            pattern: "quiz-collections/{collectionId}/questions",
            async (ISender sender, string collectionId, [AsParameters] PaginationModel paginationModel) =>
            {
                var result = await sender.Send(new GetCollectionQuestionsQuery(collectionId, paginationModel.PageNumber));
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.AllQuestions);
    }
}

using MediatR;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.QuizCollection.GetById;

namespace QuizItEasy.API.Endpoints.QuizCollection;

public sealed class GetQuizCollectionByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("quiz-collections/{id}", async (string id, ISender sender) =>
        {
            var result = await sender.Send(new GetQuizCollectionByIdQuery(id));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.QuizCollection);
    }
}

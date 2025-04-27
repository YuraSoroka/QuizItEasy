using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.QuizCollection.Create;

namespace QuizItEasy.API.Endpoints.QuizCollection;

public sealed class CreateQuizCollection : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("quiz-collections", async (ISender sender, [FromBody] CreateQuizCollectionCommand command) =>
            {
                var result = await sender.Send(command);
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.QuizCollection);
    }
}

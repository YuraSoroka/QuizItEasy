using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.SingleSelect.Create;

namespace QuizItEasy.API.Endpoints.SingleSelect;

public sealed class CreateSingleSelectEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("single-selects", async (ISender sender, [FromBody] CreateSingleSelectQuestionCommand command) =>
        {
            var result = await sender.Send(command);
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.SingleSelect);
    }
}

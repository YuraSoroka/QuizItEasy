using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.MultiSelect.Create;

namespace QuizItEasy.API.Endpoints.MultiSelect;

public sealed class CreateMultiSelectQuestionEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("multi-selects", async (ISender sender, [FromBody] CreateMultiSelectQuestionCommand command) =>
        {
            var result = await sender.Send(command);
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.MultiSelect);
    }
}

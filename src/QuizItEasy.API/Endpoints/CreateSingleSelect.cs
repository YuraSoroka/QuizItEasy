using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.SingleSelect;
using QuizItEasy.Domain.Common;

namespace QuizItEasy.API.Endpoints;

public sealed class CreateSingleSelect : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("single-select", async (ISender sender, [FromBody] CreateSingleSelectCommand command) =>
            {
                Result result = await sender.Send(command);
                return result.Match(() => Results.Ok(), ApiResults.Problem);
            })
            .WithTags(Tags.SingleSelect);
    }
}

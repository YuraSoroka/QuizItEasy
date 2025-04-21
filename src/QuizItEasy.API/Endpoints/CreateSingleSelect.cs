using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.Application.Features.SingleSelect;

namespace QuizItEasy.API.Endpoints;

public sealed class CreateSingleSelect : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("single-select", async (ISender sender, [FromBody] CreateSingleSelectCommand command) =>
            {
                await sender.Send(command);
                return Results.Ok();
            })
            .WithTags(Tags.SingleSelect);
    }
}

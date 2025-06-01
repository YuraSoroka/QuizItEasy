using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.SingleSelect.CheckAnswer;

namespace QuizItEasy.API.Endpoints.SingleSelect;

public class CheckSingleSelectAnswerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("single-selects/{questionId}/check-answer", async (ISender sender, [FromRoute] string questionId, [FromBody] CheckSingleSelectAnswerCommand command) =>
        {
            var result = await sender.Send(command);
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.SingleSelect);
    }
}

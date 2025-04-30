using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.SingleSelect.CheckAnswer;
using QuizItEasy.Application.Features.SingleSelect.Create;

namespace QuizItEasy.API.Endpoints.SingleSelect;

public class CheckSingleSelectAnswer : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("single-selects/{questionId}/check-answer", async (ISender sender, [FromRoute] string questionId, [FromBody] Guid answerId) =>
        {
            var result = await sender.Send(new CheckAnswerCommand(questionId, answerId));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
    .WithTags(Tags.SingleSelect);
    }
}

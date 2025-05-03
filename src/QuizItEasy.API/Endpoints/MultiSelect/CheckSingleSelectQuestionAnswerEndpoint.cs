using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.MultiSelect.CheckAnswer;

namespace QuizItEasy.API.Endpoints.MultiSelect;

public sealed class CheckSingleSelectQuestionAnswerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("multi-selects/{questionId}/check-answer", async (ISender sender, [FromRoute] string questionId, [FromBody] IEnumerable<Guid> answerIds) =>
        {
            var result = await sender.Send(new CheckMultiSelectAnswerCommand(questionId, answerIds));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.MultiSelect);
    }
}

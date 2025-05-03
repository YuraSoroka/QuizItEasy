using MediatR;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.SingleSelect.GetById;

namespace QuizItEasy.API.Endpoints.SingleSelect;

public sealed class GetSingleSelectByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("single-selects/{id}", async (string id, ISender sender) =>
        {
            var result = await sender.Send(new GetSingleSelectQuestionByIdQuery(id));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.SingleSelect);
    }
}

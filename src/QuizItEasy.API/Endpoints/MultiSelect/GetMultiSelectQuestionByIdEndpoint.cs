using MediatR;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.MultiSelect.GetById;

namespace QuizItEasy.API.Endpoints.MultiSelect;

public sealed class GetMultiSelectQuestionByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("multi-selects/{id}", async (string id, ISender sender) =>
        {
            var result = await sender.Send(new GetMultiSelectQuestionByIdQuery(id));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.MultiSelect);
    }
}

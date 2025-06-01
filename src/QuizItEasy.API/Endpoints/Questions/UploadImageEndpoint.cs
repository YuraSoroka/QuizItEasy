using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.Questions.UploadImage;

namespace QuizItEasy.API.Endpoints.Questions;

public sealed class UploadImageEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("quiz-collections/{collectionId}/images", async (ISender sender, [FromRoute] string collectionId, IFormFile image) =>
        {
            var result = await sender.Send(new UploadImageCommand(collectionId, image));
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.AllQuestions)
        .DisableAntiforgery();
    }
}

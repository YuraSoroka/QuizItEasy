using MediatR;
using QuizItEasy.API.Common;
using QuizItEasy.API.Common.Abstractions;
using QuizItEasy.API.Common.Models;
using QuizItEasy.API.Common.Results;
using QuizItEasy.Application.Features.QuizCollection.GetAll;

namespace QuizItEasy.API.Endpoints.QuizCollection;

public sealed class GetQuizCollection : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("quiz-collections", async (ISender sender, [AsParameters] PaginationModel paginationModel) =>
            {
                var result = await sender.Send(new GetQuizCollectionQuery(paginationModel.PageNumber, 
                                                                          paginationModel.PageSize));
                
                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.QuizCollection);
    }
}

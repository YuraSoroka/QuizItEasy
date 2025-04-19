using QuizItEasy.API.Common;

namespace QuizItEasy.API.Endpoints;

public class CreateSingleSelect : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("single-select", new Func<object>(() =>
            {
                return Results.Ok();
            }))
            .WithTags(Tags.SingleSelect);
    }
}

using QuizItEasy.API.Common;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.API.Endpoints;

public sealed class CreateSingleSelect : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("single-select", (IMongoDbContext mongoDbContext) =>
            {
                var collection = mongoDbContext.GetCollection<Question>("AZ-204");
                
                IEnumerable<Answer> answers = 
                [
                    Answer.CorrectOption("True"), 
                    Answer.WrongOption("False")
                ];
                collection.InsertOne(SingleSelect.Create(answers, "Test question"));

                return Results.Ok();
            })
            .WithTags(Tags.SingleSelect);
    }
}

namespace QuizItEasy.Application.Common.Options;

public record MongoDbOptions
{
    public string ConnectionString { get; init; } = string.Empty;
}

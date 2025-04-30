namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public record SingleSelectResponse
{
    public string Id { get; init; }
    public string Text { get; init; }
    public IEnumerable<SingleSelectAnswerResponse> Answers { get; init; }
};

public record SingleSelectAnswerResponse(Guid Id, string Value);

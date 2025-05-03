namespace QuizItEasy.Application.Features.SingleSelect.GetById;

public record SingleSelectQuestionResponse
{
    public string Id { get; init; }
    public string Text { get; init; }
    public IEnumerable<SingleSelectAnswerResponse> Answers { get; init; }
};

public record SingleSelectAnswerResponse(Guid Id, string Value);

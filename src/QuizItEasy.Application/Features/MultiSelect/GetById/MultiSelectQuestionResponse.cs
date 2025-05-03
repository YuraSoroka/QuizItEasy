using QuizItEasy.Application.Features.SingleSelect.GetById;

namespace QuizItEasy.Application.Features.MultiSelect.GetById;

public record MultiSelectQuestionResponse
{
    public string Id { get; init; }
    public string Text { get; init; }
    public IEnumerable<MultiSelectAnswerResponse> Answers { get; init; }
}

public record MultiSelectAnswerResponse(Guid Id, string Value);

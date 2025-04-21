using MediatR;

namespace QuizItEasy.Application.Features.SingleSelect;

public sealed record CreateSingleSelectCommand(
    string QuestionText,
    IEnumerable<CreateSingleSelectAnswerRequest> Answers
    ) : IRequest;

public sealed record CreateSingleSelectAnswerRequest(
    string Value,
    bool IsCorrect);

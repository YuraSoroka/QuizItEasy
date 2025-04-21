using QuizItEasy.Application.Common.Messaging;

namespace QuizItEasy.Application.Features.SingleSelect;

public sealed record CreateSingleSelectCommand(
    string QuestionText,
    IEnumerable<CreateSingleSelectAnswerRequest> Answers
    ) : ICommand;

public sealed record CreateSingleSelectAnswerRequest(
    string Value,
    bool IsCorrect);

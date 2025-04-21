using QuizItEasy.Application.Common.Messaging;

namespace QuizItEasy.Application.Features.SingleSelect.Create;

public sealed record CreateSingleSelectCommand(
    string QuestionText,
    IEnumerable<CreateSingleSelectAnswerRequest> Answers
    ) : ICommand<string>;
    
public sealed record CreateSingleSelectAnswerRequest(
    string Value,
    bool IsCorrect);

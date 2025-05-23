﻿using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.Application.Features.SingleSelect.CheckAnswer;

public record CheckSingleSelectAnswerCommand(string QuestionId, Guid AnswerId)
    : ICommand<bool>;

public class CheckAnswerCommandHandler(IMongoRepository<Question> questionRepository)
    : ICommandHandler<CheckSingleSelectAnswerCommand, bool>
{
    public async Task<Result<bool>> Handle(CheckSingleSelectAnswerCommand request, CancellationToken cancellationToken)
    {
        if (await questionRepository.FindByIdAsync(request.QuestionId) is not SingleSelectQuestion singleSelect)
        {
            return Result.Failure<bool>(Error.NotFound("404", "Not found"));
        }

        return singleSelect.IsCorrectAnswer(request.AnswerId);
    }
}

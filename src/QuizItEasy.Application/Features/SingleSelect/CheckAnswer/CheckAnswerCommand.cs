using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizItEasy.Application.Common.Messaging;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;
using SingleSelectQuestion = QuizItEasy.Domain.Entities.Questions.SingleSelect;

namespace QuizItEasy.Application.Features.SingleSelect.CheckAnswer;
public record CheckAnswerCommand(string QuestionId, Guid AnswerId) : ICommand<bool>;

public class CheckAnswerCommandHandler(IMongoRepository<Question> questionRepository) : ICommandHandler<CheckAnswerCommand, bool>
{
    public async Task<Result<bool>> Handle(CheckAnswerCommand request, CancellationToken cancellationToken)
    {
        if (await questionRepository
            .FindByIdAsync(request.QuestionId) is not SingleSelectQuestion singleSelect)
        {
            return Result.Failure<bool>(Error.NotFound("404", "Not found"));
        }

        return singleSelect.Answers
            .Any(a => a.Id == request.AnswerId && a.IsCorrect);
    }
}

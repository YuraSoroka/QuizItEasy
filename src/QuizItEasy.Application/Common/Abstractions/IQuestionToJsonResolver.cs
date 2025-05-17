using QuizItEasy.Application.Common.Models;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Application.Common.Abstractions;

public interface IQuestionToJsonResolver
{
    string QuestionDiscriminator { get; }
    QuestionResponse Resolve(Question question);
}

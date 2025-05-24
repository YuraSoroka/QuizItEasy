using System.Text.Json;
using Mapster;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Models;
using QuizItEasy.Application.Features.SingleSelect.GetById;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;

namespace QuizItEasy.Application.Common.Utility.QuestionToJsonResolvers;

internal class SingleSelectQuestionResolver : IQuestionToJsonResolver
{
    public string QuestionDiscriminator => nameof(SingleSelectQuestion);

    public QuestionResponse Resolve(Question question)
    {
        if (question is not SingleSelectQuestion singleSelectQuestion)
        {
            throw new ArgumentException("Invalid question type", nameof(question));
        }

        var singleSelectResponse = singleSelectQuestion.Adapt<SingleSelectQuestionResponse>();

        return new QuestionResponse(QuestionDiscriminator, JsonSerializer.Serialize(singleSelectResponse, JsonDefaults.Options));
    }
}

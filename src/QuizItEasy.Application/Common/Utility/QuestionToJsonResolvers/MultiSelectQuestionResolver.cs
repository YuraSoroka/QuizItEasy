using System.Text.Json;
using Mapster;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Models;
using QuizItEasy.Application.Features.MultiSelect.GetById;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.MultiSelect;

namespace QuizItEasy.Application.Common.Utility.QuestionToJsonResolvers;

internal class MultiSelectQuestionResolver : IQuestionToJsonResolver
{
    public string QuestionDiscriminator => nameof(MultiSelectQuestion);

    public QuestionResponse Resolve(Question question)
    {
        if (question is not MultiSelectQuestion multiSelectQuestion)
        {
            throw new ArgumentException("Invalid question type", nameof(question));
        }

        var multiSelectResponse = multiSelectQuestion.Adapt<MultiSelectQuestionResponse>();

        return new QuestionResponse(QuestionDiscriminator, JsonSerializer.Serialize(multiSelectResponse, JsonDefaults.Options));
    }
}

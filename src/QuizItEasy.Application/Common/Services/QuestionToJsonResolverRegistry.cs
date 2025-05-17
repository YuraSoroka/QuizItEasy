using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Models;
using QuizItEasy.Domain.Common.Extensions;
using QuizItEasy.Domain.Entities.Common;

namespace QuizItEasy.Application.Common.Services;

public class QuestionToJsonResolverRegistry
{
    private readonly Dictionary<string, IQuestionToJsonResolver> _questionResolvers;

    public QuestionToJsonResolverRegistry(IEnumerable<IQuestionToJsonResolver> questionResolvers)
    {
        _questionResolvers = questionResolvers.ToDictionary(m => m.QuestionDiscriminator);
    }

    internal QuestionResponse ResolveAsJson(Question question)
    {
        var discriminator = DomainExtensions.GetTypeDiscriminator(question);

        if (_questionResolvers.TryGetValue(discriminator, out var resolver))
        {
            return resolver.Resolve(question);
        }

        throw new NotSupportedException($"No mapper registered for question type: {discriminator}");
    }
}

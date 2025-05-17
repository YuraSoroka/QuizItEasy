using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Services;
using QuizItEasy.Application.Common.Utility.QuestionToJsonResolvers;

namespace QuizItEasy.Application;

public static class ConfigureServices
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(AssemblyReference.Assembly);

            //config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            //config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            //config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(AssemblyReference.Assembly);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        // question to json resolvers registration
        services.AddSingleton<IQuestionToJsonResolver, SingleSelectQuestionResolver>();
        services.AddSingleton<IQuestionToJsonResolver, MultiSelectQuestionResolver>();
        services.AddSingleton<QuestionToJsonResolverRegistry>();
    }
}

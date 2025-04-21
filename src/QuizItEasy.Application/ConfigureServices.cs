using Microsoft.Extensions.DependencyInjection;

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
    }
}

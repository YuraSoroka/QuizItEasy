using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Infrastructure.Persistence;

namespace QuizItEasy.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMongoDbContext, MongoDbContext>();

        services
            .AddHealthChecks()
            .AddMongoDb(
                sp => sp.GetRequiredService<IMongoDbContext>().Database,
                "MongoDB",
                HealthStatus.Unhealthy,
                ["DB", "NoSql", "Mongo"],
                timeout: TimeSpan.FromSeconds(5));
    }
}

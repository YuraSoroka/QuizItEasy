using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.Questions;
using QuizItEasy.Domain.Entities.QuizCollections;
using QuizItEasy.Infrastructure.Persistence;

namespace QuizItEasy.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration.GetConnectionString("Mongo");
        var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoConnectionString);

        //mongoClientSettings.ClusterConfigurator = c => c.Subscribe(
        //    new DiagnosticsActivityEventSubscriber(
        //        new InstrumentationOptions
        //        {
        //            CaptureCommandText = true
        //        }));

        services.AddSingleton<IMongoClient>(new MongoClient(mongoClientSettings));
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        services
            .AddHealthChecks()
            .AddMongoDb(
                sp => sp.GetRequiredService<IMongoDbContext>().Database,
                "MongoDB",
                HealthStatus.Unhealthy,
                ["DB", "NoSql", "Mongo"],
                TimeSpan.FromSeconds(5));

        RegisterEntitiesMongoConfiguration();
    }

    private static void RegisterEntitiesMongoConfiguration()
    {
        BsonClassMap.RegisterClassMap<Entity>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapIdMember(e => e.Id);
        });

        BsonClassMap.RegisterClassMap<Question>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapMember(e => e.Text);
            classMap.MapMember(e => e.Image);
        });

        BsonClassMap.RegisterClassMap<SingleSelect>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapMember(e => e.Answers);
            classMap.SetDiscriminator(nameof(SingleSelect));
            classMap.MapCreator(u => SingleSelect.Create(u.Answers, u.Text, u.QuizCollectionId, u.Image));
        });

        BsonClassMap.RegisterClassMap<QuizCollection>(classMap =>
        {
            classMap.AutoMap();
        });
    }
}




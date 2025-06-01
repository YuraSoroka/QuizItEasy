using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using QuizItEasy.Application.Common.Abstractions;
using QuizItEasy.Application.Common.Services;
using QuizItEasy.Domain.Common;
using QuizItEasy.Domain.Entities.Common;
using QuizItEasy.Domain.Entities.MultiSelect;
using QuizItEasy.Domain.Entities.Questions;
using QuizItEasy.Domain.Entities.QuizCollections;
using QuizItEasy.Infrastructure.Persistence;
using QuizItEasy.Infrastructure.Storage;

namespace QuizItEasy.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //mongoClientSettings.ClusterConfigurator = c => c.Subscribe(
        //    new DiagnosticsActivityEventSubscriber(
        //        new InstrumentationOptions
        //        {
        //            CaptureCommandText = true
        //        }));

        services.AddSingleton<IMongoClient>(_ =>
        {
            var mongoConnectionString = configuration.GetConnectionString("Mongo");
            var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoConnectionString);

            mongoClientSettings.ClusterConfigurator = cb =>
            {
                cb.Subscribe<CommandStartedEvent>(e =>
                {
                    Console.WriteLine($"MongoDB Command Started: {e.CommandName} - {e.Command.ToJson()}");
                });
            };

            return new MongoClient(mongoClientSettings);
        });

        services.AddSingleton<BlobServiceClient>(_ => new BlobServiceClient(configuration.GetConnectionString("AzureStorageAccount")));
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();
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
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        if (!BsonClassMap.IsClassMapRegistered(typeof(Entity)))
        {
            BsonClassMap.RegisterClassMap<Entity>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapIdMember(e => e.Id);
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(Question)))
        {
            BsonClassMap.RegisterClassMap<Question>(classMap =>
            {
                classMap.AutoMap();
                classMap.MapMember(e => e.Text);
                classMap.MapMember(e => e.Image);
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(SingleSelectQuestion)))
        {
            BsonClassMap.RegisterClassMap<SingleSelectQuestion>(classMap =>
            {
                classMap.AutoMap();

                classMap
                    .MapField("_answers")
                    .SetElementName("Answers");

                classMap.SetDiscriminator(nameof(SingleSelectQuestion));
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(MultiSelectQuestion)))
        {
            BsonClassMap.RegisterClassMap<MultiSelectQuestion>(classMap =>
            {
                classMap.AutoMap();

                classMap
                    .MapField("_answers")
                    .SetElementName("Answers");

                classMap.SetDiscriminator(nameof(MultiSelectQuestion));
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(QuizCollection)))
        {
            BsonClassMap.RegisterClassMap<QuizCollection>(classMap =>
            {
                classMap.AutoMap();
            });
        }
    }
}




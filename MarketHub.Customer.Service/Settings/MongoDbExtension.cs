using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MarketHub.CustomerService.Settings;

public static class MongoDbExtension
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            var servicesSettings = configuration!.GetSection(nameof(ServiceSetting)).Get<ServiceSetting>();

            var mongoDbSettings = configuration!.GetSection(nameof(MongoDbSetting)).Get<MongoDbSetting>();
            var MongoClient = new MongoClient(mongoDbSettings!.ConnectionString);
            return MongoClient.GetDatabase(servicesSettings!.ServiceName);
        });

        return services;
    }

}

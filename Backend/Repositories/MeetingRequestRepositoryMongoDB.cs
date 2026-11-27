using Core;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Backend.Repositories;

public class MeetingRequestRepositoryMongoDB : IMeetingRequestRepository
{
    // Forbindelsen til samlingen "MeetingRequests" i MongoDB
    private readonly IMongoCollection<MeetingRequest> meetingRequestCollection;

    /*----Forbindelse til MongoDB----*/
    public MeetingRequestRepositoryMongoDB(IConfiguration configuration)
    {
        // Konfigurerer MeetingRequest_Id som automatisk genereret ObjectId
        if (!BsonClassMap.IsClassMapRegistered(typeof(MeetingRequest)))
        {
            BsonClassMap<MeetingRequest> map = new BsonClassMap<MeetingRequest>();
            map.AutoMap(); //gør så modellens properties automatisk mapper til dokumentets felter

            map.MapIdMember(m => m.MeetingRequest_Id) //definere meetingRequest_Id som dokumentets id
                .SetSerializer(new StringSerializer(BsonType.ObjectId)) //Sørger for at Id'erne gemmes som objectId i mongoDB, selvom det er en string i c#
                .SetIdGenerator(StringObjectIdGenerator.Instance);//Sørger for automatisk genering af id'et når det ikke er angivet

            BsonClassMap.RegisterClassMap(map);//registrerer konfigurationen hos MongoDB driveren
        }

        // Henter connection string fra User Secrets
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        meetingRequestCollection = database.GetCollection<MeetingRequest>("MeetingRequests");
    }

    /*----Hent request ud fra ID----*/
    public MeetingRequest? GetById(string meetingRequest_Id)
    {
        return meetingRequestCollection.Find(m => m.MeetingRequest_Id == meetingRequest_Id).FirstOrDefault();
    }

    /*----Opret ny meetingRequest----*/
    public MeetingRequest Add(MeetingRequest meetingRequest)
    {
        // Gemmer forespørgslen i MongoDB, hvor driveren automatisk genererer et ObjectId
        meetingRequestCollection.InsertOne(meetingRequest);

        // Returnerer den oprettede forespørgsel
        return meetingRequest;
    }
}
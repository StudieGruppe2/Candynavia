using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class MeetingRequestRepositoryMongoDB : IMeetingRequestRepository
{
    //Forbindelsen til samlingen "MeetingRequest" i mongoDB
    private readonly IMongoCollection<MeetingRequest> meetingRequestCollection;
     
    /*----Forbindelse til MongoDB----*/
    //Constructoren køres når repositoriet bliver oprettet
    public MeetingRequestRepositoryMongoDB(IConfiguration configuration)
    {
        // Fortæller MongoDB, at MeetingRequest_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(MeetingRequest)))
        {
            BsonClassMap<MeetingRequest> map = new BsonClassMap<MeetingRequest>();
            map.AutoMap();
            map.MapIdMember(typeof(MeetingRequest).GetProperty("MeetingRequest_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Henter connection stringen fra User Secrets (ligger på hver persons egen computer)
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        meetingRequestCollection = database.GetCollection<MeetingRequest>("MeetingRequests");
    } 
     
    /*----Hent request udfra id----*/
    MeetingRequest? GetById(int meetingRequestId)
    {
         
    }
     
    /*----Opret ny meetingrequest----*/
    MeetingRequest Add(MeetingRequest meetingRequest)
    {
         
    }
}
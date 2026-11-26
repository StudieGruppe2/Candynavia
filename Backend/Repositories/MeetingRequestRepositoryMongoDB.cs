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
        //Fortæller MongoDB, at MeetingRequest_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(MeetingRequest)))
        {
            BsonClassMap<MeetingRequest> map = new BsonClassMap<MeetingRequest>();
            map.AutoMap();
            map.MapIdMember(typeof(MeetingRequest).GetProperty("MeetingRequest_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        //Henter connection stringen fra User Secrets (ligger på hver persons egen computer)
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        meetingRequestCollection = database.GetCollection<MeetingRequest>("MeetingRequests");
    } 
     
    
    
    /*----Hent request udfra id----*/
   public MeetingRequest? GetById(int meetingRequest_Id)
    {
        return meetingRequestCollection.Find(m => m.MeetingRequest_Id == meetingRequest_Id).FirstOrDefault();
    }
   
   
   
    // TODO: Overvej at anvende MongoDBs automatisk genererede ObjectId frem for selvdefinerede heltals-ID'er.
    // Den nuværende ID-generering (højeste ID + 1) kan medføre ID-konflikter ved samtidige mødeforespørgsler.
    /*----Opret ny meetingrequest----*/
    public MeetingRequest Add(MeetingRequest meetingRequest)
    {
        //Finder forespørglsen med det højeste ID
        FilterDefinition<MeetingRequest> filter = Builders<MeetingRequest>.Filter.Empty;
        SortDefinition<MeetingRequest> sort = Builders<MeetingRequest>.Sort.Descending(m => m.MeetingRequest_Id);
        
        MeetingRequest? last = meetingRequestCollection.Find(filter).Sort(sort).FirstOrDefault();
        
        //Hvis databasen er tom, starter ID på 1
        if (last == null)
        {
            //Ellers får forespørgslen et ID, der er 1 højere end det højeste
            meetingRequest.MeetingRequest_Id = 1;
        }
        else
        {
            //Ellers får annoncen et ID, der er 1 højere end det højeste
            meetingRequest.MeetingRequest_Id = last.MeetingRequest_Id + 1;
        }
        
        //Gemmer den nye forespørgsel i MongoDB
        meetingRequestCollection.InsertOne(meetingRequest);
        
        //Returnerer den oprettede forespørgsel
        return meetingRequest;
    }
}
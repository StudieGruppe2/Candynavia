using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class PostalCodeRepositoryMongoDB
{
    //Forbindelsen til samlingen "MeetingRequest" i mongoDB
    private readonly IMongoCollection<PostalCodeArea> postalCodeAreasCollection;
     
    /*----Forbindelse til MongoDB----*/
    //Constructoren køres når repositoriet bliver oprettet
    public PostalCodeRepositoryMongoDB(IConfiguration configuration)
    {
        // Fortæller MongoDB, at MeetingRequest_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(PostalCodeArea)))
        {
            BsonClassMap<MeetingRequest> map = new BsonClassMap<MeetingRequest>();
            map.AutoMap();
            map.MapIdMember(typeof(MeetingRequest).GetProperty("PostalCodeArea_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Henter connection stringen fra User Secrets (ligger på hver persons egen computer)
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        postalCodeAreasCollection = database.GetCollection<PostalCodeArea>("PostalCodeAreas");
    } 
    
    // Henter alle postnumre
    public List<PostalCodeArea> GetAllPostalCodes()
    {
        
    }

    //Henter et specifikt postnummer
    PostalCodeArea? GetByPostalCode(string PostalCode)
    {
        
    }
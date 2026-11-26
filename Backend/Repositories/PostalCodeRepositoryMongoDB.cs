using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class PostalCodeRepositoryMongoDB : IPostalCodeRepository
{
    //Forbindelsen til samlingen "MeetingRequest" i mongoDB
    private readonly IMongoCollection<PostalCodeArea> postalCodeCollection;
     
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

        postalCodeCollection = database.GetCollection<PostalCodeArea>("PostalCodeAreas");
    } 
    
    // Henter alle postnumre
    public List<PostalCodeArea> GetAllPostalCodes()
    {
        // Et tomt filter betyder, at alle postalCodes skal hentes
        FilterDefinition<PostalCodeArea> filter = Builders<PostalCodeArea>.Filter.Empty;


        return postalCodeCollection.Find(filter).ToList();


    }

    //Henter et specifikt postnummer
   public PostalCodeArea? GetByPostalCode(string PostalCode)
    {
        
        // Finder annoncen, hvor _id svarer til Annonce_Id
        FilterDefinition<PostalCodeArea> filter =
            Builders<PostalCodeArea>.Filter.Eq("PostalCode", PostalCode);

        // Returnerer annoncen eller null, hvis den ikke findes
        return postalCodeCollection.Find(filter).FirstOrDefault();
    }
    }
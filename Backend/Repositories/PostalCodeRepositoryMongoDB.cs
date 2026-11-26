using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class PostalCodeRepositoryMongoDB : IPostalCodeRepository
{
    //Forbindelsen til samlingen "PostalCodeAreas" i mongoDB
    private readonly IMongoCollection<PostalCodeArea> postalCodeCollection;

    /*----Forbindelse til MongoDB----*/
    //Constructoren køres når repositoriet bliver oprettet
    public PostalCodeRepositoryMongoDB(IConfiguration configuration)
    {
        // Fortæller MongoDB, at PostalCodeArea_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(PostalCodeArea)))
        {
            BsonClassMap<PostalCodeArea> map = new BsonClassMap<PostalCodeArea>();
            map.AutoMap();
            map.MapIdMember(typeof(PostalCodeArea).GetProperty("PostalCodeArea_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Henter connection stringen fra User Secrets
        string connectionString = configuration["MongoDB:ConnectionString"]!;

        // Opretter MongoDB-klienten
        MongoClient client = new MongoClient(connectionString);

        // Vælger databasen
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        // Vælger collection
        postalCodeCollection = database.GetCollection<PostalCodeArea>("PostalCodeAreas");
    }

    /*
    // Henter alle postnumre
    public List<PostalCodeArea> GetAllPostalCodes()
    {
        // Et tomt filter betyder, at alle postalCodes skal hentes
        FilterDefinition<PostalCodeArea> filter = Builders<PostalCodeArea>.Filter.Empty;


        return postalCodeCollection.Find(filter).ToList();
    }
    */ // Bruger vi den rent faktisk??

   
    //Henter et specifikt postnummer
    public PostalCodeArea? GetByPostalCode(string postalCode)
    {
        // Returnerer postnummerområdet eller null, hvis det ikke findes
        return postalCodeCollection.Find(p => p.PostalCode == postalCode).FirstOrDefault();
    }
    // TODO: Opret et unikt indeks på PostalCode i MongoDB, så samme postnummer ikke kan tilknyttes flere sælgere.
}

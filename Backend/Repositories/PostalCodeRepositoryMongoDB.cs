using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class PostalCodeRepositoryMongoDB : IPostalCodeRepository
{
    //Forbindelsen til samlingen "PostalCodeAreas" i mongoDB
    private readonly IMongoCollection<PostalCodeArea> postalCodeCollection;

    /*Forbindelse til MongoDB-*/
    //Constructoren køres når repositoriet bliver oprettet
    public PostalCodeRepositoryMongoDB(IConfiguration configuration)
    {
        //Fortæller MongoDB, at PostalCodeArea_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(PostalCodeArea)))
        {
            BsonClassMap<PostalCodeArea> map = new BsonClassMap<PostalCodeArea>();
            
            map.AutoMap();//Mapper klassens properties automatisk
            
            map.MapIdMember(p => p.PostalCodeArea_Id);//Mapper PostalCodeArea_Id til MongoDBs _id
            
            BsonClassMap.RegisterClassMap(map);//Registrerer mappingen
        }

        //Henter connection string fra applikationens konfiguration
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        
        MongoClient client = new MongoClient(connectionString); //Opretter klienten til kommunikation med MongoDB
        
        IMongoDatabase database = client.GetDatabase("CandynaviaDB"); //Vælger databasen
        
        postalCodeCollection = database.GetCollection<PostalCodeArea>("PostalCodeAreas"); //Vælger collectionen med postnummerområder
    }
    
    //Henter et specifikt postnummer
    public PostalCodeArea? GetByPostalCode(string postalCode)
    {
        // Returnerer postnummerområdet eller null, hvis det ikke findes
        return postalCodeCollection.Find(p => p.PostalCode == postalCode).FirstOrDefault();
    } }
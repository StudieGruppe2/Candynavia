using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class SalesPersonRepositoryMongoDB : ISalesPersonRepository
{
    //Forbindelsen til samlingen "SalesPersons" i mongoDB
    private readonly IMongoCollection<SalesPerson> salesPersonCollection;

    /*Forbindelse til MongoDB*/
    //Constructoren køres når repositoriet bliver oprettet
    public SalesPersonRepositoryMongoDB(IConfiguration configuration)
    {
        // ortæller MongoDB, at SalesPerson_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(SalesPerson)))
        {
            BsonClassMap<SalesPerson> map =
                new BsonClassMap<SalesPerson>();
            
            map.AutoMap(); //Mapper klassens properties automatisk
            
            map.MapIdMember(s => s.SalesPerson_Id); //Mapper SalesPerson_Id til MongoDBs _id
            
            BsonClassMap.RegisterClassMap(map); //Registrerer mappingen
        }

        //Henter connection string fra applikationens konfiguration
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        
        MongoClient client = new MongoClient(connectionString); //Opretter klienten til MongoDB
        
        IMongoDatabase database = client.GetDatabase("CandynaviaDB"); //Vælger databasen

      
        salesPersonCollection = database.GetCollection<SalesPerson>("SalesPersons"); //Vælger collectionen med sælgere
    }


    /*Hent sælger udfra id*/
    public SalesPerson? GetSalesPersonById(int salesPersonId)
    {
        return salesPersonCollection.Find(s => s.SalesPerson_Id == salesPersonId).FirstOrDefault();
    }
}
using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Backend.Repositories;

public class SalesPersonRepositoryMongoDB : ISalesPersonRepository
{
    //Forbindelsen til samlingen "SalesPersons" i mongoDB
    private readonly IMongoCollection<SalesPerson> salesPersonCollection;

    /*----Forbindelse til MongoDB----*/
    //Constructoren køres når repositoriet bliver oprettet
    public SalesPersonRepositoryMongoDB(IConfiguration configuration)
    {
        // Fortæller MongoDB, at SalesPerson_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(SalesPerson)))
        {
            BsonClassMap<SalesPerson> map = new BsonClassMap<SalesPerson>();
            map.AutoMap();
            map.MapIdMember(typeof(SalesPerson).GetProperty("SalesPerson_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Henter connection stringen fra User Secrets (ligger på hver persons egen computer)
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        salesPersonCollection = database.GetCollection<SalesPerson>("SalesPersons");
    }

    /*----Hent sælger udfra id----*/
    public SalesPerson? GetSalesPersonById(int salesPersonId)
    {
        return salesPersonCollection.Find(s => s.SalesPerson_Id == salesPersonId).FirstOrDefault();
    }
}
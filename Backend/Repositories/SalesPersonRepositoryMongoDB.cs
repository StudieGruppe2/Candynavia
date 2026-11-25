using Core;
using MongoDB.Driver;

namespace Backend.Repositories;

public class SalesPersonRepositoryMongoDB : ISalesPersonRepository
{
    private readonly IMongoCollection<SalesPerson> salesPersonCollection;

    public SalesPersonRepositoryMongoDB(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
        var database = client.GetDatabase("Candynavia");
        salesPersonCollection = database.GetCollection<SalesPerson>("SalesPersons");
    }

    public SalesPerson? GetSalesPersonById(int salesPersonId)
    {
        return salesPersonCollection.Find(s => s.SalesPerson_Id == salesPersonId).FirstOrDefault();
    }
}
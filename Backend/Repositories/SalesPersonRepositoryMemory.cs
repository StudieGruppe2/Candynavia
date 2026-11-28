using System.Text.Json;
using Core;

namespace Backend.Repositories;

public class SalesPersonRepositoryMemory : ISalesPersonRepository
{
    // Listen gemmer sælgerne i hukommelsen i stedet for en database
    private readonly List<SalesPerson> salesPersons;


    public SalesPersonRepositoryMemory()
    {
        // Læser testdata fra Data/salespersons.json
        string json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "salespersons.json"));
        salesPersons = JsonSerializer.Deserialize<List<SalesPerson>>(json) ?? new List<SalesPerson>();
    }

    //Henter en sælger udfra id
    public SalesPerson? GetSalesPersonById(int salesPersonId)
    {
        return salesPersons.FirstOrDefault(s => s.SalesPerson_Id == salesPersonId);
    }
}
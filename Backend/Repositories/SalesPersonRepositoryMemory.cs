using Core;

namespace Backend.Repositories;

public class SalesPersonRepositoryMemory : ISalesPersonRepository
{
    // Listen gemmer sælgerne i hukommelsen i stedet for en database
    private readonly List<SalesPerson> salesPersons = new();

    //Henter en sælger udfra id
    public SalesPerson? GetSalesPersonById(int salesPersonId)
    {
        return salesPersons.FirstOrDefault(s => s.SalesPerson_Id == salesPersonId);
    }
}
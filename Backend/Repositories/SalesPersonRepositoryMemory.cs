using Core;

namespace Backend.Repositories;

public class SalesPersonRepositoryMemory : ISalesPersonRepository
{
    private readonly List<SalesPerson> salesPersons = new();

    public SalesPerson? GetSalesPersonById(int salesPersonId)
    {
        return salesPersons.FirstOrDefault(s => s.SalesPerson_Id == salesPersonId);
    }
}
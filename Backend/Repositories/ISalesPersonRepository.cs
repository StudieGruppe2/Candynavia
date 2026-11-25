using Core;

namespace Backend.Repositories;

public interface ISalesPersonRepository
{
    SalesPerson? GetSalesPersonById(int salesPersonId);
}
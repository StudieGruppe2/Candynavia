using Core; // Giver adgang til modelklasserne

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public interface ISalesPersonRepository
{
    //Henter en sælger udfra id
    SalesPerson? GetSalesPersonById(int salesPersonId);
}
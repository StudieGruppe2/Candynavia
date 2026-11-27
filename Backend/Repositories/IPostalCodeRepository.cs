using Core;

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public interface IPostalCodeRepository
{
    
    //Henter et specifikt postnummer og returnere null hvis postnummeret ikke findes
   PostalCodeArea? GetByPostalCode(string postalCode);

}
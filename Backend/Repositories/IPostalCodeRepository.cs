using Core;

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public class IPostalCodeRepository
{
    // Henter alle postnumre
    List<PostalCodeArea> GetAllPostalCodes();
    
    //Henter et specifikt postnummer
    PostalCodeArea? GetByPostalCode(string PostalCode);

}
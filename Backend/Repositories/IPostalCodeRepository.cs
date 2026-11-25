using Core;

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public interface IPostalCodeRepository
{
    // Henter alle postnumre
    public List<PostalCodeArea> GetAllPostalCodes();
    
    //Henter et specifikt postnummer
   public  PostalCodeArea? GetByPostalCode(string PostalCode);

}
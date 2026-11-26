using Core;

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public interface IPostalCodeRepository
{
    // Henter alle postnumre
   // List<PostalCodeArea> GetAllPostalCodes(); // TODO: Kontrollér, om metoden er nødvendig i den nuværende løsning.

    
    //Henter et specifikt postnummer og returnere null hvis postnummeret ikke findes
   PostalCodeArea? GetByPostalCode(string postalCode);

}
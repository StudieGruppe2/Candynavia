using Core;

namespace Backend.Repositories;

public class PostalCodeRepositoryMemory
{
    private List<PostalCodeArea> postalCodeAreas = new List<PostalCodeArea>();
    
    // Henter alle postnumre
    public List<PostalCodeArea> GetAllPostalCodes()
    {
        
    }

    //Henter et specifikt postnummer
   public PostalCodeArea? GetByPostalCode(string PostalCode)
    {
        
    }

}
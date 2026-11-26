using Core;

namespace Backend.Repositories;

public class PostalCodeRepositoryMemory : IPostalCodeRepository
{
    // Listen gemmer Postnumrerne i hukommelsen i stedet for en database
    private List<PostalCodeArea> postalCodeAreas = new List<PostalCodeArea>();
    
    // Henter alle postnumre
    public List<PostalCodeArea> GetAllPostalCodes()
    {
        return postalCodeAreas;
    }

    //Henter et specifikt postnummer
   public PostalCodeArea? GetByPostalCode(string PostalCode)
    {
        return postalCodeAreas.FirstOrDefault(p => p.PostalCode == PostalCode);
    }

}
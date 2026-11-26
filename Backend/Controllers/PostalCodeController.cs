using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PostalCodeController : ControllerBase
{
    //Controlleren bruger interfacet i stedet for et bestemt repository
    //Det kaldes Dependency Inversion og gør det muligt at skifte mellem Memory og MongoDB
    private readonly IPostalCodeRepository postalCodeRepository;

    //Dependency Injection gennem constructoren
    public PostalCodeController(IPostalCodeRepository postalCodeRepository)
    {
        // Gemmer referencerne, så de kan bruges i controllerens metoder
        this.postalCodeRepository = postalCodeRepository;
    }

    /*----Hent alle postnumre----**/
    [HttpGet]
    public ActionResult<List<PostalCodeArea>> GetAll()
    {
        return postalCodeRepository.GetAllPostalCodes();
    }

    /*----Hent bestemt postnummer----*/
    [HttpGet("{postalCode}")]
    public ActionResult<PostalCodeArea> GetByPostalCode(string PostalCode)
    {
        // Leder efter postalCode i repository
        PostalCodeArea? postalCode = postalCodeRepository.GetByPostalCode(PostalCode);

        // Hvis postalcode ikke findes, returneres 404 Not Found
        if (postalCode == null)
        {
            return NotFound("Postal Code does not exist.");
        }
        // Returnerer annoncen til frontend
        return postalCode;
    }

}
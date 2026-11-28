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

    /*Henter et specifikt postnummerområde*/
    [HttpGet("{postalCode}")]
    public ActionResult<PostalCodeArea> GetByPostalCode(string postalCode)
    {
        //Finder postnummerområdet i repositoryet
        PostalCodeArea? postalCodeArea =
            postalCodeRepository.GetByPostalCode(postalCode);

        //Hvis postnummeret ikke findes, returneres 404 Not Found
        if (postalCodeArea == null)
        {
            return NotFound("Postal code does not exist.");
        }

        //Returnerer det fundne postnummerområde
        return postalCodeArea;
    }

}
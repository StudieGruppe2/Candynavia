using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesPersonController : ControllerBase
{
    //Controlleren bruger interfacet i stedet for et bestemt repository
    //Det kaldes Dependency Inversion og gør det muligt at skifte mellem Memory og MongoDB
    private readonly ISalesPersonRepository salesPersonRepository;
    private readonly IPostalCodeRepository postalCodeRepository;

    //Dependency Injection gennem constructoren
    public SalesPersonController(ISalesPersonRepository salesPersonRepository, IPostalCodeRepository postalCodeRepository)
    {
        // Gemmer referencerne, så de kan bruges i controllerens metoder
        this.salesPersonRepository = salesPersonRepository;
        this.postalCodeRepository = postalCodeRepository;
    }

    /*----Hent sælger ud fra postnummer----*/
    [HttpGet("postalcode/{postalCode}")]
    public ActionResult<SalesPerson> GetByPostalCode(string postalCode)
    {
        //Kontrollerer, at postnummeret består af fire tegn
        if (postalCode.Length != 4)
        {
            return BadRequest();
        }

        //Finder postnummerområdet ud fra kundens postnummer
        PostalCodeArea? postalCodeArea =
            postalCodeRepository.GetByPostalCode(postalCode);

        //Kontrollerer, at postnummerområdet findes og har en tilknyttet sælger
        if (postalCodeArea == null || postalCodeArea.SalesPerson_Id == null)
        {
            return NotFound();
        }

        //Finder den tilknyttede sælger - .Value tilgår heltalsværdien i den nullable property
        SalesPerson? salesPerson = 
            salesPersonRepository.GetSalesPersonById(postalCodeArea.SalesPerson_Id.Value);

        //Kontrollerer, at den tilknyttede sælger eksisterer
        if (salesPerson == null)
        {
            return NotFound();
        }

        //Returnerer den fundne sælger til frontend
        return salesPerson;
    }
}
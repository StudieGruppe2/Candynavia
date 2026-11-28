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
            return BadRequest(); //postnummer skal være 4 cifre
        }

        //Finder postnummerområdet ud fra kundens postnummer
        PostalCodeArea? postalCodeArea =
            postalCodeRepository.GetByPostalCode(postalCode);

        //Returnerer 404, hvis postnummerområdet ikke findes
        if (postalCodeArea == null)
        {
            return NotFound();//postkoden findes ikke i systemet
        }

        //Finder den sælger, der er tilknyttet postnummerområdet
        SalesPerson? salesPerson = salesPersonRepository.GetSalesPersonById(postalCodeArea.SalesPerson_Id);

        //Kontrollerer, at den tilknyttede sælger eksisterer
        if (salesPerson == null)
        {
            return NotFound();//der er ingen sælger til postnummeret
        }

        // Returnerer den fundne sælger til frontend
        return salesPerson;
    }
}
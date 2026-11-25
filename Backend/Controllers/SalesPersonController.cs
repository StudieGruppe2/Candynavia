using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesPersonController : ControllerBase
{
    private readonly ISalesPersonRepository salesPersonRepository;
    private readonly IPostalCodeRepository postalCodeRepository;

    public SalesPersonController(ISalesPersonRepository salesPersonRepository, IPostalCodeRepository postalCodeRepository)
    {
        this.salesPersonRepository = salesPersonRepository;
        this.postalCodeRepository = postalCodeRepository;
    }

    [HttpGet("postalcode/{postalCode}")]
    public ActionResult<SalesPerson> GetByPostalCode(string postalCode)
    {
        PostalCodeArea? area = postalCodeRepository.GetByPostalCode(postalCode);
        if (area == null)
        {
            return NotFound();
        }

        SalesPerson? salesPerson = salesPersonRepository.GetSalesPersonById(area.SalesPerson_Id);
        if (salesPerson == null)
        {
            return NotFound();
        }

        return salesPerson;
    }
}
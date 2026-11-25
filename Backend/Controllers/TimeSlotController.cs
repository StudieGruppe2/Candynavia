using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;
[ApiController]
[Route("[controller]")]

public class TimeSlotController : ControllerBase
{ 
    // Controlleren bruger interfacet i stedet for et bestemt repository
    //Det kaldes Dependency Inversion og gør det muligt at skifte mellem Memory og MongoDB
    private readonly ITimeSlotRepository repository; 
    
    // Dependency Injection: Repository modtages gennem constructoren - vi "giver det" i program.cs
    public (ITimeSlotRepository repository)
    { 
        // Gemmer repository, så det kan bruges i controllerens metoder
        this.repository = repository;
    }

    // ---------- HENT Time slot ----------
    // GET /TimeSlot - Henter alle time slots
    [HttpGet]
    public List<TimeSlot> GetAll()
    {
        return repository;
    }

    [HttpGet("{}")]
    public ActionResult<TimeSlot>GetAvailableTimeSlotsBySalesPerson(int SalesPerson_id)
    {
        
    }
    
    
}
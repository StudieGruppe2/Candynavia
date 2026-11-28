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
    private readonly ITimeSlotRepository timeSlotRepository; 
    
    // Dependency Injection: Repository modtages gennem constructoren - vi "giver det" i program.cs
    public TimeSlotController (ITimeSlotRepository timeSlotRepository)
    { 
        // Gemmer repository, så det kan bruges i controllerens metoder
        this.timeSlotRepository = timeSlotRepository;
    }

    /*Hent ledige timeslots udfra sælger_id*/
    [HttpGet("salesperson/{SalesPerson_Id}")]
    public ActionResult<List<TimeSlot>> GetAvailableTimeSlotsBySalesPerson(int salesPerson_Id)
    {
        //Henter alle ledige tidspunkter for sælgeren
        List<TimeSlot> timeSlots = timeSlotRepository.GetAvailableTimeSlots(salesPerson_Id);

        //Returnerer listen til frontend (den kan være tom)
        return timeSlots;
    }
    
    
}
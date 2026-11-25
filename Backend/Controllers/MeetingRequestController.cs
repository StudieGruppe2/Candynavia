using Backend.Repositories;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]")]

public class MeetingRequestController : ControllerBase
{
    //Controlleren bruger interfacet i stedet for et bestemt repository
    //Det kaldes Dependency Inversion og gør det muligt at skifte mellem Memory og MongoDB
    private readonly IMeetingRequestRepository meetingRequestRepository;
    private readonly ITimeSlotRepository TimeSlotRepository;

    //Dependency Injection gennem constructoren
    public MeetingRequestController(IMeetingRequestRepository meetingRequestRepository,
        ITimeSlotRepository timeSlotRepository)
    {
        // Gemmer referencerne, så de kan bruges i controllerens metoder
        this.meetingRequestRepository = meetingRequestRepository;
        this.TimeSlotRepository = timeSlotRepository;
    }


    /*----Hent mødeforespørgelser----*/
    [HttpGet]
    public ActionResult<MeetingRequest> GetRequestById(int MeetingRequest_Id)
    {
        //Leder efter forespørgslen i repository
        MeetingRequest? meetingRequest = meetingRequestRepository.GetById(MeetingRequest_Id);

        //Hvis forespørgslen ikke findes, returneres 404 Not Found
        if (meetingRequest == null)
        {
            return NotFound("The request does not exist");
        }

        //Returnerer forespørgslen til frontend
        return meetingRequest;
    }


    /*----Lav ny mødeforespørgsel----*/
    [HttpPost]
    public ActionResult<MeetingRequest> CreateRequest(MeetingRequest meetingRequest)
    {
        //Validerer mødeforespørgslen
        if (meetingRequest.Description == ""
            || meetingRequest.Consent == false)
        {
            return BadRequest("You must enter a description and give consent");
        }

        //Kontrollerer, at Customer-objektet eksisterer
        if (meetingRequest.Customer == null)
        {
            return BadRequest("Customer information is required");
        }

        //Validerer kundens oplysninger
        if (meetingRequest.Customer.Name == ""
            || meetingRequest.Customer.Email == ""
            || meetingRequest.Customer.Phone == ""
            || meetingRequest.Customer.Company == ""
            || meetingRequest.Customer.Position == "")
        {
            return BadRequest("All customer information must be provided");
        }

        //Backenden sætter oprettelsestidspunktet
        meetingRequest.CreatedAt = DateTime.UtcNow;

        //Backenden sætter status til Pending
        meetingRequest.Status = "Pending";

        //Henter det valgte tidspunkt
        TimeSlot? timeSlot = TimeSlotRepository
            .GetTimeSlotById(meetingRequest.TimeSlot_Id);

        //Kontrollerer, at tidspunktet eksisterer
        if (timeSlot == null)
        {
            return NotFound("The time slot does not exist");
        }

        //Kontrollerer, at tidspunktet er ledigt
        if (timeSlot.Status != "Available")
        {
            return Conflict("The time slot is not available");
        }

        //Reserverer tidspunktet gennem repositoryet
        bool reserved = TimeSlotRepository.ReserveTimeSlot(meetingRequest.TimeSlot_Id, "Reserved");

        if (reserved == false)
        {
            return Conflict("The time slot could not be reserved");
        }

        //Sælgeren følger det valgte tidspunkt
        meetingRequest.SalesPerson_Id = timeSlot.SalesPerson_Id;

        //Gemmer forespørgslen
        MeetingRequest createdRequest =
            meetingRequestRepository.Add(meetingRequest);

        //Returnerer den oprettede forespørgsel
        return Ok(createdRequest);
    }
}
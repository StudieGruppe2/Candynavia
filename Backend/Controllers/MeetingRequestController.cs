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
    private readonly ITimeSlotRepository timeSlotRepository;

    //Dependency Injection gennem constructoren
    public MeetingRequestController(
        IMeetingRequestRepository meetingRequestRepository,
        ITimeSlotRepository timeSlotRepository)
    {
        this.meetingRequestRepository = meetingRequestRepository;
        this.timeSlotRepository = timeSlotRepository;
    }


    /*----Hent mødeforespørgelser----*/
    [HttpGet]
    public ActionResult<MeetingRequest> GetRequestById(string meetingRequest_Id)
    {
        //Leder efter forespørgslen i repository
        MeetingRequest? meetingRequest = meetingRequestRepository.GetById(meetingRequest_Id);

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
        // Validerer mødeforespørgslen
        if (string.IsNullOrWhiteSpace(meetingRequest.Description) //IsNullOrWhiteSpace bruges til at tjekke om der blot er skrevet et mellemrum
            || meetingRequest.Consent == false)
        {
            return BadRequest("You must enter a description and give consent");
        }

        //Kontrollerer, at Customer-objektet eksisterer
        if (meetingRequest.Customer == null)
        {
            return BadRequest("Customer information is required");
        }
        
        // Validerer kundens oplysninger
        if (string.IsNullOrWhiteSpace(meetingRequest.Customer.Name)//IsNullOrWhiteSpace bruges til at tjekke om der blot er skrevet et mellemrum
            || string.IsNullOrWhiteSpace(meetingRequest.Customer.Email)
            || string.IsNullOrWhiteSpace(meetingRequest.Customer.Phone)
            || string.IsNullOrWhiteSpace(meetingRequest.Customer.Company)
            || string.IsNullOrWhiteSpace(meetingRequest.Customer.Position))
        {
            return BadRequest("All customer information must be provided");
        }
        
        //Validerer formatet på kundens email
        if (!meetingRequest.Customer.Email.Contains("@") //skal indeholde @
            || !meetingRequest.Customer.Email.Contains(".")) //Skal indeholde .
        {
            return BadRequest("Invalid email address");
        }

        //Backenden sætter oprettelsestidspunktet
        meetingRequest.CreatedAt = DateTime.UtcNow;

        //Backenden sætter status til Pending
        meetingRequest.Status = "Pending";

        //Henter det valgte tidspunkt
        TimeSlot? timeSlot = timeSlotRepository.GetTimeSlotById(meetingRequest.TimeSlot_Id);

        //Kontrollerer, at tidspunktet eksisterer
        if (timeSlot == null)
        {
            return NotFound("The time slot does not exist");
        }

        //Reserverer tidspunktet gennem repositoryet
        bool reserved = timeSlotRepository.ReserveTimeSlot(meetingRequest.TimeSlot_Id);

        if (reserved == false)
        {
            return Conflict("The time slot could not be reserved");
        }

        //Sælgeren følger det valgte tidspunkt
        meetingRequest.SalesPerson_Id = timeSlot.SalesPerson_Id;

        //Gemmer forespørgslen
        MeetingRequest createdRequest = meetingRequestRepository.Add(meetingRequest);

        //Returnerer den oprettede forespørgsel
        return Ok(createdRequest);
    }
}
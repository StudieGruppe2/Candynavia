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
    public ActionResult<MeetingRequest> GetRequestById(int meetingRequest_Id)
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
        if (meetingRequest.Description == "" // TODO: Måske vi skal anvende IsNullOrWhiteSpace alligevel, da den sikre at man ikke bare skriver et mellemrum eks??
            || meetingRequest.Consent == false)
        {
            return BadRequest("You must enter a description and give consent");
        }

        //Kontrollerer, at Customer-objektet eksisterer
        if (meetingRequest.Customer == null)
        {
            return BadRequest("Customer information is required");
        }

        // TODO: Valider formatet på kundens e-mailadresse og telefonnummer.
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
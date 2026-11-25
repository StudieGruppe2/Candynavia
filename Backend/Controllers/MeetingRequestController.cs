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
        // Gemmer referencerne, så de kan bruges i controllerens metoder
        this.meetingRequestRepository = meetingRequestRepository;
        this.timeSlotRepository = timeSlotRepository;
    }
   
    
    /*----Hent mødeforespørgelser----*/
    [HttpGet]
    public ActionResult<MeetingRequest> GetRequestById(int MeetingRequest_Id)
    {
        return 
    }

    /*----Lav ny mødeforespørgsel----*/
    [HttpPost]
    public ActionResult<MeetingRequest> CreateRequest(MeetingRequest meetingRequest)
    {
        return
    }

}
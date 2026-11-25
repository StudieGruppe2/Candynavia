using Core;

namespace Backend.Repositories;

public class MeetingRequestRepositoryMemory : IMeetingRequestRepository
{
    // Listen gemmer forespørgslerne i hukommelsen i stedet for en database
    private List<MeetingRequest> meetingRequests = new List<MeetingRequest>();
    
    /*----Hent request udfra id----*/
    MeetingRequest? GetById(int meetingRequestId)
    {
         
    }
     
    /*----Opret ny meetingrequest----*/
    MeetingRequest Add(MeetingRequest meetingRequest)
    {
         
    }

}
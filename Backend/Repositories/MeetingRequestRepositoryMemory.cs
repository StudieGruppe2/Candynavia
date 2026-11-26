using System.Text.Json;
using Core;

namespace Backend.Repositories;

public class MeetingRequestRepositoryMemory : IMeetingRequestRepository
{



    // Listen gemmer forespørgslerne i hukommelsen i stedet for en database
    private List<MeetingRequest> meetingRequests = new List<MeetingRequest>();


    public MeetingRequestRepositoryMemory()
{
    // Læser testdata fra Data/meetingrequests.json
    string json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "meetingrequests.json"));
    meetingRequests = JsonSerializer.Deserialize<List<MeetingRequest>>(json) ?? new List<MeetingRequest>();
}
    
    /*----Hent request udfra id----*/
   public  MeetingRequest? GetById(int meetingRequest_Id)
   {
       return meetingRequests.FirstOrDefault(m => m.MeetingRequest_Id == meetingRequest_Id);
   }
     
    /*----Opret ny forespørgsel----*/
   public MeetingRequest Add(MeetingRequest meetingRequest)
    {
       Add(meetingRequest);
       return meetingRequest;  
    }

}
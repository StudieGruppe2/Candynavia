using System.Text.Json;
using Core;

namespace Backend.Repositories;

public class MeetingRequestRepositoryMemory : IMeetingRequestRepository
{
    //Listen gemmer forespørgslerne i hukommelsen i stedet for en database
    private readonly List<MeetingRequest> meetingRequests = new List<MeetingRequest>();
    
    // Sikrer, at kun én forespørgsel ad gangen kan ændre listen
    private readonly object lockObject = new object();


    public MeetingRequestRepositoryMemory()
{
    //Læser testdata fra Data/meetingrequests.json
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
       //Sikrer, at kun en ad gangen udfører denne kode
       lock (lockObject)
       {
           //Finder mødeforespørgslen med det højeste Id
           MeetingRequest? lastRequest = meetingRequests.OrderByDescending(m => m.MeetingRequest_Id).FirstOrDefault();

           //Hvis listen er tom, starter Id på 1
           if (lastRequest == null)
           {
               meetingRequest.MeetingRequest_Id = 1;
           }
           else
           {
               meetingRequest.MeetingRequest_Id = lastRequest.MeetingRequest_Id + 1;
           }

           //Tilføjer forespørgslen til listen
           meetingRequests.Add(meetingRequest);

           return meetingRequest;
       }
   }
}

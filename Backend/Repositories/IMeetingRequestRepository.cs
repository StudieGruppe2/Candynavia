using Core; // Giver adgang til modelklasserne

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public interface IMeetingRequestRepository
{
    //Henter en request udfra dens id
    public MeetingRequest? GetById(int meetingRequest_Id);
    
    //Opretter en ny meetingrequest
    public MeetingRequest Add(MeetingRequest meetingRequest);
}
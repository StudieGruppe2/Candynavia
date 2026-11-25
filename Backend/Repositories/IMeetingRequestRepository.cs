using Core; // Giver adgang til modelklasserne

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public interface IMeetingRequestRepository
{
    //Henter en request udfra dens id
    MeetingRequest? GetById(int meetingRequestId);
    
    //Opretter en ny meetingrequest
    MeetingRequest Add(MeetingRequest meetingRequest);
}
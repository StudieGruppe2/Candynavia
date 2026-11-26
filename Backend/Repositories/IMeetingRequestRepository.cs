using Core; // Giver adgang til modelklasserne

namespace Backend.Repositories;

// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder mongoDB og Memory skal have
public interface IMeetingRequestRepository
{
    //Henter en request udfra dens id og returnerer null, hvis forespørgslen ikke findes
    MeetingRequest? GetById(int meetingRequest_Id);
    
    // Gemmer og returnerer den oprettede mødeforespørgsel
    MeetingRequest Add(MeetingRequest meetingRequest);
}
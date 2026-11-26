using Core; // Giver adgang til modelklasserne

namespace Backend.Repositories;

//Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder TimeSlotRepositoryMongoDB og TimeSlotRepositoryMemory skal have
public interface ITimeSlotRepository
{
    //Henter et specifikt mødetidspunkt ud fra dets ID
    TimeSlot? GetTimeSlotById(int timeSlotId);

    //Henter alle ledige mødetidspunkter for en bestemt sælger
    List<TimeSlot> GetAvailableTimeSlots(int salesPersonId);

    //Reserverer et mødetidspunkt, hvis det stadig er ledigt
    //Returnerer true ved succes og ellers false
    bool ReserveTimeSlot(int timeSlotId);
    
}
using Core; // Giver adgang til Annonce-klassen fra Core
namespace Backend.Repositories;

public interface ITimeSlotRepository
// Interfacet fungerer som en kontrakt, der bestemmer hvilke metoder
// Både TimeSlotRepositoryMongoDB og TimeSlotRepositoryMemory skal have
{ 
    // // Henter alle time slots (ledige tider)
    // List<TimeSlot> betyder, at metoden returnerer en liste med time slots (ledige tider)
    List<TimeSlot> GetAll();
    TimeSlot? GetTimeSlotById(int TimeSlot_Id);
    List<TimeSlot> GetAvailableTimeSlots(int SalesPerson_Id);
    List<TimeSlot> GetTimeSlotsBySalesPersonId(int SalesPerson_Id);
    Bool ReserveTimeSlot(int TimeSlot_Id, string status);

    //Mangler DateTime fra core klassen
    

}
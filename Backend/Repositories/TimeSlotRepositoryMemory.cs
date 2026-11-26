using Core;
namespace Backend.Repositories;

public class TimeSlotRepositoryMemory : ITimeSlotRepository
{
    private List<TimeSlot> timeSlots = new List<TimeSlot>();
    private readonly object reservationLock = new object();

    public TimeSlot? GetTimeSlotById(int TimeSlot_Id)
    {
        return timeSlots.FirstOrDefault(t => t.TimeSlot_Id == TimeSlot_Id);
    }

    public List<TimeSlot> GetAvailableTimeSlots(int SalesPerson_Id)
    {
        return timeSlots.Where(t => t.SalesPerson_Id == SalesPerson_Id).ToList();
    } 
    
    /* public List<TimeSlot> GetTimeSlotsBySalesPersonId(int SalesPerson_Id)
    {
        Den gør den samme den ovenover ??
    }

    */
    
    public bool ReserveTimeSlot(int TimeSlot_Id, string status)
    {
        lock (reservationLock) //koden sættes i en lock for at både opdateringen, kontrollen og opslaget gøres mens ingen andre kan gøre det samme på samme tidspunkt
        {
            // Finder TimeSlot ud fra dets ID
            TimeSlot? timeSlot = timeSlots.FirstOrDefault(t => t.TimeSlot_Id == TimeSlot_Id);

            //Hvis TimeSlot ikke findes eller ikke er ledigt returneres false
            if (timeSlot == null || timeSlot.Status != "Available")
            {
                return false;
            }

            //Ændrer TimeSlots status til den nye status
            timeSlot.Status = status;

            //Returnerer true, fordi status blev opdateret
            return true;
        }
    }
}
using Core;
namespace Backend.Repositories;

public class TimeSlotRepositoryMemory : ITimeSlotRepository
{
    private List<TimeSlot> timeSlots = new List<TimeSlot>();


    public TimeSlotRepositoryMemory()
    {
        //Test data med add
    }

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

        // Finder timeSlotId ud fra dens ID
        // FirstOrDefault returnerer null, hvis købsanmodningen ikke findes
        TimeSlot? timeSlot = timeSlots.FirstOrDefault(t => t.TimeSlot_Id == TimeSlot_Id);


        // Hvis timeslottet ikke findes, returneres false
        if (timeSlot == null)
        {
            return false;
        }

        // Ændrer timeslot status til den nye status
        timeSlot.Status = status;

        // Returnerer true, fordi status blev opdateret
        return true;

    }
}
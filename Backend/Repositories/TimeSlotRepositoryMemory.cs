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
        
    }

    public List<TimeSlot> GetAvailableTimeSlots(int SalesPerson_Id)
    {
        
    } 
    
    public <TimeSlot> GetTimeSlotsBySalesPersonId(int SalesPerson_Id)
    {
        
    }

    public bool ReserveTimeSlot(int TimeSlot_Id, string status)
    {
        
    }
}
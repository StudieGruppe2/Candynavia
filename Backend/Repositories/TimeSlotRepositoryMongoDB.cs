using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
namespace Backend.Repositories;

public class TimeSlotRepositoryMongoDB : ITimeSlotRepository
{
    // ---------- FORBINDELSE TIL MONGODB ----------
    //ALT sættes ind her
    
    private readonly// Collectionen i MongoDB, hvor medlemmerne bliver gemt

        public TimeSlotRepositoryMongoDB()
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
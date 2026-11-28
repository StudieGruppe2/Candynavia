using System.Text.Json;
using Core;
namespace Backend.Repositories;

public class TimeSlotRepositoryMemory : ITimeSlotRepository
{
    //Gemmer mødetidspunkterne i hukommelsen i stedet for en database
    private readonly List<TimeSlot> timeSlots;
    
    //låseobjekt som forhindrer konflikter ved samtidige reservationer
    private readonly object reservationLock = new object();

    public TimeSlotRepositoryMemory()
    {
        //Læser testdata fra Data/timeslots.json
        string json = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "timeslots.json"));
        timeSlots = JsonSerializer.Deserialize<List<TimeSlot>>(json) ?? new List<TimeSlot>();
    }

   /*----henter et specifikt mødetidspunkt ud fra dets id----*/
    public TimeSlot? GetTimeSlotById(int TimeSlotId)
    {
        return timeSlots.FirstOrDefault(t => t.TimeSlot_Id == TimeSlotId);
    }

    /*----Henter en sælgers ledige og fremtidige mødetidspunkter----*/
    public List<TimeSlot> GetAvailableTimeSlots(int salesPersonId)
    {
        lock (reservationLock)
        {
            return timeSlots.Where(t =>
                t.SalesPerson_Id == salesPersonId &&
                t.Status == "Available" &&
                t.StartTime > DateTime.UtcNow).ToList();
        }
    }
    
    /*----Reserverer et mødetidspunkt, hvis det stadig er ledigt----*/
    public bool ReserveTimeSlot(int timeSlotId)
    {
        // Sikrer, at opslag, kontrol og reservation udføres uden at en anden forespørgsel kan reservere tidspunktet samtidig
        lock (reservationLock)
        {
            //Finder mødetidspunktet ud fra dets ID
            TimeSlot? timeSlot = timeSlots.FirstOrDefault(t => t.TimeSlot_Id == timeSlotId);

            //Returnerer false, hvis tidspunktet ikke findes, ikke er ledigt eller ligger i fortiden
            if (timeSlot == null || timeSlot.Status != "Available" || timeSlot.StartTime <= DateTime.UtcNow)
            {
                return false;
            }

            //Ændrer tidspunktets status til Reserved
            timeSlot.Status = "Reserved";

            //Returnerer true, fordi reservationen lykkedes
            return true;
        }
    }
}
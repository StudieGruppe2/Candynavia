using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
namespace Backend.Repositories;

public class TimeSlotRepositoryMongoDB : ITimeSlotRepository
{
    //Forbindelsen til samlingen "TimeSlots" i mongoDB
    private readonly IMongoCollection<TimeSlot> timeSlotCollection;

    /*Forbindelse til MongoDB*/
    //Constructoren køres når repositoriet bliver oprettet
    public TimeSlotRepositoryMongoDB(IConfiguration configuration)
    {
        // Fortæller MongoDB, at TimeSlot_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(TimeSlot)))
        {
            BsonClassMap<TimeSlot> map = new BsonClassMap<TimeSlot>();
            map.AutoMap();
            map.MapIdMember(t => t.TimeSlot_Id);            
            BsonClassMap.RegisterClassMap(map);
        }

        // Henter connection stringen fra User Secrets (ligger på hver persons egen computer)
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        timeSlotCollection = database.GetCollection<TimeSlot>("TimeSlots");
    }
    
  
    /*Henter et specifikt mødetidspunkt udfra dets id*/
    public TimeSlot? GetTimeSlotById(int timeSlotId)
    {
        return timeSlotCollection.Find(t => t.TimeSlot_Id == timeSlotId).FirstOrDefault();
    }

   
    /*Henter en sælgers ledige og fremtidige mødetidspunkter*/
    public List<TimeSlot> GetAvailableTimeSlots(int salesPerson_Id)
    {
            return timeSlotCollection.Find(t =>
                t.SalesPerson_Id == salesPerson_Id &&
                t.Status == "Available" &&
                t.StartTime > DateTime.UtcNow).ToList();
    } 

    /*Reserverer et mødetidspunkt, hvis det stadig er ledigt*/
    public bool ReserveTimeSlot(int timeSlotId)
    {
        //Finder tidspunktet, hvis alle tre betingelser er opfyldt: tid i fremtiden, tidspunkt tilhøre valgt sælger og har status available
        FilterDefinition<TimeSlot> filter = Builders<TimeSlot>.Filter.And(
                Builders<TimeSlot>.Filter.Eq(t => t.TimeSlot_Id, timeSlotId),
                Builders<TimeSlot>.Filter.Eq(t => t.Status, "Available"),
                Builders<TimeSlot>.Filter.Gt(t => t.StartTime, DateTime.UtcNow)
            );

        //en definition som siger at tidspunktets status skal ændres til Reserved
        UpdateDefinition<TimeSlot> update = Builders<TimeSlot>.Update.Set(t => t.Status, "Reserved");

        //Opdaterer tidspunktet i MongoDB
        UpdateResult result = timeSlotCollection.UpdateOne(filter, update);

        //ModifiedCount angiver antallet af ændrede dokumenter i MongoDB.
        //Hvis tidspunktet allerede er reserveret, opfyldes filteret ikke og ModifiedCount bliver 0. så forhindres dobbeltbooking af samme TimeSlot
        return result.ModifiedCount == 1; 
                                          
    }
}
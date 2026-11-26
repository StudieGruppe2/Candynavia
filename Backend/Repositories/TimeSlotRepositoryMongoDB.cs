using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
namespace Backend.Repositories;

public class TimeSlotRepositoryMongoDB : ITimeSlotRepository
{
    // ---------- FORBINDELSE TIL MONGODB ----------
     //Forbindelsen til samlingen "SalesPersons" i mongoDB
    //Forbindelsen til samlingen "TimeSlots" i mongoDB
    private readonly IMongoCollection<TimeSlot> timeSlotCollection;

    /*----Forbindelse til MongoDB----*/
    //Constructoren køres når repositoriet bliver oprettet
    public TimeSlotRepositoryMongoDB(IConfiguration configuration)
    {
        // Fortæller MongoDB, at TimeSlot_Id er nøglen (_id)
        if (!BsonClassMap.IsClassMapRegistered(typeof(TimeSlot)))
        {
            BsonClassMap<TimeSlot> map = new BsonClassMap<TimeSlot>();
            map.AutoMap();
            map.MapIdMember(typeof(TimeSlot).GetProperty("TimeSlot_Id")!);
            BsonClassMap.RegisterClassMap(map);
        }

        // Henter connection stringen fra User Secrets (ligger på hver persons egen computer)
        string connectionString = configuration["MongoDB:ConnectionString"]!;
        MongoClient client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase("CandynaviaDB");

        timeSlotCollection = database.GetCollection<TimeSlot>("TimeSlots");
    }
    
  
    
    public TimeSlot? GetTimeSlotById(int TimeSlot_Id)
    {
        return timeSlotCollection.Find(t => t.TimeSlot_Id == TimeSlot_Id).FirstOrDefault();
    }

    public List<TimeSlot> GetAvailableTimeSlots(int SalesPerson_Id)
    {
         FilterDefinition<TimeSlot> filter = 
             Builders<TimeSlot>.Filter.Eq("SalesPerson_Id", SalesPerson_Id)
            & Builders<TimeSlot>.Filter.Eq(t => t.Status, "Available");
         
         return timeSlotCollection.Find(filter).ToList();
    } 


    /*
    public <TimeSlot> GetTimeSlotsBySalesPersonId(int SalesPerson_Id)
    {
        
    }
    */

    public bool ReserveTimeSlot(int TimeSlot_Id, string status)
    {
        // Finder timeSlot ud fra dens ID
        FilterDefinition<TimeSlot> filter = Builders<TimeSlot>.Filter.Eq(p => p.TimeSlot_Id, TimeSlot_Id);

        // Angiver, at det kun er timeSlots Status, der skal ændres
        UpdateDefinition<TimeSlot> update = Builders<TimeSlot>.Update.Set(p => p.Status, status);

        // Opdaterer timeSlot i MongoDB
        UpdateResult result = timeSlotCollection.UpdateOne(filter, update);

        // Returnerer true, hvis timeSlot blev fundet
        // Ellers returneres false
        return result.MatchedCount == 1;
    }
}
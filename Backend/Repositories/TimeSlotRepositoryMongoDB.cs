using Core;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
namespace Backend.Repositories;

public class TimeSlotRepositoryMongoDB : ITimeSlotRepository
{
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
        //Finder tidspunktet ud fra ID, hvis det stadig er ledigt
        FilterDefinition<TimeSlot> filter = Builders<TimeSlot>.Filter.And( //And siger at begge betingelser skal være opfyldt
            Builders<TimeSlot>.Filter.Eq(p => p.TimeSlot_Id, TimeSlot_Id),
            Builders<TimeSlot>.Filter.Eq(p => p.Status, "Available") //Status skal være available 
        );

        //Angiver at det kun er TimeSlots status der skal ændres
        UpdateDefinition<TimeSlot> update = Builders<TimeSlot>.Update.Set(p => p.Status, status);

        //Opdaterer TimeSlot i MongoDB, og tager filter og update som parametre
        UpdateResult result = timeSlotCollection.UpdateOne(filter, update);

        //Returnerer true hvis status blev ændret
        return result.ModifiedCount == 1; //ModifidCount = property i MongoDB-driveren, som fortæller hvor mange dokumenter der faktisk blev opdateret
    }
}
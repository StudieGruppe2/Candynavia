namespace Core;

public class TimeSlot
{
    // Unikt ID for mødetidspunktet
    public int TimeSlot_Id { get; set; } = Random.Shared.Next();
   
    //Mødetidspunktets start og slut gemmes i UTC - den fælles international tidsstandard
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }

    //Mulige statusser: Available, Reserved og Blocked.
    //Available: Tidspunktet er ledigt.
    //Reserved: Tidspunktet er reserveret til en mødeforespørgsel.
    //Blocked: Sælgeren har gjort tidspunktet utilgængeligt.
    public string Status { get; set; } = "";

    //Reference til den sælger, som tidspunktet tilhører
    public int SalesPerson_Id { get; set; }
}
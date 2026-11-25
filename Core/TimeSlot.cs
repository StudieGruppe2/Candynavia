namespace Core;

public class TimeSlot
{
    public int TimeSlot_Id {get; set;} = Random.Shared.Next();

    public DateTime StartTime {get; set;}

    public DateTime EndTime {get; set;}

    public string Status {get; set;} = "";

    public int SalesPerson_Id {get; set;}

}
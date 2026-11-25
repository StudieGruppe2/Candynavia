namespace Core;

public class MeetingRequest
{
    public int MeetingRequest_Id {get; set;} = Random.Shared.Next();

    public string Description {get; set;} = "";

    public string Status {get; set;} = "";

    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public bool Consent {get; set;} = false;

    public int TimeSlot_Id {get; set;}
    
    public int SalesPerson_Id {get; set;}


    public Customer Customer {get; set;} = new();

}
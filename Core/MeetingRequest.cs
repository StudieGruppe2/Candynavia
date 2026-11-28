namespace Core;
//Klassen repræsenterer data om mødeforespørgslen. Den har ikke selv ansvar for at godkende, afvise eller reservere et tidspunkt
//En del af mødeforespørgslen er også kundens info, dette er embedded.

public class MeetingRequest
{
    public string? MeetingRequest_Id { get; set; }

    public string Description { get; set; } = "";
    
    public string Status { get; set; } = "";
    //Mulige statusser: Pending, Accepted, Rejected og Cancelled.
    //Cancelled anvendes, hvis mødet aflyses, så forespørgslen bevares i DB.

    // Controlleren sætter tidspunktet for oprettelsen af forespørgslen.
    public DateTime CreatedAt { get; set; }

    public bool Consent { get; set; } = false;

    //Reference til det valgte mødetidspunkt.
    public int TimeSlot_Id { get; set; }

    //Reference til den tilknyttede sælger.
    public int SalesPerson_Id { get; set; }

    //Customer gemmes som et embedded objekt i MeetingRequest.
    //Initialiseres med et nyt Customer-objekt.
    public Customer Customer { get; set; } = new();
}
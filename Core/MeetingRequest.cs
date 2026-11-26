namespace Core;

public class MeetingRequest
{
    // TODO: Sikre unik ID-generering ved samtidige forespørgsler.
    public int MeetingRequest_Id {get; set;} //Id tildeles i repositoriet ved oprettelse af forespørgslen
    
    public string Description {get; set;} = "";

    // TODO: Sikre gyldige statusændringer, når sælgerfunktionaliteten implementeres.
    public string Status {get; set;} = ""; //mulige statusser: Pending, Accepted, Rejected og cancelled
    //Cancelled bruges til hvis mødet aflyses af en af parterne - forespørgslen bevares på denne måde stadig i DB

    public DateTime CreatedAt {get; set;} //controller sørger for at sætte tidspunktet til hvornår forespørgslen oprettes

    public bool Consent {get; set;} = false;

    public int TimeSlot_Id { get; set; } //Reference til det valgte mødetidspunkt i TimeSlots-collectionen

    public int SalesPerson_Id { get; set; } //Reference til den sælger, som mødeforespørgslen er tilknyttet
    
    
    // Customer gemmes som et embedded objekt i MeetingRequest.
    // Initialiseres med et nyt Customer-objekt
    public Customer Customer {get; set;} = new();

}
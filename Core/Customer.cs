namespace Core;

public class Customer
{
//Customer har ikke et selvstændigt ID, da kunden gemmes som et embedded objekt i MeetingRequest.
//Kunden skal ikke identificeres selvstændigt, da kundeoplysningerne altid anvendes i forbindelse med en mødeforespørgsel.
//Dette forenkler datamodellen og reducerer behovet for databaseopslag  
   
    public string Name {get; set;} = "";

    public string Email {get; set;} = "";

    public string Phone {get; set;} = "";

    public string Company {get; set;} = "";

    public string Position {get; set;} = ""; //jobstilling i virksomheden

}
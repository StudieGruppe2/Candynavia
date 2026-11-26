namespace Core;

public class SalesPerson
{
    public int SalesPerson_Id {get; set;} // Unikt ID, der identificerer sælgeren i databasen - skal ikke oprettes med random da det er en stamdata

    public string Name {get; set;} = "";

    public string Email {get; set;} = "";
    
    //Telefonnummer gemmes som string så det kan indeholde landekode
    public string Phone {get; set;} = "";

    public string Position {get; set;} = "";

}
namespace Core;

//Klassen repræsenterer en sælger i systemet og de oplysninger, der bruges til at identificere og kontakte
public class SalesPerson
{
    public int SalesPerson_Id {get; set;} 

    public string Name {get; set;} = "";

    public string Email {get; set;} = "";
    
    //Telefonnummer gemmes som string så det kan indeholde landekode
    public string Phone {get; set;} = "";

    public string Position {get; set;} = "";

}
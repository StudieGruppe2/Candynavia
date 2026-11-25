namespace Core;

public class SalesPerson
{
    public int SalesPerson_Id {get; set;} = Random.Shared.Next();

    public string Name {get; set;} = "";

    public string Email {get; set;} = "";

    public string Phone {get; set;} = "";


    public string Position {get; set;} = "";

}
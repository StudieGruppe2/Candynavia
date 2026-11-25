namespace Core;

public class PostalCodeArea
{ 
    public int PostalCodeArea_Id { get; set; } = Random.Shared.Next();
    public string PostalCode { get; set; } = "";
    public string CityName { get; set; } = "";
    public int SalesPerson_Id { get; set; }    

}
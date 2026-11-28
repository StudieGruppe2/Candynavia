namespace Core;

//Klassen repræsenterer et postnummerområde og dets tilknytning til den geografisk ansvarlige sælger
public class PostalCodeArea
{ 
    public int PostalCodeArea_Id { get; set; } = Random.Shared.Next(); //den her kunne blive relevant når administrators funktioner skal laves ift fordeling af salgsområder??? 
    public string PostalCode { get; set; } = ""; //Postnummeret gemmes som string, da det ikke anvendes til beregninger
    
    public string CityName { get; set; } = ""; //KONTROLLERE OM DENNE BRUGES NÅR VI ER HELT FÆRDIGE??
    public int? SalesPerson_Id { get; set; } //Nullable reference da et postnummerområde midlertidigt kan være uden en ansvarlig sælger
}
namespace Core;

public class PostalCodeArea
{ 
    public int PostalCodeArea_Id { get; set; } = Random.Shared.Next(); //den her kunne blive relevant når administrators funktioner skal laves ift fordeling af salgsområder??? 
    public string PostalCode { get; set; } = ""; //Postnummeret gemmes som string, da det ikke anvendes til beregninger
    
    // TODO: Kontrollér, om CityName er nødvendig i den endelige løsning.
    public string CityName { get; set; } = ""; //KONTROLLERE OM DENNE BRUGES NÅR VI ER HELT FÆRDIGE??
    public int SalesPerson_Id { get; set; } //Reference til den sælger, der er ansvarlig for postnummerområdet
    
}
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public enum MoedeStatus
{
    Afventer,
    Godkendt,
    Afvist
}

public class MoedeAnmodning
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Titel skal udfyldes")]
    [StringLength(100)]
    public string Titel { get; set; } = string.Empty;

    [StringLength(500)]
    public string Beskrivelse { get; set; } = string.Empty;

    [Required(ErrorMessage = "Deltager skal udfyldes")]
    public string Deltager { get; set; } = string.Empty;

    public DateTime Dato { get; set; } = DateTime.Today.AddDays(1);

    public MoedeStatus Status { get; set; } = MoedeStatus.Afventer;
}

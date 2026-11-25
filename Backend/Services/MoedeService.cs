using Core.Models;

namespace Backend.Services;

// Gemmer data i hukommelsen. Skift senere ud med en database.
public class MoedeService
{
    private readonly List<MoedeAnmodning> _moeder = new();
    private int _naesteId = 1;

    public MoedeService()
    {
        Opret(new MoedeAnmodning
        {
            Titel = "Kickoff-møde",
            Beskrivelse = "Første møde i projektet",
            Deltager = "Gruppen",
            Dato = DateTime.Today.AddDays(2)
        });
    }

    public List<MoedeAnmodning> HentAlle() => _moeder.OrderBy(m => m.Dato).ToList();

    public MoedeAnmodning? Hent(int id) => _moeder.FirstOrDefault(m => m.Id == id);

    public MoedeAnmodning Opret(MoedeAnmodning moede)
    {
        moede.Id = _naesteId++;
        moede.Status = MoedeStatus.Afventer;
        _moeder.Add(moede);
        return moede;
    }

    public bool SaetStatus(int id, MoedeStatus status)
    {
        var moede = Hent(id);
        if (moede is null) return false;
        moede.Status = status;
        return true;
    }

    public bool Slet(int id) => _moeder.RemoveAll(m => m.Id == id) > 0;
}

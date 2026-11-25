using Backend.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/moeder")]
public class MoederController : ControllerBase
{
    private readonly MoedeService _service;

    public MoederController(MoedeService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<MoedeAnmodning>> HentAlle() => _service.HentAlle();

    [HttpGet("{id}")]
    public ActionResult<MoedeAnmodning> Hent(int id)
    {
        var moede = _service.Hent(id);
        return moede is null ? NotFound() : moede;
    }

    [HttpPost]
    public ActionResult<MoedeAnmodning> Opret(MoedeAnmodning moede)
    {
        var oprettet = _service.Opret(moede);
        return CreatedAtAction(nameof(Hent), new { id = oprettet.Id }, oprettet);
    }

    [HttpPut("{id}/status")]
    public IActionResult SaetStatus(int id, [FromBody] MoedeStatus status)
    {
        return _service.SaetStatus(id, status) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Slet(int id)
    {
        return _service.Slet(id) ? NoContent() : NotFound();
    }
}

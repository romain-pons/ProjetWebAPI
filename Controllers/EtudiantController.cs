[ApiController]
[Route("[controller]")]
public class EtudiantController : ControllerBase
{
    private readonly EtudiantService _service;

    public EtudiantController(EtudiantService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var etudiant = await _service.GetById(id);
        if (etudiant == null) return NotFound();
        return Ok(etudiant);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Etudiant etudiant)
    {
        await _service.Add(etudiant);
        return CreatedAtAction(nameof(Get), new { id = etudiant.Id }, etudiant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Etudiant etudiant)
    {
        if (id != etudiant.Id) return BadRequest();
        await _service.Update(etudiant);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

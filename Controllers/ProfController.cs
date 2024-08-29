[ApiController]
[Route("[controller]")]
public class ProfController : ControllerBase
{
    private readonly ProfService _service;

    public ProfController(ProfService service)
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
        var prof = await _service.GetById(id);
        if (prof == null) return NotFound();
        return Ok(prof);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Prof prof)
    {
        await _service.Add(prof);
        return CreatedAtAction(nameof(Get), new { id = prof.Id }, prof);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Prof prof)
    {
        if (id != prof.Id) return BadRequest();
        await _service.Update(prof);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

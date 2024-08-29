[ApiController]
[Route("[controller]")]
public class CourController : ControllerBase
{
    private readonly CoursService _service;

    public CourController(CoursService service)
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
        var cours = await _service.GetById(id);
        if (cours == null) return NotFound();
        return Ok(cours);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Cours cours)
    {
        await _service.Add(cours);
        return CreatedAtAction(nameof(Get), new { id = cours.Id }, cours);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Cours cours)
    {
        if (id != cours.Id) return BadRequest();
        await _service.Update(cours);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}

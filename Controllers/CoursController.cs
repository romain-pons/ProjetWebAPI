using Microsoft.AspNetCore.Mvc;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursController : ControllerBase
    {
        private readonly CoursService _service;

        public CoursController(CoursService service)
        {
            _service = service;
        }

        /// <summary>
        /// Recuperer tous les cours.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les cours disponibles dans le systeme.
        /// </remarks>
        /// <response code="200">Retourner la liste des cours.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cours = await _service.GetAll();
            return Ok(cours);
        }

        /// <summary>
        /// Recuperer un cours specifique par ID.
        /// </summary>
        /// <param name="id">L'ID du cours a recuperer.</param>
        /// <remarks>
        /// Cet endpoint retourne un cours unique base sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourner le cours demande.</response>
        /// <response code="404">Si le cours n'est pas trouve.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cours = await _service.GetById(id);
            if (cours == null) return NotFound();
            return Ok(cours);
        }

        /// <summary>
        /// Creer un nouveau cours.
        /// </summary>
        /// <param name="cours">L'objet cours a creer.</param>
        /// <remarks>
        /// Cet endpoint cree un nouveau cours dans le systeme.
        /// </remarks>
        /// <response code="201">Le cours a ete cree avec succes.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cours cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");

            await _service.Add(cours);
            return CreatedAtAction(nameof(Get), new { id = cours.Id }, cours);
        }

        /// <summary>
        /// Mettre a jour un cours existant.
        /// </summary>
        /// <param name="id">L'ID du cours a mettre a jour.</param>
        /// <param name="cours">L'objet cours mis a jour.</param>
        /// <remarks>
        /// Cet endpoint met a jour un cours existant dans le systeme.
        /// </remarks>
        /// <response code="204">Le cours a ete mis a jour avec succes.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas a l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si le cours a mettre a jour n'est pas trouve.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cours cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");
            if (id != cours.Id) return BadRequest("Incoh�rence d'ID.");

            var coursExistant = await _service.GetById(id);
            if (coursExistant == null) return NotFound();

            await _service.Update(cours);
            return NoContent();
        }

        /// <summary>
        /// Supprimer un cours par ID.
        /// </summary>
        /// <param name="id">L'ID du cours a supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un cours du syst�me base sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le cours a ete supprime avec succes.</response>
        /// <response code="404">Si le cours a supprimer n'est pas trouve.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var coursExistant = await _service.GetById(id);
            if (coursExistant == null) return NotFound();

            await _service.Delete(id);
            return NoContent();
        }
    }
}

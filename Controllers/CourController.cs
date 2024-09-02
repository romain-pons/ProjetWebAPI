using Microsoft.AspNetCore.Mvc;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourController : ControllerBase
    {
        private readonly CourService _service;

        public CourController(CourService service)
        {
            _service = service;
        }

        /// <summary>
        /// Récupère tous les cours.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les cours disponibles dans le système.
        /// </remarks>
        /// <response code="200">Retourne la liste des cours.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cours = await _service.GetAll();
            return Ok(cours);
        }

        /// <summary>
        /// Récupère un cours spécifique par ID.
        /// </summary>
        /// <param name="id">L'ID du cours à récupérer.</param>
        /// <remarks>
        /// Cet endpoint retourne un cours unique basé sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourne le cours demandé.</response>
        /// <response code="404">Si le cours n'est pas trouvé.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cours = await _service.GetById(id);
            if (cours == null) return NotFound();
            return Ok(cours);
        }

        /// <summary>
        /// Crée un nouveau cours.
        /// </summary>
        /// <param name="cours">L'objet cours à créer.</param>
        /// <remarks>
        /// Cet endpoint crée un nouveau cours dans le système.
        /// </remarks>
        /// <response code="201">Le cours a été créé avec succès.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cours cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");

            await _service.Add(cours);
            return CreatedAtAction(nameof(Get), new { id = cours.Id }, cours);
        }

        /// <summary>
        /// Met à jour un cours existant.
        /// </summary>
        /// <param name="id">L'ID du cours à mettre à jour.</param>
        /// <param name="cours">L'objet cours mis à jour.</param>
        /// <remarks>
        /// Cet endpoint met à jour un cours existant dans le système.
        /// </remarks>
        /// <response code="204">Le cours a été mis à jour avec succès.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas à l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si le cours à mettre à jour n'est pas trouvé.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cours cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");
            if (id != cours.Id) return BadRequest("Incohérence d'ID.");

            var coursExistant = await _service.GetById(id);
            if (coursExistant == null) return NotFound();

            await _service.Update(cours);
            return NoContent();
        }

        /// <summary>
        /// Supprime un cours par ID.
        /// </summary>
        /// <param name="id">L'ID du cours à supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un cours du système basé sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le cours a été supprimé avec succès.</response>
        /// <response code="404">Si le cours à supprimer n'est pas trouvé.</response>
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

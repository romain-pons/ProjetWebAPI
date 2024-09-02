using Microsoft.AspNetCore.Mvc;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfController : ControllerBase
    {
        private readonly ProfService _service;

        public ProfController(ProfService service)
        {
            _service = service;
        }

        /// <summary>
        /// Récupère tous les professeurs.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les professeurs disponibles dans le système.
        /// </remarks>
        /// <response code="200">Retourne la liste des professeurs.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var professeurs = await _service.GetAll();
            return Ok(professeurs);
        }

        /// <summary>
        /// Récupère un professeur spécifique par ID.
        /// </summary>
        /// <param name="id">L'ID du professeur à récupérer.</param>
        /// <remarks>
        /// Cet endpoint retourne un professeur unique basé sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourne le professeur demandé.</response>
        /// <response code="404">Si le professeur n'est pas trouvé.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var professeur = await _service.GetById(id);
            if (professeur == null) return NotFound();
            return Ok(professeur);
        }

        /// <summary>
        /// Crée un nouveau professeur.
        /// </summary>
        /// <param name="prof">L'objet professeur à créer.</param>
        /// <remarks>
        /// Cet endpoint crée un nouveau professeur dans le système.
        /// </remarks>
        /// <response code="201">Le professeur a été créé avec succès.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Profs prof)
        {
            if (prof == null) return BadRequest("L'objet professeur est nul.");

            await _service.Add(prof);
            return CreatedAtAction(nameof(Get), new { id = prof.Id }, prof);
        }

        /// <summary>
        /// Met à jour un professeur existant.
        /// </summary>
        /// <param name="id">L'ID du professeur à mettre à jour.</param>
        /// <param name="prof">L'objet professeur mis à jour.</param>
        /// <remarks>
        /// Cet endpoint met à jour un professeur existant dans le système.
        /// </remarks>
        /// <response code="204">Le professeur a été mis à jour avec succès.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas à l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si le professeur à mettre à jour n'est pas trouvé.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Profs prof)
        {
            if (prof == null) return BadRequest("L'objet professeur est nul.");
            if (id != prof.Id) return BadRequest("Incohérence d'ID.");

            var professeurExistant = await _service.GetById(id);
            if (professeurExistant == null) return NotFound();

            await _service.Update(prof);
            return NoContent();
        }

        /// <summary>
        /// Supprime un professeur par ID.
        /// </summary>
        /// <param name="id">L'ID du professeur à supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un professeur du système basé sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le professeur a été supprimé avec succès.</response>
        /// <response code="404">Si le professeur à supprimer n'est pas trouvé.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var professeurExistant = await _service.GetById(id);
            if (professeurExistant == null) return NotFound();

            await _service.Delete(id);
            return NoContent();
        }
    }
}

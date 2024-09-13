using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfsController : ControllerBase
    {
        private readonly ProfsService _service;

        public ProfsController(ProfsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Récupérer tous les professeurs.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les professeurs disponibles dans le système.
        /// </remarks>
        /// <response code="200">Retourner la liste des professeurs.</response>
        [HttpGet]
        [Authorize(Roles = "Professor,Student")]
        public async Task<IActionResult> Get()
        {
            var professeurs = await _service.GetAll();
            return Ok(professeurs);
        }

        /// <summary>
        /// Récuperer un professeur spécifique par ID.
        /// </summary>
        /// <param name="id">L'ID du professeur a récupérer.</param>
        /// <remarks>
        /// Cet endpoint retourne un professeur unique base sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourner le professeur demandé.</response>
        /// <response code="404">Si le professeur n'est pas trouvé.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Professor,Student")]
        public async Task<IActionResult> Get(int id)
        {
            var professeur = await _service.GetById(id);
            if (professeur == null) return NotFound();
            return Ok(professeur);
        }

        /// <summary>
        /// Creer un nouveau professeur.
        /// </summary>
        /// <param name="prof">L'objet professeur a créer.</param>
        /// <remarks>
        /// Cet endpoint crée un nouveau professeur dans le système.
        /// </remarks>
        /// <response code="201">Le professeur a été crée avec succes.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Post([FromBody] Profs prof)
        {
            if (prof == null) return BadRequest("L'objet professeur est nul.");

            await _service.Add(prof);
            return CreatedAtAction(nameof(Get), new { id = prof.Id }, prof);
        }

        /// <summary>
        /// Mettre a jour un professeur existant.
        /// </summary>
        /// <param name="id">L'ID du professeur a mettre a jour.</param>
        /// <param name="prof">L'objet professeur mis a jour.</param>
        /// <remarks>
        /// Cet endpoint met a jour un professeur existant dans le système.
        /// </remarks>
        /// <response code="204">Le professeur a été mis a jour avec succes.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas a l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si le professeur a mettre a jour n'est pas trouvé.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Put(int id, [FromBody] ProfsUpdate prof)
        {
            if (prof == null) return BadRequest("L'objet professeur est nul.");

            // Récupérer l'entité existante
            var professeurExistant = await _service.GetById(id);
            if (professeurExistant == null) return NotFound();

            // Mettre à jour manuellement les propriétés
            professeurExistant.Nom = prof.Nom;
            professeurExistant.Prenom = prof.Prenom;
            professeurExistant.Matiere = prof.Matiere;

            try
            {
                await _service.Update(professeurExistant);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erreur lors de la mise à jour.");
            }

            return NoContent();
        }


        /// <summary>
        /// Supprimer un professeur par ID.
        /// </summary>
        /// <param name="id">L'ID du professeur a supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un professeur du système base sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le professeur a été supprimé avec succes.</response>
        /// <response code="404">Si le professeur a supprimer n'est pas trouvé.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Delete(int id)
        {
            var professeurExistant = await _service.GetById(id);
            if (professeurExistant == null) return NotFound();

            await _service.Delete(id);
            return NoContent();
        }
    }
}


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
        /// Recuperer tous les professeurs.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les professeurs disponibles dans le systeme.
        /// </remarks>
        /// <response code="200">Retourner la liste des professeurs.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var professeurs = await _service.GetAll();
            return Ok(professeurs);
        }

        /// <summary>
        /// Recuperer un professeur specifique par ID.
        /// </summary>
        /// <param name="id">L'ID du professeur a recuperer.</param>
        /// <remarks>
        /// Cet endpoint retourne un professeur unique base sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourner le professeur demande.</response>
        /// <response code="404">Si le professeur n'est pas trouve.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var professeur = await _service.GetById(id);
            if (professeur == null) return NotFound();
            return Ok(professeur);
        }

        /// <summary>
        /// Creer un nouveau professeur.
        /// </summary>
        /// <param name="prof">L'objet professeur a creer.</param>
        /// <remarks>
        /// Cet endpoint cree un nouveau professeur dans le systeme.
        /// </remarks>
        /// <response code="201">Le professeur a ete cree avec succes.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
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
        /// Cet endpoint met a jour un professeur existant dans le systeme.
        /// </remarks>
        /// <response code="204">Le professeur a ete mis a jour avec succes.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas a l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si le professeur a mettre a jour n'est pas trouve.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Profs prof)
        {
            if (prof == null) return BadRequest("L'objet professeur est nul.");
            if (id != prof.Id) return BadRequest("Incohérence d'ID.");

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
        /// Cet endpoint supprime un professeur du systeme base sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le professeur a ete supprime avec succes.</response>
        /// <response code="404">Si le professeur a supprimer n'est pas trouve.</response>
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

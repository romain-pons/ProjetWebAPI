using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Data;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursController : ControllerBase
    {
        private readonly CoursService _service;
        private readonly AppDbContext _context;

        public CoursController(CoursService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Récupérer tous les cours.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les cours disponibles dans le système.
        /// </remarks>
        /// <response code="200">Retourne la liste des cours.</response>
        [HttpGet]
        [Authorize(Roles = "Professor,Student")]
        public async Task<IActionResult> Get()
        {
            var cours = await _service.GetAll();
            return Ok(cours);
        }

        /// <summary>
        /// Récupérer un cours spécifique par ID.
        /// </summary>
        /// <param name="id">L'ID du cours à récupérer.</param>
        /// <remarks>
        /// Cet endpoint retourne un cours unique basé sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourne le cours demandé.</response>
        /// <response code="404">Si le cours n'est pas trouvé.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Professor,Student")]
        public async Task<IActionResult> Get(int id)
        {
            var cours = await _service.GetById(id);
            if (cours == null) return NotFound();
            return Ok(cours);
        }

        /// <summary>
        /// Créer un nouveau cours.
        /// </summary>
        /// <param name="cours">L'objet cours à créer.</param>
        /// <remarks>
        /// Cet endpoint crée un nouveau cours dans le système.
        /// </remarks>
        /// <response code="201">Le cours a été créé avec succès.</response>
        /// <response code="400">Si la demande est invalide ou si le professeur n'existe pas.</response>
        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Post([FromBody] Cours cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");

            // Vérifie si le professeur existe
            var prof = await _context.Profs.FindAsync(cours.ProfId);
            if (prof == null) return BadRequest("Le professeur spécifié n'existe pas.");

            await _service.Add(cours);
            return CreatedAtAction(nameof(Get), new { id = cours.Id }, cours);
        }

        /// <summary>
        /// Mettre à jour un cours existant.
        /// </summary>
        /// <param name="id">L'ID du cours à mettre à jour.</param>
        /// <param name="cours">L'objet cours mis à jour.</param>
        /// <remarks>
        /// Cet endpoint met à jour un cours existant dans le système.
        /// </remarks>
        /// <response code="204">Le cours a été mis à jour avec succès.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas à l'ID dans le corps de la demande ou si le professeur n'existe pas.</response>
        /// <response code="404">Si le cours à mettre à jour n'est pas trouvé.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Put(int id, [FromBody] CoursUpdate cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");

            // Vérifie si le professeur existe
            var prof = await _context.Profs.FindAsync(cours.ProfId);
            if (prof == null) return BadRequest("Le professeur spécifié n'existe pas.");

            // Récupérer l'entité existante
            var coursExistant = await _service.GetById(id);
            if (coursExistant == null) return NotFound();

            // Mettre à jour manuellement les propriétés de l'entité existante
            coursExistant.Titre = cours.Titre;
            coursExistant.Description = cours.Description;
            coursExistant.ProfId = cours.ProfId;

            try
            {
                // Mise à jour de l'entité dans le contexte
                await _service.Update(coursExistant);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erreur lors de la mise à jour.");
            }

            return NoContent();
        }

        /// <summary>
        /// Supprimer un cours par ID.
        /// </summary>
        /// <param name="id">L'ID du cours à supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un cours du système basé sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le cours a été supprimé avec succès.</response>
        /// <response code="404">Si le cours à supprimer n'est pas trouvé.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Delete(int id)
        {
            var coursExistant = await _service.GetById(id);
            if (coursExistant == null) return NotFound();

            await _service.Delete(id);
            return NoContent();
        }
    }
}

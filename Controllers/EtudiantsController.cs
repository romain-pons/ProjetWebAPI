using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EtudiantsController : ControllerBase
    {
        private readonly EtudiantsService _service;

        public EtudiantsController(EtudiantsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Récupérer tous les etudiants.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les étudiants disponibles dans le système.
        /// </remarks>
        /// <response code="200">Retourner la liste des étudiants.</response>
        [HttpGet]
        [Authorize(Roles = "Professor,Student")]
        public async Task<IActionResult> Get()
        {
            var etudiants = await _service.GetAll();
            return Ok(etudiants);
        }

        /// <summary>
        /// Récupérer un étudiant spécifique par ID.
        /// </summary>
        /// <param name="id">L'ID de l'étudiant a récupérer.</param>
        /// <remarks>
        /// Cet endpoint retourne un étudiant unique base sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourner l'étudiant demandé.</response>
        /// <response code="404">Si l'étudiant n'est pas trouvé.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Professor,Student")]
        public async Task<IActionResult> Get(int id)
        {
            var etudiant = await _service.GetById(id);
            if (etudiant == null) return NotFound();
            return Ok(etudiant);
        }

        /// <summary>
        /// Créer un nouvel étudiant.
        /// </summary>
        /// <param name="etudiant">L'objet étudiant a créer.</param>
        /// <remarks>
        /// Cet endpoint crée un nouvel étudiant dans le système.
        /// </remarks>
        /// <response code="201">L'etudiant a été créé avec succes.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Post([FromBody] Etudiants etudiant)
        {
            if (etudiant == null) return BadRequest("L'objet étudiant est nul.");

            await _service.Add(etudiant);
            return CreatedAtAction(nameof(Get), new { id = etudiant.Id }, etudiant);
        }

        /// <summary>
        /// Mettre à jour un étudiant existant.
        /// </summary>
        /// <param name="id">L'ID de l'étudiant a mettre à jour.</param>
        /// <param name="etudiant">L'objet étudiant mis à jour.</param>
        /// <remarks>
        /// Cet endpoint met à jour un étudiant existant dans le système.
        /// </remarks>
        /// <response code="204">L'étudiant a été mis à jour avec succes.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas à l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si l'étudiant a mettre à jour n'est pas trouvé.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Put(int id, [FromBody] EtudiantsUpdate etudiant)
        {
            if (etudiant == null) return BadRequest("L'objet étudiant est nul.");

            var etudiantExistant = await _service.GetById(id);
            if (etudiantExistant == null) return NotFound();

            // Mettre à jour uniquement les propriétés nécessaires
            etudiantExistant.Nom = etudiant.Nom;
            etudiantExistant.Prenom = etudiant.Prenom;
            etudiantExistant.Age = etudiant.Age;

            try
            {
                await _service.Update(etudiantExistant);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Erreur lors de la mise à jour.");
            }

            return NoContent();
        }


        /// <summary>
        /// Supprimer un étudiant par ID.
        /// </summary>
        /// <param name="id">L'ID de l'étudiant a supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un étudiant du système base sur l'ID fourni.
        /// </remarks>
        /// <response code="204">L'étudiant a été supprimé avec succes.</response>
        /// <response code="404">Si l'étudiant a supprimer n'est pas trouvé.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Professor")]
        public async Task<IActionResult> Delete(int id)
        {
            var etudiantExistant = await _service.GetById(id);
            if (etudiantExistant == null) return NotFound();

            await _service.Delete(id);
            return NoContent();
        }
    }
}

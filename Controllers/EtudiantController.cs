using Microsoft.AspNetCore.Mvc;
using ProjetWebAPI.Models;

namespace ProjetWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EtudiantController : ControllerBase
    {
        private readonly EtudiantService _service;

        public EtudiantController(EtudiantService service)
        {
            _service = service;
        }

        /// <summary>
        /// Récupère tous les étudiants.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les étudiants disponibles dans le système.
        /// </remarks>
        /// <response code="200">Retourne la liste des étudiants.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var etudiants = await _service.GetAll();
            return Ok(etudiants);
        }

        /// <summary>
        /// Récupère un étudiant spécifique par ID.
        /// </summary>
        /// <param name="id">L'ID de l'étudiant à récupérer.</param>
        /// <remarks>
        /// Cet endpoint retourne un étudiant unique basé sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourne l'étudiant demandé.</response>
        /// <response code="404">Si l'étudiant n'est pas trouvé.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var etudiant = await _service.GetById(id);
            if (etudiant == null) return NotFound();
            return Ok(etudiant);
        }

        /// <summary>
        /// Crée un nouvel étudiant.
        /// </summary>
        /// <param name="etudiant">L'objet étudiant à créer.</param>
        /// <remarks>
        /// Cet endpoint crée un nouvel étudiant dans le système.
        /// </remarks>
        /// <response code="201">L'étudiant a été créé avec succès.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Etudiants etudiant)
        {
            if (etudiant == null) return BadRequest("L'objet étudiant est nul.");

            await _service.Add(etudiant);
            return CreatedAtAction(nameof(Get), new { id = etudiant.Id }, etudiant);
        }

        /// <summary>
        /// Met à jour un étudiant existant.
        /// </summary>
        /// <param name="id">L'ID de l'étudiant à mettre à jour.</param>
        /// <param name="etudiant">L'objet étudiant mis à jour.</param>
        /// <remarks>
        /// Cet endpoint met à jour un étudiant existant dans le système.
        /// </remarks>
        /// <response code="204">L'étudiant a été mis à jour avec succès.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas à l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si l'étudiant à mettre à jour n'est pas trouvé.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Etudiants etudiant)
        {
            if (etudiant == null) return BadRequest("L'objet étudiant est nul.");
            if (id != etudiant.Id) return BadRequest("Incohérence d'ID.");

            var etudiantExistant = await _service.GetById(id);
            if (etudiantExistant == null) return NotFound();

            await _service.Update(etudiant);
            return NoContent();
        }

        /// <summary>
        /// Supprime un étudiant par ID.
        /// </summary>
        /// <param name="id">L'ID de l'étudiant à supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un étudiant du système basé sur l'ID fourni.
        /// </remarks>
        /// <response code="204">L'étudiant a été supprimé avec succès.</response>
        /// <response code="404">Si l'étudiant à supprimer n'est pas trouvé.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var etudiantExistant = await _service.GetById(id);
            if (etudiantExistant == null) return NotFound();

            await _service.Delete(id);
            return NoContent();
        }
    }
}

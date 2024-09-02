using Microsoft.AspNetCore.Mvc;
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
        /// Recuperer tous les etudiants.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les etudiants disponibles dans le systeme.
        /// </remarks>
        /// <response code="200">Retourner la liste des etudiants.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var etudiants = await _service.GetAll();
            return Ok(etudiants);
        }

        /// <summary>
        /// Recuperer un etudiant specifique par ID.
        /// </summary>
        /// <param name="id">L'ID de l'etudiant a recuperer.</param>
        /// <remarks>
        /// Cet endpoint retourne un etudiant unique base sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourner l'etudiant demande.</response>
        /// <response code="404">Si l'etudiant n'est pas trouve.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var etudiant = await _service.GetById(id);
            if (etudiant == null) return NotFound();
            return Ok(etudiant);
        }

        /// <summary>
        /// Creer un nouvel etudiant.
        /// </summary>
        /// <param name="etudiant">L'objet etudiant a creer.</param>
        /// <remarks>
        /// Cet endpoint cree un nouvel etudiant dans le systeme.
        /// </remarks>
        /// <response code="201">L'etudiant a ete cree avec succes.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Etudiants etudiant)
        {
            if (etudiant == null) return BadRequest("L'objet �tudiant est nul.");

            await _service.Add(etudiant);
            return CreatedAtAction(nameof(Get), new { id = etudiant.Id }, etudiant);
        }

        /// <summary>
        /// Mettre a jour un etudiant existant.
        /// </summary>
        /// <param name="id">L'ID de l'etudiant a mettre a jour.</param>
        /// <param name="etudiant">L'objet etudiant mis a jour.</param>
        /// <remarks>
        /// Cet endpoint met a jour un etudiant existant dans le systeme.
        /// </remarks>
        /// <response code="204">L'etudiant a ete mis a jour avec succes.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas a l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si l'etudiant a mettre a jour n'est pas trouve.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Etudiants etudiant)
        {
            if (etudiant == null) return BadRequest("L'objet �tudiant est nul.");
            if (id != etudiant.Id) return BadRequest("Incoh�rence d'ID.");

            var etudiantExistant = await _service.GetById(id);
            if (etudiantExistant == null) return NotFound();

            await _service.Update(etudiant);
            return NoContent();
        }

        /// <summary>
        /// Supprimer un etudiant par ID.
        /// </summary>
        /// <param name="id">L'ID de l'etudiant a supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un etudiant du systeme base sur l'ID fourni.
        /// </remarks>
        /// <response code="204">L'etudiant a ete supprime avec succes.</response>
        /// <response code="404">Si l'etudiant a supprimer n'est pas trouve.</response>
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

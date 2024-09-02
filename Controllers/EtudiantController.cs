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
        /// R�cup�re tous les �tudiants.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les �tudiants disponibles dans le syst�me.
        /// </remarks>
        /// <response code="200">Retourne la liste des �tudiants.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var etudiants = await _service.GetAll();
            return Ok(etudiants);
        }

        /// <summary>
        /// R�cup�re un �tudiant sp�cifique par ID.
        /// </summary>
        /// <param name="id">L'ID de l'�tudiant � r�cup�rer.</param>
        /// <remarks>
        /// Cet endpoint retourne un �tudiant unique bas� sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourne l'�tudiant demand�.</response>
        /// <response code="404">Si l'�tudiant n'est pas trouv�.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var etudiant = await _service.GetById(id);
            if (etudiant == null) return NotFound();
            return Ok(etudiant);
        }

        /// <summary>
        /// Cr�e un nouvel �tudiant.
        /// </summary>
        /// <param name="etudiant">L'objet �tudiant � cr�er.</param>
        /// <remarks>
        /// Cet endpoint cr�e un nouvel �tudiant dans le syst�me.
        /// </remarks>
        /// <response code="201">L'�tudiant a �t� cr�� avec succ�s.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Etudiants etudiant)
        {
            if (etudiant == null) return BadRequest("L'objet �tudiant est nul.");

            await _service.Add(etudiant);
            return CreatedAtAction(nameof(Get), new { id = etudiant.Id }, etudiant);
        }

        /// <summary>
        /// Met � jour un �tudiant existant.
        /// </summary>
        /// <param name="id">L'ID de l'�tudiant � mettre � jour.</param>
        /// <param name="etudiant">L'objet �tudiant mis � jour.</param>
        /// <remarks>
        /// Cet endpoint met � jour un �tudiant existant dans le syst�me.
        /// </remarks>
        /// <response code="204">L'�tudiant a �t� mis � jour avec succ�s.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas � l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si l'�tudiant � mettre � jour n'est pas trouv�.</response>
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
        /// Supprime un �tudiant par ID.
        /// </summary>
        /// <param name="id">L'ID de l'�tudiant � supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un �tudiant du syst�me bas� sur l'ID fourni.
        /// </remarks>
        /// <response code="204">L'�tudiant a �t� supprim� avec succ�s.</response>
        /// <response code="404">Si l'�tudiant � supprimer n'est pas trouv�.</response>
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

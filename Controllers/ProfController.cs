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
        /// R�cup�re tous les professeurs.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les professeurs disponibles dans le syst�me.
        /// </remarks>
        /// <response code="200">Retourne la liste des professeurs.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var professeurs = await _service.GetAll();
            return Ok(professeurs);
        }

        /// <summary>
        /// R�cup�re un professeur sp�cifique par ID.
        /// </summary>
        /// <param name="id">L'ID du professeur � r�cup�rer.</param>
        /// <remarks>
        /// Cet endpoint retourne un professeur unique bas� sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourne le professeur demand�.</response>
        /// <response code="404">Si le professeur n'est pas trouv�.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var professeur = await _service.GetById(id);
            if (professeur == null) return NotFound();
            return Ok(professeur);
        }

        /// <summary>
        /// Cr�e un nouveau professeur.
        /// </summary>
        /// <param name="prof">L'objet professeur � cr�er.</param>
        /// <remarks>
        /// Cet endpoint cr�e un nouveau professeur dans le syst�me.
        /// </remarks>
        /// <response code="201">Le professeur a �t� cr�� avec succ�s.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Profs prof)
        {
            if (prof == null) return BadRequest("L'objet professeur est nul.");

            await _service.Add(prof);
            return CreatedAtAction(nameof(Get), new { id = prof.Id }, prof);
        }

        /// <summary>
        /// Met � jour un professeur existant.
        /// </summary>
        /// <param name="id">L'ID du professeur � mettre � jour.</param>
        /// <param name="prof">L'objet professeur mis � jour.</param>
        /// <remarks>
        /// Cet endpoint met � jour un professeur existant dans le syst�me.
        /// </remarks>
        /// <response code="204">Le professeur a �t� mis � jour avec succ�s.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas � l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si le professeur � mettre � jour n'est pas trouv�.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Profs prof)
        {
            if (prof == null) return BadRequest("L'objet professeur est nul.");
            if (id != prof.Id) return BadRequest("Incoh�rence d'ID.");

            var professeurExistant = await _service.GetById(id);
            if (professeurExistant == null) return NotFound();

            await _service.Update(prof);
            return NoContent();
        }

        /// <summary>
        /// Supprime un professeur par ID.
        /// </summary>
        /// <param name="id">L'ID du professeur � supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un professeur du syst�me bas� sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le professeur a �t� supprim� avec succ�s.</response>
        /// <response code="404">Si le professeur � supprimer n'est pas trouv�.</response>
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

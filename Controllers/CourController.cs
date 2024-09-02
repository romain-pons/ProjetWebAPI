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
        /// R�cup�re tous les cours.
        /// </summary>
        /// <remarks>
        /// Cet endpoint retourne la liste de tous les cours disponibles dans le syst�me.
        /// </remarks>
        /// <response code="200">Retourne la liste des cours.</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cours = await _service.GetAll();
            return Ok(cours);
        }

        /// <summary>
        /// R�cup�re un cours sp�cifique par ID.
        /// </summary>
        /// <param name="id">L'ID du cours � r�cup�rer.</param>
        /// <remarks>
        /// Cet endpoint retourne un cours unique bas� sur l'ID fourni.
        /// </remarks>
        /// <response code="200">Retourne le cours demand�.</response>
        /// <response code="404">Si le cours n'est pas trouv�.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cours = await _service.GetById(id);
            if (cours == null) return NotFound();
            return Ok(cours);
        }

        /// <summary>
        /// Cr�e un nouveau cours.
        /// </summary>
        /// <param name="cours">L'objet cours � cr�er.</param>
        /// <remarks>
        /// Cet endpoint cr�e un nouveau cours dans le syst�me.
        /// </remarks>
        /// <response code="201">Le cours a �t� cr�� avec succ�s.</response>
        /// <response code="400">Si la demande est invalide.</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cours cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");

            await _service.Add(cours);
            return CreatedAtAction(nameof(Get), new { id = cours.Id }, cours);
        }

        /// <summary>
        /// Met � jour un cours existant.
        /// </summary>
        /// <param name="id">L'ID du cours � mettre � jour.</param>
        /// <param name="cours">L'objet cours mis � jour.</param>
        /// <remarks>
        /// Cet endpoint met � jour un cours existant dans le syst�me.
        /// </remarks>
        /// <response code="204">Le cours a �t� mis � jour avec succ�s.</response>
        /// <response code="400">Si l'ID dans l'URL ne correspond pas � l'ID dans le corps de la demande, ou si la demande est invalide.</response>
        /// <response code="404">Si le cours � mettre � jour n'est pas trouv�.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Cours cours)
        {
            if (cours == null) return BadRequest("L'objet cours est nul.");
            if (id != cours.Id) return BadRequest("Incoh�rence d'ID.");

            var coursExistant = await _service.GetById(id);
            if (coursExistant == null) return NotFound();

            await _service.Update(cours);
            return NoContent();
        }

        /// <summary>
        /// Supprime un cours par ID.
        /// </summary>
        /// <param name="id">L'ID du cours � supprimer.</param>
        /// <remarks>
        /// Cet endpoint supprime un cours du syst�me bas� sur l'ID fourni.
        /// </remarks>
        /// <response code="204">Le cours a �t� supprim� avec succ�s.</response>
        /// <response code="404">Si le cours � supprimer n'est pas trouv�.</response>
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

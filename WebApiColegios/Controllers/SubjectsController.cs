using DomainLayer.DTOs;
using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApiColegios.Controllers
{
    [ApiController]
    [Route("api/school/subjects")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        /// <summary>
        /// Lista todas las materias.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetAll()
        {
            var subjects = await _subjectService.GetAllAsync();
            return Ok(subjects);
        }

        /// <summary>
        /// Obtiene una materia por ID.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SubjectDTO>> GetById(int id)
        {
            var subject = await _subjectService.GetByIdAsync(id);
            if (subject == null)
                return NotFound();

            return Ok(subject);
        }

        /// <summary>
        /// Crea una nueva materia.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SubjectDTO>> Create(CreateSubjectDTO dto)
        {
            var created = await _subjectService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.SubjectID }, created);
        }

        /// <summary>
        /// Actualiza una materia existente.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UpdateSubjectDTO dto)
        {
            await _subjectService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Elimina una materia.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _subjectService.DeleteAsync(id);
            return NoContent();
        }
    }
}

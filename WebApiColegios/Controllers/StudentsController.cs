using DomainLayer.DTOs;
using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApiColegios.Controllers
{
    [ApiController]
    [Route("api/school/Students")]
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("lista")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDTO>> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDTO>> Create(CreateStudentDTO dto)
        {
            var created = await _studentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.StudentID }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, UpdateStudentDTO dto)
        {
            await _studentService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}

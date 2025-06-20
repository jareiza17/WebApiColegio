using DomainLayer.DTOs;
using DomainLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer.Entities;

namespace WebApiColegios.Controllers
{
    [ApiController]
    [Route("api/school/enrollments")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        /// <summary>
        /// Inscribe materias a un estudiante.
        /// </summary>
        /// <param name="dto">DTO con el ID del estudiante y las materias</param>
        [HttpPost]
        public async Task<ActionResult> EnrollStudent([FromBody] EnrollmentDTO dto)
        {
            try
            {
                await _enrollmentService.EnrollAsync(dto);
                return Ok("Enrollment successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{studentId:int}")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjectsByStudent(int studentId)
        {
            try
            {
                var subjects = await _enrollmentService.GetSubjectsByStudentIdAsync(studentId);
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

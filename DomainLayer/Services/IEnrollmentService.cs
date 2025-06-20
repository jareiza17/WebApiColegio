using DomainLayer.DTOs;
using ModelsLayer.Entities;

namespace DomainLayer.Services
{
    public interface IEnrollmentService
    {
        Task EnrollAsync(EnrollmentDTO dto);
        Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId);

    }
}

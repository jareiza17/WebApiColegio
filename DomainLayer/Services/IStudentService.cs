using DomainLayer.DTOs;
using ModelsLayer.Entities;

namespace DomainLayer.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllAsync();
        Task<StudentDTO> GetByIdAsync(int id);
        Task<StudentDTO> CreateAsync(CreateStudentDTO dto);
        Task UpdateAsync(int id, UpdateStudentDTO dto);
        Task DeleteAsync(int id);
    }
}

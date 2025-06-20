using DomainLayer.DTOs;

namespace DomainLayer.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetAllAsync();
        Task<SubjectDTO> GetByIdAsync(int id);
        Task<SubjectDTO> CreateAsync(CreateSubjectDTO dto);
        Task UpdateAsync(int id, UpdateSubjectDTO dto);
        Task DeleteAsync(int id);
    }
}

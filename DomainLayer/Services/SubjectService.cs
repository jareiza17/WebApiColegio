using AutoMapper;
using DomainLayer.DTOs;
using DomainLayer.Repositories;
using ModelsLayer.Entities;

namespace DomainLayer.Services
{
    public class SubjectService: ISubjectService
    {
        private readonly IGenericRepository<Subject> _repository;
        private readonly IMapper _mapper;

        public SubjectService(IGenericRepository<Subject> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDTO>> GetAllAsync()
        {
            var subjects = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SubjectDTO>>(subjects);
        }

        public async Task<SubjectDTO> GetByIdAsync(int id)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
                throw new Exception("Subject not found");

            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task<SubjectDTO> CreateAsync(CreateSubjectDTO dto)
        {
            var subject = _mapper.Map<Subject>(dto);
            await _repository.CreateAsync(subject);
            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task UpdateAsync(int id, UpdateSubjectDTO dto)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
                throw new Exception("Subject not found");

            _mapper.Map(dto, subject);
            await _repository.UpdateAsync(subject);
        }

        public async Task DeleteAsync(int id)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
                throw new Exception("Subject not found");

            await _repository.DeleteAsync(subject);
        }
    }
}

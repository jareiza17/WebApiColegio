using AutoMapper;
using DomainLayer.DTOs;
using DomainLayer.Repositories;
using ModelsLayer.Entities;

namespace DomainLayer.Services
{
    public class StudentService: IStudentService
    {
        private readonly IGenericRepository<Student> _repository;
        private readonly IMapper _mapper;

        public StudentService(IGenericRepository<Student> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<StudentDTO>>(students);
    }

    public async Task<StudentDTO> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
            throw new Exception("Student not found");

        return _mapper.Map<StudentDTO>(student);
    }

    public async Task<StudentDTO> CreateAsync(CreateStudentDTO dto)
    {
        var student = _mapper.Map<Student>(dto);
        await _repository.CreateAsync(student);
        return _mapper.Map<StudentDTO>(student);
    }

    public async Task UpdateAsync(int id, UpdateStudentDTO dto)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
            throw new Exception("Student not found");

        _mapper.Map(dto, student);
        await _repository.UpdateAsync(student);
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
            throw new Exception("Student not found");

        await _repository.DeleteAsync(student);
    }
    }
}

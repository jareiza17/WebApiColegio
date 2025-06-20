using AutoMapper;
using DomainLayer.DTOs;
using DomainLayer.Repositories;
using ModelsLayer.Entities;

namespace DomainLayer.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IGenericRepository<Enrollment> _enrollmentRepo;
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IGenericRepository<Subject> _subjectRepo;

        public EnrollmentService(
            IGenericRepository<Enrollment> enrollmentRepo,
            IGenericRepository<Student> studentRepo,
            IGenericRepository<Subject> subjectRepo)
        {
            _enrollmentRepo = enrollmentRepo;
            _studentRepo = studentRepo;
            _subjectRepo = subjectRepo;
        }

        public async Task EnrollAsync(EnrollmentDTO dto)
        {
            var student = await _studentRepo.GetByIdAsync(dto.StudentID);
            if (student == null)
                throw new Exception("Student not found");

            // Obtener materias seleccionadas
            var selectedSubjects = await _subjectRepo.FindAsync(s => dto.SubjectIDs.Contains(s.SubjectID));

            // Obtener materias con más de 4 créditos
            var highCreditSubjects = selectedSubjects.Where(s => s.Credits > 4).ToList();

            if (highCreditSubjects.Count > 3)
                throw new Exception("A student cannot enroll in more than 3 subjects with more than 4 credits.");

            // Obtener inscripciones existentes del estudiante
            var existingEnrollments = await _enrollmentRepo.FindAsync(e => e.StudentID == dto.StudentID);
            var alreadyEnrolledSubjectIds = existingEnrollments.Select(e => e.SubjectID).ToHashSet();

            // Filtrar materias ya inscritas
            var newSubjectsToEnroll = selectedSubjects
                .Where(s => !alreadyEnrolledSubjectIds.Contains(s.SubjectID))
                .ToList();

            // Crear solo inscripciones nuevas
            foreach (var subject in newSubjectsToEnroll)
            {
                var enrollment = new Enrollment
                {
                    StudentID = dto.StudentID,
                    SubjectID = subject.SubjectID
                };

                await _enrollmentRepo.CreateAsync(enrollment);
            }
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByStudentIdAsync(int studentId)
        {
            try
            {
                // Obtener las inscripciones del estudiante
                var enrollments = await _enrollmentRepo.FindAsync(e => e.StudentID == studentId)
                                  ?? new List<Enrollment>();

                if (!enrollments.Any())
                    return new List<Subject>();

                // Obtener IDs de materias inscritas
                var subjectIds = enrollments.Select(e => e.SubjectID).ToList();

                // Buscar las materias correspondientes
                var subjects = await _subjectRepo.FindAsync(s => subjectIds.Contains(s.SubjectID))
                               ?? new List<Subject>();

                return subjects;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

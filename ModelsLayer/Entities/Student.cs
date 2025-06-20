using System.ComponentModel.DataAnnotations;

namespace ModelsLayer.Entities
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string DocumentNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}

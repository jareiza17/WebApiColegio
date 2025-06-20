using System.ComponentModel.DataAnnotations;

namespace DomainLayer.DTOs
{
    public class CreateStudentDTO
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string DocumentNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

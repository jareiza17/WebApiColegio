using System.ComponentModel.DataAnnotations;

namespace DomainLayer.DTOs
{
    public class EditAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

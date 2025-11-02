using System.ComponentModel.DataAnnotations;

namespace ApiProject.DTOs
{
    public class ParentDto
    {
        [Required]
        public int ParentId { get; set; }

        [Required]
        public string ParentName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}

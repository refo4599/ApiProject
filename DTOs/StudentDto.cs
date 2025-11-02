using System.ComponentModel.DataAnnotations;

namespace ApiProject.DTOs
{
    public class StudentDto
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public int ParentId { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class EmployeeForManipulationDto
    {
        [MaxLength(30, ErrorMessage = "Name Max Length is 30")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative integer")]
        public int Age { get; set; }

        [Required] [MaxLength(20)] public string Position { get; set; }
    }
}